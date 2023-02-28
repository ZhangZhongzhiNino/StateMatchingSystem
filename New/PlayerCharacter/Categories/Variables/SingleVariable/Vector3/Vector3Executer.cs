using Sirenix.OdinInspector;
using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3Executer : SMSExecuter
    {
        protected override void InitializeInstance()
        {
            if(dataController == null) dataController = ScriptableObject.CreateInstance<Vector3DataController>();
            if(actionController == null) actionController = new Vector3ActionController(this);
            if (compareController == null) compareController = new Vector3CompareController(this);
        }

        protected override void PreRemoveInstance()
        {

        }
        protected override string WriteLocalAddress()
        {
            return "Vector3";
        }
    }
}

