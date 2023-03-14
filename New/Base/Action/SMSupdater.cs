using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace Nino.NewStateMatching
{
    public class SMSupdater : StateMatchingMonoBehaviour
    {
        public UnityEvent actionUpdate;
        public float updateInterval;
        public bool updateOn;
        public UnityEvent timerUpdate;
        public float timerInterval;
        public bool timerOn;
        private void Start()
        {
            
        }
        private void OnEnable()
        {
            StartCoroutine(SMSTimerInvoke());
        }
        private void OnDisable()
        {
            StopAllCoroutines();
        }
        public IEnumerator SMSTimerInvoke()
        {
            while (true)
            {
                if(timerOn) timerUpdate?.Invoke();
                yield return new WaitForSeconds(timerInterval);
            }
        }
        public IEnumerator SMSUpdateInvoke()
        {
            while (true)
            {
                if (updateOn) actionUpdate?.Invoke();
                yield return new WaitForSeconds(updateInterval);
            }
        }
        public override void Initialize()
        {
            timerInterval = 0.2f;
            timerOn = true;
            updateInterval = 0.2f;
            updateOn = true;
        }

        public override void Remove()
        {
            
        }
    }
}

