using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.StateMatching.Helper
{
    public class EditModeUpdater : MonoBehaviour
    {
        public bool autoUpdate = true;
        public delegate void updateFunctions();
        public updateFunctions call;
        private void Start() { }
        private void OnDrawGizmos()
        {
            if (!autoUpdate) return;
            if(call != null)call();
        }
        public bool Contain (System.Action action)
        {
            if (call == null) return false;
            return call.GetInvocationList().Contains(action);
        }
    }
}


