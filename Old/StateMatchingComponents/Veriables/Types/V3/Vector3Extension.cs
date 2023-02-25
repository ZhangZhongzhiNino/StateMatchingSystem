using UnityEngine;

namespace Nino.StateMatching.Variable
{
    public class Vector3Extension : VariableExtension<Vector3ExtensionExecuter>
    {
        public Vector3Extension(string _extensionName, GameObject _controller, StateMatchingRoot _root) : base(_extensionName, _controller, _root)
        {
            
        }
    }
}

