using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class StringExtension : VariableExtension<StringExtensionExecuter>
    {
        public StringExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {

        }
    }
}

