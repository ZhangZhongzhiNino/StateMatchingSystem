using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using System;
using Nino.NewStateMatching.PlayerCharacter.Trigger.PlayerInput;
public class SlideGestureInput : MonoBehaviour
{
    public InputExecuter_TouchGesture executer;


    [BoxGroup("Reference")][Required][field: SerializeField] InputReader inputReader;

    [TabGroup("Tap")] public UnityEvent singleTap;

    [TabGroup("Slide")] public UnityEvent Slide8;
    [TabGroup("Slide")] public UnityEvent Slide2;
    [TabGroup("Slide")] public UnityEvent Slide4;
    [TabGroup("Slide")] public UnityEvent Slide6;
    [TabGroup("Slide")] public UnityEvent Slide82;
    [TabGroup("Slide")] public UnityEvent Slide28;
    [TabGroup("Slide")] public UnityEvent Slide46;
    [TabGroup("Slide")] public UnityEvent Slide64;


    //tap data
    [TabGroup("Data", "Tap Data")][field : SerializeField] float tapFinishInterval;
    [TabGroup("Data", "Tap Data")][field : SerializeField] bool inFinishInterval;

    //gesture data
    [TabGroup("Data", "Gesture Data")][field: SerializeField] float minimumGestureDistance;
    [TabGroup("Data", "Gesture Data")][field: SerializeField] float gestureDenoiseThreshold;
    [TabGroup("Data", "Gesture Data")][field: SerializeField] Vector2 upMostPosition;
    [TabGroup("Data", "Gesture Data")][field: SerializeField] Vector2 downMostPosition;
    [TabGroup("Data", "Gesture Data")][field: SerializeField] Vector2 leftMostPosition;
    [TabGroup("Data", "Gesture Data")][field: SerializeField] Vector2 rightMostPosition;

    //UnityActions
    UnityAction[] OnfingerAction;
    UnityAction[] FingerMoveActions;
    UnityAction[] PreFingerLeaveActions;

    void Start()
    {
        inFinishInterval = false;
        upMostPosition = Vector2.zero;
        downMostPosition = Vector2.zero;
        leftMostPosition = Vector2.zero;
        rightMostPosition = Vector2.zero;
    }
    private void Awake()
    {
        OnfingerAction = new UnityAction[2];
        FingerMoveActions = new UnityAction[2];
        PreFingerLeaveActions = new UnityAction[2];
        for(int i = 0; i < 2; i++)
        {
            int value = i;
            OnfingerAction[value] += delegate { onGestureStart(value); };
            FingerMoveActions[value] += delegate { onGestureMove(value); };
            PreFingerLeaveActions[value] += delegate { onGestureFinish(value); };
        }
        //Gesture Debug
        /*Slide2.AddListener(delegate { EventDebug("2"); });
        Slide4.AddListener(delegate { EventDebug("4"); });
        Slide6.AddListener(delegate { EventDebug("6"); });
        Slide8.AddListener(delegate { EventDebug("8"); });
        Slide28.AddListener(delegate { EventDebug("28"); });
        Slide82.AddListener(delegate { EventDebug("82"); });
        Slide46.AddListener(delegate { EventDebug("46"); });
        Slide64.AddListener(delegate { EventDebug("64"); }); */
    }
    private void OnEnable()
    {
        if (executer == null) executer = GetComponent<InputExecuter_TouchGesture>();
        if (inputReader == null) inputReader = gameObject.GetComponent<InputReader>();
        for(int i = 0; i < 2; i++)
        {
            inputReader.TapEvent[i].AddListener(OnTap);
            inputReader.OnFinger[i].AddListener(OnfingerAction[i]);
            inputReader.FingerMove[i].AddListener(FingerMoveActions[i]);
            inputReader.PreFingerLeave[i].AddListener(PreFingerLeaveActions[i]);
        }

        
    }
    private void OnDisable()
    {
        for (int i = 0; i < 2; i++)
        {
            inputReader.TapEvent[i].RemoveListener(OnTap);
            inputReader.OnFinger[i].RemoveListener(OnfingerAction[i]);
            inputReader.FingerMove[i].RemoveListener(FingerMoveActions[i]);
            inputReader.PreFingerLeave[i].RemoveListener(PreFingerLeaveActions[i]);
        }


    }
    //EventDebug
    void EventDebug(string text)
    {
        Debug.Log(text);
    }

    //Tap Functions
    void OnTap()
    {
        if (inFinishInterval) return;
        if (EventSystem.current?.currentSelectedGameObject != null) return; 
        singleTap?.Invoke();
        inFinishInterval = true;
        StartCoroutine(tapFinishTimer());
    }
    
    IEnumerator tapFinishTimer()
    {
        yield return new WaitForSeconds(tapFinishInterval);
        inFinishInterval = false;
    }
    //Gesture Functions

