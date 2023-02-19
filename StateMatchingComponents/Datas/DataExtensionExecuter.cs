using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;

namespace Nino.StateMatching.Data
{
    public abstract class DataExtensionExecuter : ExtensionExecuter
    {
        public override CategoryController GetCategory()
        {
            return root.dataCategory;
        }
    }
}

