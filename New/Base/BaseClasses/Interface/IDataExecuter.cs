using Sirenix.OdinInspector;
namespace Nino.NewStateMatching
{
    public interface IDataExecuter<Item, ItemCollection,DataController >
        where Item : NewStateMatching.Item
        where ItemCollection : Collection<Item>
        where DataController : DataController<Item, ItemCollection>
    {
        public DataController dataController { get; set; }
        
    }
}

 