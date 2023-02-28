using Sirenix.OdinInspector;
using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector3Executer : VariableExecuter<Vector3,Vector3Value,Vector3DataController>
    {
        protected override void InitilizeActionContainer()
        {

        }
        protected override string WriteLocalAddress()
        {
            return "Vector3";
        }
    }
}

