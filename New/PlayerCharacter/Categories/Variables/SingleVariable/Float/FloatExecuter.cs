using Sirenix.OdinInspector;
using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class FloatExecuter : SMSExecuter
    {
        protected override void InitializeInstance()
        {
            if(dataController == null) dataController = ScriptableObject.CreateInstance<FloatDataController>();
            if(actionController == null) actionController = new FloatActionController(this);
        }

        protected override void PreRemoveInstance()
        {
            
        }

        protected override string WriteLocalAddress()
        {
            return "Float";
        }
    }
}

