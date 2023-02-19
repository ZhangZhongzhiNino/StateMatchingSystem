using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nino.StateMatching.Helper
{
    public interface IExtension
    {
        public void Initiate(string _extensionName, GameObject _controller, StateMatchingRoot _root);
    }
}

