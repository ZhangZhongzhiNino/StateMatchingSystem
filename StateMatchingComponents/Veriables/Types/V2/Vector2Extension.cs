using UnityEngine;

namespace StateMatching.Variable
{
    public class Vector2Extension : VariableExtension<Vector2Executer>
    {
        public Vector2Extension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {

        }
    }
}

