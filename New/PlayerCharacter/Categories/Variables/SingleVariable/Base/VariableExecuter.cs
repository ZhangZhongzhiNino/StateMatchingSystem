using UnityEngine;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class VariableExecuter<Item, Collection, DataController>
        : SMSExecuter, IDataExecuter<Item, Collection, DataController>
        where Item : NewStateMatching.Item
        where Collection : NewStateMatching.Collection<Item>
        where DataController : NewStateMatching.DataController<Item, Collection>
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

