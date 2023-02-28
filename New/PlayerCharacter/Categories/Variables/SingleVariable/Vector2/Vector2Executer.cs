using Sirenix.OdinInspector;
using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector2Executer : SMSExecuter
    {
        protected override void InitializeInstance()
        {
            if(dataController == null) dataController = ScriptableObject.CreateInstance<Vector2DataController>();
            if(actionController == null) actionController = new Vector2ActionController(this);
        }

        protected override void PreRemoveInstance()
        {

        }
        protected override string WriteLocalAddress()
        {
            return "Vector2";
        }
    }
}

