using UnityEngine;

namespace StateMatching.Variable
{
    public class QuaternionExtension : VariableExtension<QuaternionExecuter>
    {
        public QuaternionExtension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {

        }
    }
}