    void onGestureStart(int fingerIndex)
    {
        if (inputReader.fingerRule[fingerIndex] != 1) return;
        upMostPosition = inputReader.fingerStartLocation[fingerIndex];
        downMostPosition = inputReader.fingerStartLocation[fingerIndex];
        leftMostPosition = inputReader.fingerStartLocation[fingerIndex]; 
        rightMostPosition = inputReader.fingerStartLocation[fingerIndex];
    }
    void onGestureMove(int fingerIndex)
    {
        if (inputReader.fingerRule[fingerIndex] != 1) return;
        if (inputReader.fingerLocation[fingerIndex].y > upMostPosition.y) upMostPosition = inputReader.fingerLocation[fingerIndex];
        if (inputReader.fingerLocation[fingerIndex].y < downMostPosition.y) downMostPosition = inputReader.fingerLocation[fingerIndex];
        if (inputReader.fingerLocation[fingerIndex].x > rightMostPosition.x) rightMostPosition = inputReader.fingerLocation[fingerIndex];
        if (inputReader.fingerLocation[fingerIndex].x < leftMostPosition.x) leftMostPosition = inputReader.fingerLocation[fingerIndex];
    }
    void onGestureFinish(int fingerIndex)
    {
        if (inputReader.fingerRule[fingerIndex] != 1) return;
        
        Vector2 difference = new Vector2(0,0);
        difference.x = rightMostPosition.x - leftMostPosition.x;
        difference.y = upMostPosition.y - downMostPosition.y;
        if (difference.magnitude < minimumGestureDistance) return;
        GestureCalculator(difference, fingerIndex);
    }

    void GestureCalculator(Vector2 difference, int fingerIndex)
    {
        Vector2 finishLocation = inputReader.fingerLocation[fingerIndex];
        Vector2 startLocation = inputReader.fingerStartLocation[fingerIndex];
        if (Mathf.Abs(difference.y) > Mathf.Abs(difference.x))
        {
            if (finishLocation.y > startLocation.y)
            {
                if ((finishLocation - upMostPosition).magnitude < gestureDenoiseThreshold
                    && (startLocation - downMostPosition).magnitude < gestureDenoiseThreshold)
                    executer.eventController.InvokeEvent("Slide 8");
                else
                {
                    if (upMostPosition.y > finishLocation.y) executer.eventController.InvokeEvent("Slide 82");
                    else if (downMostPosition.y < startLocation.y) executer.eventController.InvokeEvent("Slide 28");
                }
            }
            else if(finishLocation.y < startLocation.y)
            {
                if ((finishLocation - downMostPosition).magnitude < gestureDenoiseThreshold
                    && (startLocation - upMostPosition).magnitude < gestureDenoiseThreshold)
                    executer.eventController.InvokeEvent("Slide 2");
                else
                {
                    if (upMostPosition.y > startLocation.y) executer.eventController.InvokeEvent("Slide 82"); 
                    else if (downMostPosition.y < finishLocation.y) executer.eventController.InvokeEvent("Slide 28"); 
                }
            }
            else
            {
                if (upMostPosition.y > startLocation.y) executer.eventController.InvokeEvent("Slide 82"); 
                else executer.eventController.InvokeEvent("Slide 28");
            }
            
        }
        else
        {
            if (finishLocation.x > startLocation.x)
            {
                if ((finishLocation - rightMostPosition).magnitude < gestureDenoiseThreshold
                    && (startLocation - leftMostPosition).magnitude < gestureDenoiseThreshold)
                    executer.eventController.InvokeEvent("Slide 6");
                else
                {
                    if (rightMostPosition.x > finishLocation.x) executer.eventController.InvokeEvent("Slide 64");
                    else if (leftMostPosition.x < startLocation.x) executer.eventController.InvokeEvent("Slide 46");
                }
            }
            else if (finishLocation.x < startLocation.x)
            {
                if ((finishLocation - leftMostPosition).magnitude < gestureDenoiseThreshold
                    && (startLocation - rightMostPosition).magnitude < gestureDenoiseThreshold)
                    executer.eventController.InvokeEvent("Slide 4");
                else
                {
                    if (rightMostPosition.x > startLocation.x) executer.eventController.InvokeEvent("Slide 64");
                    else if (leftMostPosition.x < finishLocation.x) executer.eventController.InvokeEvent("Slide 46");
                }
            }
            else
            {
                if (rightMostPosition.x > startLocation.x) executer.eventController.InvokeEvent("Slide 64");
                else executer.eventController.InvokeEvent("Slide 46");
            }
        }
    }

    [Button(ButtonSizes.Medium)]
    [GUIColor(0.6f,1f,0.6f)]
    private void RestoreDefaultValue()
    {
        tapFinishInterval = 0.2f;
        minimumGestureDistance = 10;
        gestureDenoiseThreshold = 15;
    }
}
