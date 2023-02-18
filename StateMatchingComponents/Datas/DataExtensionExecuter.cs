using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.Data
{
    public class DataExtensionExecuter : ExtensionExecuter
    {
        public override CategoryController getCategory()
        {
            return root.dataController;
        }
    }
}

