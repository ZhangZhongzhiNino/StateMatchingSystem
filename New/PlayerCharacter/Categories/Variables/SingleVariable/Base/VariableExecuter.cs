using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public abstract class VariableExecuter<T,V, DataController>
        : SMSExecuter
        where V: VariableValue<T>,new()
        where DataController : VariableDataController<T,V>
        
    {
        protected override void InitializeInstance()
        {
            dataController = ScriptableObject.CreateInstance<DataController>();
            InitilizeActionContainer();
        }
        protected abstract void InitilizeActionContainer();

        protected override void PreRemoveInstance()
        {

        }
    }
}

