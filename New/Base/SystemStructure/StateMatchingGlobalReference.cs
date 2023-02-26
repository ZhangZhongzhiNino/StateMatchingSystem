using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
namespace Nino.NewStateMatching
{
    public class StateMatchingGlobalReference : StateMatchingMonoBehaviour
    {
        public List<StateMatchingRoot> references;
        public override void Initialize()
        {
            if(references == null) references = new List<StateMatchingRoot>();
        }
        public override void Remove()
        {
            GeneralUtility.RemoveGameObject(this.gameObject);
        }


        [PropertyOrder(-100), Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f)]
        void SaveAllScriptableObjectInstance()
        {
            AssetDatabase.SaveAssets();
        }
    }
}

