using UnityEngine;

namespace StateMatching.Variable
{
    public class Vector3Extension : VariableExtension<Vector3Executer>
    {
        public Vector3Extension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {
            
        }
    }
}

