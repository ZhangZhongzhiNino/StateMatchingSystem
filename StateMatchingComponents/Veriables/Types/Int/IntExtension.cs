using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class IntExtension : VariableExtension<IntExtensionExecuter>
    {
        public IntExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {

        }
    }
}

