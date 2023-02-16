using UnityEngine;

namespace StateMatching.Variable
{
    public class FloatExtension : VariableExtension<FloatExecuter>
    {
        public FloatExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {

        }
    }
}

