using Sirenix.OdinInspector;
using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class Vector2Executer : VariableExecuter<Vector2,Vector2Value,Vector2DataController>
    {

        protected override void InitilizeActionContainer()
        {

        }
        protected override string WriteLocalAddress()
        {
            return "Vector2";
        }
    }
}

