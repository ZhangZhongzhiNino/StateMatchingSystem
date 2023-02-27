﻿using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public abstract class VariableExecuter<Item, DataController>
        : SMSExecuter, IDataExecuter<Item, DataController>
        where Item : NewStateMatching.Item, new()
        where DataController : NewStateMatching.DataController<Item>
        
    {
        
        public DataController _datacontroller;
        public DataController dataController { get => _datacontroller; set => _datacontroller = value; }

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

