using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Nino.NewStateMatching.PlayerCharacter;

namespace Nino.NewStateMatching
{
    public class ValueUpdater : SerializedMonoBehaviour
    {
        public List<ActionInput> dynamicInputs;
        public SMSPlayerCharacterRoot root;
        public void FindAllDynamicInputs()
        {

        }
        public void OnEnable()
        {
            FindAllDynamicInputs();
        }
        private void Update()
        {
            foreach(ActionInput input in dynamicInputs)
            {
                input.ReadData();
            }
        }
    }

}
