using Sirenix.OdinInspector;
using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class BoolExecuter : SMSExecuter
    {
        protected override void InitializeInstance()
        {
            if(dataController == null) dataController = ScriptableObject.CreateInstance<BoolDataController>();
            if(actionController == null) actionController = new BoolActionController(this);
        }

        protected override void PreRemoveInstance()
        {
            
        }

        protected override string WriteLocalAddress()
        {
            return "Bool";
        }
    }
}

