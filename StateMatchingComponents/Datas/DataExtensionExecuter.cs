using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Data
{
    public class DataExtensionExecuter : ExtensionExecuter
    {
        public override CategoryController getCategory()
        {
            return root.dataCategory;
        }
    }
}

