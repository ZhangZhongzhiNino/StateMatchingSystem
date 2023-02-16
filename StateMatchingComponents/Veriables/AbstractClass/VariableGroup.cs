using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.Variable
{
    public class VariableGroup<T, V> : StructGroup<T, V> where T : MonoBehaviour, IGroupItem<T,V> where V : struct
    {

    }
}

