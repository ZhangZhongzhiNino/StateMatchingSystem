using Sirenix.OdinInspector;
using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntExecuter : SMSExecuter
    {
        protected override void InitializeInstance()
        {
            if (dataController == null) dataController = ScriptableObject.CreateInstance<IntDataController>();
            if (actionController == null) actionController = new IntActionController(this);
            if (compareController == null) compareController = new IntCompareController(this);
        }

        protected override void PreRemoveInstance()
        {

        }
        protected override string WriteLocalAddress()
        {
            return "Int";
        }
    }
}

