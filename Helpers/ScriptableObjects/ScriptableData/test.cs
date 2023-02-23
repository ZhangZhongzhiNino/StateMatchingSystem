using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


namespace Nino.StateMatching.Helper.Data
{
    public class test : IDataExecuter<NewDataController,NewItem,NewCollection>
    {
        public NewDataController datasC;
        [Button,GUIColor(0.4f,1,0.4f)]
        void CreateData()
        {
            dataController = ScriptableObject.CreateInstance<NewDataController>();
            dataController.dataType = "test data type";
        }
    }
    public class NewItem : Item
    {
        public float f;
        public string s;
        public Quaternion q;

        protected override void AssignItem(Item newItem)
        {
            NewItem getItem = (NewItem)newItem;
            f = getItem.f;
            s = getItem.s;
            q = getItem.q;
        }

        protected override void InitializeInstance()
        {
            f = 0;
            s = "abc";
            q = Quaternion.identity;
        }
    }
    public class NewCollection : ItemCollection<NewItem>
    {
        protected override void InitializeInstance()
        {
            
        }
    }
    public class NewDataController : DataController<NewItem, NewCollection>
    {
        protected override void InitializeInstance()
        {
            
        }

        protected override string WriteHint()
        {
            return "This is a test data controller \nhave fun! \n\nSome hint here";
        }
        [Button,GUIColor(0.3f,0.4f,0.5f)]
        void AddItem(string ItemName)
        {
            collection.AddItem(ItemName);
        }


    }
    
    
}

