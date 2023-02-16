using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
public class JoysticController : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [BoxGroup("Reference")][field: SerializeField] RectTransform joysticRectTransform;
    [BoxGroup("Reference")][field: SerializeField] Animator joysticAnimator;
    [BoxGroup("Input Values")][field: SerializeField] Vector2 _joysticSize;
    [BoxGroup("Input Values")][field: SerializeField] float animationFadeTime;
    public Vector2 joysticSize
    {
        get { return _joysticSize; }
        set
        {
            _joysticSize = value;
            joysticRectTransform.sizeDelta = joysticSize;
        }
    }
    [BoxGroup("Output Values")]public bool pointerInCircle;
    
    private bool firstEnter;
    string defaultState;
    string exitState;
    string enterState;
    private void Awake()
    {
        joysticRectTransform.sizeDelta = joysticSize;
        firstEnter = true;
        pointerInCircle = false;
        defaultState = "JoysticDefaultState";
        exitState = "PointerOutCircle";
        enterState = "PointerInCircle";
    }
    void Start() { }
    public void createIcon(Vector2 touchPos)
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(touchPos.x, touchPos.y, 0);
    }

    public void destroyIcon()
    {
        joysticAnimator.Play(defaultState);
        pointerInCircle = false;
        firstEnter = true;
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerInCircle = true;
        if (firstEnter)
        {
            firstEnter = false;
            return;
        }
        joysticAnimator.CrossFadeInFixedTime(enterState, animationFadeTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerInCircle = false;
        joysticAnimator.CrossFadeInFixedTime(exitState, animationFadeTime);
    }
}
