using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Nino.StateMatching.Helper;
public class PlayerMoveControl : MonoBehaviour,IEventContainer
{
    [BoxGroup("Reference")] [field:SerializeField] InputControl inputControl;
    [BoxGroup("Reference")][field: SerializeField] JoysticController joystic;
    // public value in
    [TabGroup("Input Value")] [field: SerializeField] public float WalkDis;
    // Public value out
    [TabGroup("Output Value")] [field: SerializeField] public Vector2 moveDirection;
    [TabGroup("Output Value")] [field: SerializeField] public bool isMoving;
    [TabGroup("Output Value")] [field: SerializeField] private int _moveType;
    public int moveType
    {
        get { return _moveType; }
        set 
        { 
            if (value != _moveType)
            {
                if (value == -1)
                {
                    isMoving = false;
                    if (_moveType == 0) stopWalk?.Invoke();
                    if(_moveType==1) stopRun?.Invoke();
                }
                else
                {
                    isMoving = true;
                    if (_moveType == -1) startMoving?.Invoke();
                    if (value == 0) startWalk?.Invoke();
                    if (value == 1) startRun?.Invoke();
                }
            }
            _moveType = value;
        }
    }
    [TabGroup("Events")] public UnityEvent stopWalk;
    [TabGroup("Events")] public UnityEvent stopRun;
    [TabGroup("Events")] public UnityEvent startWalk;
    [TabGroup("Events")] public UnityEvent startRun;
    [TabGroup("Events")] public UnityEvent startMoving;

    UnityAction[] FingerMoveAction;
    UnityAction[] PreFingerLeaveAction;
    UnityAction[] OnFingerAction;
    private void Awake()
    {
        FingerMoveAction = new UnityAction[2];
        PreFingerLeaveAction = new UnityAction[2];
        OnFingerAction = new UnityAction[2];
        for (int i = 0; i<2; i++)
        {
            int value = i;
            FingerMoveAction[value] += delegate { TrySetMove(value); };
            PreFingerLeaveAction[value] += delegate { TryClearMove(value); };
            OnFingerAction[value] += delegate { TryCreateJoystic(value); };
        }
    }
    void Start()
    {
        SetUpValue();
    }
    void SetUpValue()
    {
        moveDirection = Vector2.zero;
        isMoving = false;
        moveType = -1;
    }
    private void OnEnable()
    {
        for(int i=0; i < 2; i++)
        {
            inputControl.OnFinger[i].AddListener(OnFingerAction[i]);
            inputControl.PreFingerLeave[i].AddListener(PreFingerLeaveAction[i]);
            inputControl.FingerMove[i].AddListener(FingerMoveAction[i]);
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < 2; i++)
        {
            inputControl.OnFinger[i].RemoveListener(OnFingerAction[i]);
            inputControl.PreFingerLeave[i].RemoveListener(PreFingerLeaveAction[i]);
            inputControl.FingerMove[i].RemoveListener(FingerMoveAction[i]);
        }
    }
    public void TrySetMove(int fingerIndex)
    { 
        if (inputControl.fingerRule[fingerIndex] != 0) return;
        Vector2 Difference = inputControl.fingerLocation[fingerIndex] - inputControl.fingerStartLocation[fingerIndex];
        float FingerMoveDis = Difference.magnitude;
        if (FingerMoveDis < WalkDis) return;
        if (joystic.pointerInCircle) moveType = 0;
        else moveType = 1;
        moveDirection = Difference.normalized;
        
        
    }
    public void TryClearMove(int i)
    {
        if (inputControl.fingerRule[i] != 0) return;
        joystic.destroyIcon();
        moveDirection = Vector2.zero;
        moveType = -1;
    }
    public void TryCreateJoystic(int i)
    {
        if (inputControl.fingerRule[i] != 0) return;
        joystic.createIcon(inputControl.fingerStartLocation[i]);
    }
    [Button(ButtonSizes.Medium)]
    [GUIColor(0.6f, 1f, 0.6f)]
    [TabGroup("Input Value")]
    void RestoreDefaultValue()
    {
        WalkDis = 15;
    }
}
