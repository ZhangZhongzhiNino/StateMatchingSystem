using UnityEngine;

namespace StateMatching.Variable
{
    public class IntExtension : VariableExtension<IntExecuter>
    {
        public IntExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {

        }
    }
}

