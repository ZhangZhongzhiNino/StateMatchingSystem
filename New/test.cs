using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sirenix.OdinInspector;


namespace Nino.NewStateMatching
{
    public class test : DataExecuter, IDataExecuter<NewDataController,NewItem,NewCollection>
    {

        [ShowInInspector] public NewDataController dataController { get; set; }

        protected override void PreRemoveDataControllers()
        {
            
        }

        protected override void InitializeDataControllers()
        {
            dataController = ScriptableObject.CreateInstance<NewDataController>();
        }

        protected override void ResetHierarchy()
        {

        }

        public override void Refresh()
        {
            throw new NotImplementedException();
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
    public class NewCollection : Collection<NewItem>
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

        protected override string WriteDataType()
        {
            return "NewData";
        }

        protected override string WriteHint()
        {
            return "This is a test data controller \nhave fun! \n\nSome hint here";
        }


    }
    
    
}

