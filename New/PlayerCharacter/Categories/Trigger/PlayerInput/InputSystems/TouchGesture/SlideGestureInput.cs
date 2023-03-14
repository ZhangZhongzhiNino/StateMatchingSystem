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
        for (int i = 0; i < 2; i++)
        {
            int value = i;
            OnfingerAction[value] += delegate { onGestureStart(value); };
            FingerMoveActions[value] += delegate { onGestureMove(value); };
            PreFingerLeaveActions[value] += delegate { onGestureFinish(value); };
        }
    }
    private void OnEnable()
    {
        if (executer == null) executer = GetComponent<InputExecuter_TouchGesture>();
        if (inputReader == null) inputReader = gameObject.GetComponent<InputReader>();
        for (int i = 0; i < 2; i++)
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

        Vector2 difference = new Vector2(0, 0);
        difference.x = rightMostPosition.x - leftMostPosition.x;
        difference.y = upMostPosition.y - downMostPosition.y;
        if (difference.magnitude < minimumGestureDistance) return;
        GestureCalculator(difference, fingerIndex);
    }

    void GestureCalculator(Vector2 difference, int fingerIndex)
    {
        UnityEvent Slide2 = executer.dataController.GetItem("Slide 2").value as UnityEvent;
        UnityEvent Slide4 = executer.dataController.GetItem("Slide 4").value as UnityEvent;
        UnityEvent Slide6 = executer.dataController.GetItem("Slide 6").value as UnityEvent;
        UnityEvent Slide8 = executer.dataController.GetItem("Slide 8").value as UnityEvent;
        UnityEvent Slide28 = executer.dataController.GetItem("Slide 28").value as UnityEvent;
        UnityEvent Slide82 = executer.dataController.GetItem("Slide 82").value as UnityEvent;
        UnityEvent Slide46 = executer.dataController.GetItem("Slide 46").value as UnityEvent;
        UnityEvent Slide64 = executer.dataController.GetItem("Slide 64").value as UnityEvent;
        Vector2 finishLocation = inputReader.fingerLocation[fingerIndex];
        Vector2 startLocation = inputReader.fingerStartLocation[fingerIndex];
        if (Mathf.Abs(difference.y) > Mathf.Abs(difference.x))
        {
            if (finishLocation.y > startLocation.y)
            {
                if ((finishLocation - upMostPosition).magnitude < gestureDenoiseThreshold
                    && (startLocation - downMostPosition).magnitude < gestureDenoiseThreshold)
                    Slide8.Invoke();
                else
                {
                    if (upMostPosition.y > finishLocation.y) Slide82.Invoke();
                    else if (downMostPosition.y < startLocation.y) Slide28.Invoke();
                }
            }
            else if (finishLocation.y < startLocation.y)
            {
                if ((finishLocation - downMostPosition).magnitude < gestureDenoiseThreshold
                    && (startLocation - upMostPosition).magnitude < gestureDenoiseThreshold)
                    Slide2.Invoke();
                else
                {
                    if (upMostPosition.y > startLocation.y) Slide82.Invoke();
                    else if (downMostPosition.y < finishLocation.y) Slide28.Invoke();
                }
            }
            else
            {
                if (upMostPosition.y > startLocation.y) Slide82.Invoke();
                else Slide28.Invoke();
            }

        }
        else
        {
            if (finishLocation.x > startLocation.x)
            {
                if ((finishLocation - rightMostPosition).magnitude < gestureDenoiseThreshold
                    && (startLocation - leftMostPosition).magnitude < gestureDenoiseThreshold)
                    Slide6.Invoke();
                else
                {
                    if (rightMostPosition.x > finishLocation.x) Slide64.Invoke();
                    else if (leftMostPosition.x < startLocation.x) Slide46.Invoke();
                }
            }
            else if (finishLocation.x < startLocation.x)
            {
                if ((finishLocation - leftMostPosition).magnitude < gestureDenoiseThreshold
                    && (startLocation - rightMostPosition).magnitude < gestureDenoiseThreshold)
                    Slide4.Invoke();
                else
                {
                    if (rightMostPosition.x > startLocation.x) Slide64.Invoke();
                    else if (leftMostPosition.x < finishLocation.x) Slide46.Invoke();
                }
            }
            else
            {
                if (rightMostPosition.x > startLocation.x) Slide64.Invoke();
                else Slide46.Invoke();
            }
        }
    }

    [Button(ButtonSizes.Medium)]
    [GUIColor(0.6f, 1f, 0.6f)]
    private void RestoreDefaultValue()
    {
        tapFinishInterval = 0.2f;
        minimumGestureDistance = 10;
        gestureDenoiseThreshold = 15;
    }
}
