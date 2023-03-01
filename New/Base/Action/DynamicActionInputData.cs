using UnityEngine.Events;
using System.Collections.Generic;
namespace Nino.NewStateMatching
{
    public abstract class DynamicActionInputData
    {
        public string dataName;
        public abstract object value { get; set; }
        public DynamicActionInputData(string dataName)
        {
            this.dataName = dataName;
        }
        public T GetValue<T>()
        {
            if (typeof(T) == GetValueType()) return (T)value;
            else return default(T);
        }
        public abstract System.Type GetValueType();
        public UnityEvent OnValueChange;
        public void RemoveListener()
        {
            OnValueChange.RemoveAllListeners();
        }
    }
    public class DynamicDataController
    {
        public List<DynamicActionInputData> dynamicDatas;
        public DynamicDataController()
        {
            dynamicDatas = new List<DynamicActionInputData>();
        }
        public void AssignValue(string dataName, object newValue)
        {
            DynamicActionInputData getData = dynamicDatas.Find(x => x.dataName == dataName);
            getData.value = newValue;
            getData.OnValueChange.Invoke();
        }
    }
}

