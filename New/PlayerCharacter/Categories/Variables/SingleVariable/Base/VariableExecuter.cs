using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public abstract class OldVariableExecuter<Item, DataController>
        : SMSExecuter
        where Item : NewStateMatching.Item, new()
        where DataController : NewStateMatching.DataController
    {
        public DataController _datacontroller;
        public DataController dataController { get => _datacontroller; set => _datacontroller = value; }
        protected override void InitializeDataController()
        {
            dataController = ScriptableObject.CreateInstance<DataController>();
        }

        protected override void PreRemoveDataControllers()
        {

        }
    }
}

