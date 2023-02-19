using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class FloatExtension : VariableExtension<FloatExtensionExecuter>
    {
        public FloatExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {

        }
    }
}

