using Sirenix.OdinInspector;
using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class StringExecuter : SMSExecuter
    {
        protected override void InitializeInstance()
        {
            if(dataController == null) dataController = ScriptableObject.CreateInstance<StringDataController>();
            if(actionController == null) actionController = new StringActionController(this);
            if (compareController == null) compareController = new StringCompareController(this);
        }

        protected override void PreRemoveInstance()
        {

        }
        protected override string WriteLocalAddress()
        {
            return "String";
        }
    }
}

