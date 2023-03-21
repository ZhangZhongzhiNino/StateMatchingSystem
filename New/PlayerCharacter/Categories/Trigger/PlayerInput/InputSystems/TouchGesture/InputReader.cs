using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Nino.NewStateMatching.Trigger.PlayerInput;
public class InputReader : MonoBehaviour, PlayerInput.ITouchActions 
{
    public InputExecuter_TouchGesture executer;

    public float delta0Timer;
    public float delta1Timer;
    IEnumerator ClearDelta()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (delta0Timer < 0) deltaLocation[0] = Vector2.zero;
            else delta0Timer -= 0.2f;
            if (delta1Timer < 0) deltaLocation[1] = Vector2.zero;
            else delta1Timer -= 0.2f;
        }
    }

    //private settings
    private PlayerInput Pinput;
    private int fingerCount;

    //public settings
    [TabGroup("Tap")] public UnityEvent[] TapEvent;
    [TabGroup("On touch")] public UnityEvent[] OnFinger;
    [TabGroup("Finger Leave")] public UnityEvent[] PreFingerLeave;
    [TabGroup("Finger Leave")] public UnityEvent[] OnFingerLeave;
    [TabGroup("Finger Move")] public UnityEvent[] FingerMove;

    //public value
    [BoxGroup("Current Data")] public Vector2[] fingerLocation; [Space]

    [BoxGroup("Current Data")] public bool[] fingerOn; [Space]
    [BoxGroup("Current Data")] public Vector2 lastTapLocation; [Space]
    [BoxGroup("Current Data")] public Vector2[] fingerStartLocation; [Space]
    [BoxGroup("Current Data")] public int[] fingerRule; [Space]
    [BoxGroup("Current Data")] public Vector2[] deltaLocation;
    private void Awake() => SetUpValues();
    private void Start() => StartCallBack();
    void StartCallBack()
    {
        Pinput = new PlayerInput();
        Pinput.Touch.SetCallbacks(this);
        Pinput.Touch.Enable(); 
    }
    private void OnEnable()
    {
        StartCoroutine(ClearDelta());
    }
    private void OnDisable()
    {
        StopCoroutine(ClearDelta());
    }
    void SetUpValues()
    {
        delta0Timer = 0;
        delta1Timer = 0;
        fingerCount = 2;

        if(TapEvent.Length < fingerCount) TapEvent = new UnityEvent[2];
        if (OnFinger.Length < fingerCount) OnFinger = new UnityEvent[2];
        if (PreFingerLeave.Length < fingerCount) PreFingerLeave = new UnityEvent[2];
        if (OnFingerLeave.Length < fingerCount) OnFingerLeave = new UnityEvent[2];
        if (FingerMove.Length < fingerCount) FingerMove = new UnityEvent[2];

        fingerLocation = new Vector2[fingerCount];
        fingerStartLocation = new Vector2[fingerCount];
        deltaLocation = new Vector2[fingerCount];
        fingerOn = new bool[fingerCount];
        fingerRule = new int[fingerCount];
        
        for (int i = 0; i<fingerCount; i++)
        {
            TapEvent[i] = new UnityEvent();
            OnFinger[i] = new UnityEvent();
            PreFingerLeave[i] = new UnityEvent();
            OnFingerLeave[i] = new UnityEvent();
            FingerMove[i] = new UnityEvent();

            fingerLocation[i] = Vector2.zero;
            fingerStartLocation[i] = Vector2.zero;
            deltaLocation[i] = Vector2.zero;
            fingerOn[i] = false;
            fingerRule[i] = -1;
        }
    
        lastTapLocation = Vector2.zero;
    }
    
    void PlayerInput.ITouchActions.OnFinger0(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
        if (!context.performed) 
        {
            if(context.phase == UnityEngine.InputSystem.InputActionPhase.Canceled)
            {
                fingerOn[0] = false;
                PreFingerLeave[0]?.Invoke();
                fingerRule[0] = -1;
                OnFingerLeave[0]?.Invoke();
            }
            return;
        } 
        MarkStartLocation_AssignRule(0);
    }

    void PlayerInput.ITouchActions.OnFinger1(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            if (context.phase == UnityEngine.InputSystem.InputActionPhase.Canceled)
            {
                fingerOn[1] = false;
                PreFingerLeave[1]?.Invoke();
                fingerRule[1] = -1;
                OnFingerLeave[1]?.Invoke();
            }
                
            return;
        }
        MarkStartLocation_AssignRule(1);
    }

    void PlayerInput.ITouchActions.OnLocation0(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        delta0Timer = 0.2f;
        if (!context.performed) return;
        if (fingerLocation[0] != Vector2.zero) deltaLocation[0] = context.ReadValue<Vector2>() - fingerLocation[0];
        fingerLocation[0] = context.ReadValue<Vector2>();
        FingerMove[0]?.Invoke();
        if (fingerRule[0] == 0) executer.dataController.GetItem("Delta L").setValue(deltaLocation[0]);
        else executer.dataController.GetItem("Delta R").setValue(deltaLocation[0]);
    }

    void PlayerInput.ITouchActions.OnLocation1(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        delta1Timer = 0.2f;
        if (!context.performed) return;
        if (fingerLocation[1] != Vector2.zero) deltaLocation[1] = context.ReadValue<Vector2>() - fingerLocation[1];
        fingerLocation[1] = context.ReadValue<Vector2>();
        FingerMove[1]?.Invoke();
        if (fingerRule[1] == 0) executer.dataController.GetItem("Delta L").setValue(deltaLocation[1]);
        else executer.dataController.GetItem("Delta R").setValue(deltaLocation[1]);
    }

    void PlayerInput.ITouchActions.OnTap0(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        lastTapLocation = fingerLocation[0];
        TapEvent[0]?.Invoke();


        if(lastTapLocation.x > (Screen.width/2))
        {
            UnityEvent tapEvent = executer.dataController.GetItem("Tap R").value as UnityEvent;
            tapEvent.Invoke();
        }
        else
        {
            UnityEvent tapEvent = executer.dataController.GetItem("Tap L").value as UnityEvent;
            tapEvent.Invoke();
        }
    }

    void PlayerInput.ITouchActions.OnTap1(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        lastTapLocation = fingerLocation[1];
        TapEvent[1]?.Invoke();
        if (lastTapLocation.x > (Screen.width / 2))
        {
            UnityEvent tapEvent = executer.dataController.GetItem("Tap R").value as UnityEvent;
            tapEvent.Invoke();
        }
        else
        {
            UnityEvent tapEvent = executer.dataController.GetItem("Tap L").value as UnityEvent;
            tapEvent.Invoke();
        }
    }

    void MarkStartLocation_AssignRule(int fingerIndex)
    {
        fingerOn[fingerIndex] = true;
        int anotherFingerIndex;
        if (fingerIndex == 0) anotherFingerIndex = 1;
        else anotherFingerIndex = 0;
        fingerStartLocation[fingerIndex] = fingerLocation[fingerIndex];
        if (fingerStartLocation[fingerIndex].x < (Screen.width / 2))
        {
            if (fingerRule[anotherFingerIndex] != 0) fingerRule[fingerIndex] = 0;
            else fingerRule[fingerIndex] = 1;
        }
        else if (fingerStartLocation[fingerIndex].x >= (Screen.width / 2))
        {
            if (fingerRule[anotherFingerIndex] != 1) fingerRule[fingerIndex] = 1;
            else fingerRule[fingerIndex] = 0;
        }
        OnFinger[fingerIndex]?.Invoke();
    }
}
