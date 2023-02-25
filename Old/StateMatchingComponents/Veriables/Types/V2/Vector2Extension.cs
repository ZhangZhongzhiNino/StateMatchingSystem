using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class Vector2Extension : VariableExtension<Vector2ExtensionExecuter>
    {
        public Vector2Extension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {

        }
    }
}

