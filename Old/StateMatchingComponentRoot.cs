using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using Nino.StateMatching.Helper.New;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching
{
    public class StateMatchingComponentRoot : MonoBehaviour
    {
        [Button(ButtonSizes.Large), GUIColor(0.4f, 1, 1), PropertyOrder(-99999999999)]
        void ResetHierarchy()
        {
            EditorUtility.ResetHierachy(transform.parent.gameObject);
        }
        [InlineEditor] public RootReferences rootReferences;
    }
}

