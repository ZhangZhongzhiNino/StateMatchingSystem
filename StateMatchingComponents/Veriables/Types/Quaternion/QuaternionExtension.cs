using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class QuaternionExtension : VariableExtension<QuaternionExtensionExecuter>
    {
        public QuaternionExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {

        }
    }
}

