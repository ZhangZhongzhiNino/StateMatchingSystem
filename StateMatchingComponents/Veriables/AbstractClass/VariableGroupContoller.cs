using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;
using System;

namespace StateMatching.Variable
{
    public abstract class VariableGroupContoller<T, V> : StructGroupController<T, V> where T : MonoBehaviour, IGroupItem<T, V> where V : struct
    {

    }

}
