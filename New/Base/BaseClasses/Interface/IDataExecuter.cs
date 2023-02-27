using Sirenix.OdinInspector;
namespace Nino.NewStateMatching
{
    public interface IDataExecuter<Item,DataController>
        where Item : NewStateMatching.Item,new()
        where DataController : DataController<Item>
    {
        public DataController dataController { get; set; }
        
    }
}

 