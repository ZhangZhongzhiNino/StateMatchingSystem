using UnityEditor;
using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntExecuter : SMSExecuter
    {
        public IntData items;
        protected override void InitializeDataController()
        {
            if(items == null) items = ScriptableObject.CreateInstance<IntData>();
        }

        protected override void PreRemoveDataControllers()
        {
            
        }

        protected override string WriteLocalAddress()
        {
            return "Int";
        }
    }
}

