using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nino.StateMatching.Helper
{
    [CreateAssetMenu(fileName ="Folder Path List",menuName = "State Matching System/Folder Path List")]
    public class FolderPathList : ScriptableObject
    {
        public List<FolderPath> pathes;
    }
}
