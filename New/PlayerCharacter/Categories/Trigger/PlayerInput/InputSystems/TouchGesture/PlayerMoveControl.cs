using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Nino.NewStateMatching.PlayerCharacter.Trigger.PlayerInput;
public class PlayerMoveControl : MonoBehaviour
{
    public InputExecuter_TouchGesture executer;


    [BoxGroup("Reference")] [field:SerializeField] InputReader inputControl;
    // public value in
    [TabGroup("Input Value")] [field: SerializeField] public float WalkDis;
    [TabGroup("Input Value")][field: SerializeField] public float RunDis;
    // Public value out
    [TabGroup("Output Value")] public Vector2 _moveDirection;
   /* public Vector2 moveDirection
    {
        get => _moveDirection;
        set
        {
            if (value == _moveDirection) return;
            _moveDirection = value;
            if (executer == null) return;
            executer.dynamicDataController.AssignValue("Move Direction", value);

        }
    }
    [TabGroup("Output Value")] [field: SerializeField] public bool isMoving;
    [TabGroup("Output Value")] [field: SerializeField] private int _moveType;
    public int moveType
    {
        get { return _moveType; }
        set
        {
            if (value != _moveType && executer != null)
            {
                if (_moveType == -1)
                {
                    if (value > -1) executer.eventController.InvokeEvent("Start Move");
                    if (value == 1) executer.eventController.InvokeEvent("Start Run");
                }
                if (_moveType == 0)
                {
                    if (value == 1) executer.eventController.InvokeEvent("Start Run");
                    else executer.eventController.InvokeEvent("Stop Move");
                }
                if (_moveType == 1)
                {
                    if (value < 1) executer.eventController.InvokeEvent("Stop Run");
                    if (value == -1) executer.eventController.InvokeEvent("Stop Move");
                }
            }
            
            _moveType = value;
        }
    }*/

    UnityAction[] FingerMoveAction;
    UnityAction[] PreFingerLeaveAction;
    private void Awake()
    {
        FingerMoveAction = new UnityAction[2];
        PreFingerLeaveAction = new UnityAction[2];
        for (int i = 0; i<2; i++)
        {
            int value = i;
            FingerMoveAction[value] += delegate { TrySetMove(value); };
            PreFingerLeaveAction[value] += delegate { TryClearMove(value); };
        }
    }
    void Start()
    {
        SetUpValue();
    }
    void SetUpValue()
    {
       /* moveDirection = Vector2.zero;
        isMoving = false;
        moveType = -1;*/
    }
    private void OnEnable()
    {
        if (executer == null) executer = GetComponent<InputExecuter_TouchGesture>();
        if (inputControl == null) inputControl = gameObject.GetComponent<InputReader>();
        for(int i=0; i < 2; i++)
        {
            inputControl.PreFingerLeave[i].AddListener(PreFingerLeaveAction[i]);
            inputControl.FingerMove[i].AddListener(FingerMoveAction[i]);
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < 2; i++)
        {
            inputControl.PreFingerLeave[i].RemoveListener(PreFingerLeaveAction[i]);
            inputControl.FingerMove[i].RemoveListener(FingerMoveAction[i]);
        }
    }
    public void TrySetMove(int fingerIndex)
    { 
        /*if (inputControl.fingerRule[fingerIndex] != 0) return;
        Vector2 Difference = inputControl.fingerLocation[fingerIndex] - inputControl.fingerStartLocation[fingerIndex];
        float FingerMoveDis = Difference.magnitude;
        if (FingerMoveDis < WalkDis) moveType = -1;
        else if (FingerMoveDis < RunDis) moveType = 0;
        else moveType = 1;
        moveDirection = Difference.normalized;*/
        
        
    }
    public void TryClearMove(int i)
    {
        /*if (inputControl.fingerRule[i] != 0) return;
        moveDirection = Vector2.zero;
        moveType = -1;*/
    }
    [Button(ButtonSizes.Medium)]
    [GUIColor(0.6f, 1f, 0.6f)]
    [TabGroup("Input Value")]
    void RestoreDefaultValue()
    {
        WalkDis = 15;
        RunDis = 50;
    }
}
