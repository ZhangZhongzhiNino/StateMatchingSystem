using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Nino.NewStateMatching.PlayerCharacter.Trigger.PlayerInput;
public class PlayerMoveControl : MonoBehaviour
{
    public InputExecuter_TouchGesture executer;


    [BoxGroup("Reference")] [field:SerializeField] InputReader inputReader;
    // public value in
    [TabGroup("Input Value")] [field: SerializeField] public float WalkDis;
    [TabGroup("Input Value")][field: SerializeField] public float RunDis;
    // Public value out
    [TabGroup("Output Value")] public Vector2 _moveDirection;
    public Vector2 moveDirection
    {
        get => _moveDirection;
        set
        {
            if (value == _moveDirection) return;
            _moveDirection = value;
            if (executer == null) return;
            executer.dataController.GetItem("Move Direction").setValue(value);

        }
    }
    [TabGroup("Output Value")][field: SerializeField] private int _moveType;
    public int moveType
    {
        get { return _moveType; }
        set
        {
            if (value != _moveType && executer != null)
            {
                if (_moveType == -1)
                {
                    

                    if (value > -1)
                    {
                        UnityEvent getEvent = executer.dataController.GetItem("Start Move").value as UnityEvent;
                        getEvent.Invoke();
                    }
                    if (value == 1)
                    {
                        UnityEvent getEvent = executer.dataController.GetItem("Start Run").value as UnityEvent;
                        getEvent.Invoke();
                    }
                }
                if (_moveType == 0)
                {
                    if (value == 1)
                    {
                        UnityEvent getEvent = executer.dataController.GetItem("Start Run").value as UnityEvent;
                        getEvent.Invoke();
                    }
                    else if (value == -1)
                    {
                        UnityEvent getEvent = executer.dataController.GetItem("Stop Move").value as UnityEvent;
                        getEvent.Invoke();
                    }

                }
                if (_moveType == 1)
                {
                    if (value < 1)
                    {
                        UnityEvent getEvent = executer.dataController.GetItem("Stop Run").value as UnityEvent;
                        getEvent.Invoke();
                    }
                    if (value == -1)
                    {
                        UnityEvent getEvent = executer.dataController.GetItem("Stop Move").value as UnityEvent;
                        getEvent.Invoke();
                    }
                }
            }

            _moveType = value;
        }
    }

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
        moveDirection = Vector2.zero;
        moveType = -1;
    }
    private void OnEnable()
    { 
        if (executer == null) executer = GetComponent<InputExecuter_TouchGesture>();
        if (inputReader == null) inputReader = gameObject.GetComponent<InputReader>();
        for(int i=0; i < 2; i++)
        {
            inputReader.PreFingerLeave[i].AddListener(PreFingerLeaveAction[i]);
            inputReader.FingerMove[i].AddListener(FingerMoveAction[i]);
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < 2; i++)
        {
            inputReader.PreFingerLeave[i].RemoveListener(PreFingerLeaveAction[i]);
            inputReader.FingerMove[i].RemoveListener(FingerMoveAction[i]);
        }
    }
    public void TrySetMove(int fingerIndex)
    {
        if (inputReader.fingerRule[fingerIndex] != 0) return;
        Vector2 Difference = inputReader.fingerLocation[fingerIndex] - inputReader.fingerStartLocation[fingerIndex];
        float FingerMoveDis = Difference.magnitude;
        if (FingerMoveDis < WalkDis) moveType = -1;
        else if (FingerMoveDis < RunDis) moveType = 0;
        else moveType = 1;
        moveDirection = Difference.normalized;


    }
    public void TryClearMove(int i)
    {
        if (inputReader.fingerRule[i] != 0) return;
        moveDirection = Vector2.zero;
        moveType = -1;
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
