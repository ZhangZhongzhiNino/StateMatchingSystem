using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using System;
using StateMatching.Helper;
public class SlideGestureInput : MonoBehaviour,IEventContainer
{
    [BoxGroup("Reference")][Required][field: SerializeField] InputControl inputControl;

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
        for(int i = 0; i < 2; i++)
        {
            inputControl.TapEvent[i].AddListener(OnTap);
            inputControl.OnFinger[i].AddListener(OnfingerAction[i]);
            inputControl.FingerMove[i].AddListener(FingerMoveActions[i]);
            inputControl.PreFingerLeave[i].AddListener(PreFingerLeaveActions[i]);
        }

        
    }
    private void OnDisable()
    {
        for (int i = 0; i < 2; i++)
        {
            inputControl.TapEvent[i].RemoveListener(OnTap);
            inputControl.OnFinger[i].RemoveListener(OnfingerAction[i]);
            inputControl.FingerMove[i].RemoveListener(FingerMoveActions[i]);
            inputControl.PreFingerLeave[i].RemoveListener(PreFingerLeaveActions[i]);
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
        if (inputControl.fingerRule[fingerIndex] != 1) return;
        upMostPosition = inputControl.fingerStartLocation[fingerIndex];
        downMostPosition = inputControl.fingerStartLocation[fingerIndex];
        leftMostPosition = inputControl.fingerStartLocation[fingerIndex]; 
        rightMostPosition = inputControl.fingerStartLocation[fingerIndex];
    }
    void onGestureMove(int fingerIndex)
    {
        if (inputControl.fingerRule[fingerIndex] != 1) return;
        if (inputControl.fingerLocation[fingerIndex].y > upMostPosition.y) upMostPosition = inputControl.fingerLocation[fingerIndex];
        if (inputControl.fingerLocation[fingerIndex].y < downMostPosition.y) downMostPosition = inputControl.fingerLocation[fingerIndex];
        if (inputControl.fingerLocation[fingerIndex].x > rightMostPosition.x) rightMostPosition = inputControl.fingerLocation[fingerIndex];
        if (inputControl.fingerLocation[fingerIndex].x < leftMostPosition.x) leftMostPosition = inputControl.fingerLocation[fingerIndex];
    }
    void onGestureFinish(int fingerIndex)
    {
        if (inputControl.fingerRule[fingerIndex] != 1) return;
        
        Vector2 difference = new Vector2(0,0);
        difference.x = rightMostPosition.x - leftMostPosition.x;
        difference.y = upMostPosition.y - downMostPosition.y;
        if (difference.magnitude < minimumGestureDistance) return;
        GestureCalculator(difference, fingerIndex);
    }

    void GestureCalculator(Vector2 difference, int fingerIndex)
    {
        Vector2 finishLocation = inputControl.fingerLocation[fingerIndex];
        Vector2 startLocation = inputControl.fingerStartLocation[fingerIndex];
        if (Mathf.Abs(difference.y) > Mathf.Abs(difference.x))
        {
            if (finishLocation.y > startLocation.y)
            {
                if ((finishLocation - upMostPosition).magnitude < gestureDenoiseThreshold
                    && (startLocation - downMostPosition).magnitude < gestureDenoiseThreshold) Slide8?.Invoke();
                else
                {
                    if (upMostPosition.y > finishLocation.y) Slide82?.Invoke();
                    else if (downMostPosition.y < startLocation.y) Slide28?.Invoke();
                }
            }
            else if(finishLocation.y < startLocation.y)
            {
                if ((finishLocation - downMostPosition).magnitude < gestureDenoiseThreshold
                    && (startLocation - upMostPosition).magnitude < gestureDenoiseThreshold) Slide2?.Invoke();
                else
                {
                    if (upMostPosition.y > startLocation.y) Slide82?.Invoke();
                    else if (downMostPosition.y < finishLocation.y) Slide28?.Invoke();
                }
            }
            else
            {
                if (upMostPosition.y > startLocation.y) Slide82?.Invoke();
                else Slide28?.Invoke();
            }
            
        }
        else
        {
            if (finishLocation.x > startLocation.x)
            {
                if ((finishLocation - rightMostPosition).magnitude < gestureDenoiseThreshold
                    && (startLocation - leftMostPosition).magnitude < gestureDenoiseThreshold) Slide6?.Invoke();
                else
                {
                    if (rightMostPosition.x > finishLocation.x) Slide64?.Invoke();
                    else if (leftMostPosition.x < startLocation.x) Slide46?.Invoke();
                }
            }
            else if (finishLocation.x < startLocation.x)
            {
                if ((finishLocation - leftMostPosition).magnitude < gestureDenoiseThreshold
                    && (startLocation - rightMostPosition).magnitude < gestureDenoiseThreshold) Slide4?.Invoke();
                else
                {
                    if (rightMostPosition.x > startLocation.x) Slide64?.Invoke();
                    else if (leftMostPosition.x < finishLocation.x) Slide46?.Invoke();
                }
            }
            else
            {
                if (rightMostPosition.x > startLocation.x) Slide64?.Invoke();
                else Slide46?.Invoke();
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
