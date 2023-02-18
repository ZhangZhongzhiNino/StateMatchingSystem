using UnityEngine;

namespace StateMatching.Variable
{
    public class StringExtension : VariableExtension<StringExecuter>
    {
        public StringExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {

        }
    }
}

