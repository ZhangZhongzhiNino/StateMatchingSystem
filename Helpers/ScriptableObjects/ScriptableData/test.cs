using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.StateMatching.Helper.Data
{
    public class test : MonoBehaviour
    {
        public NewDataController dataController;
        [Button,GUIColor(0.4f,1,0.4f)]
        void CreateData()
        {
           dataController = ScriptableObject.CreateInstance<NewDataController>();
            dataController.dataType = "test data type";
        }
    }
    public class NewItem : Item
    {
        float f;
        string s;
        Quaternion q;
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
            return "This is a test data controller \n have fun! \n\n Some hint here";
        }
    }
}

