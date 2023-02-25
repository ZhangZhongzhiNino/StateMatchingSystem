using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Nino.StateMatching.Helper
{
    [CreateAssetMenu(fileName = "Folder Path", menuName = "State Matching System/Folder Path")]
    public class FolderPath : ScriptableObject
    {
        public string folderName;
        public string relativePath;
        [FolderPath] public string absPath;
        [FolderPath] public string rootPath;

        [Button]
        void CalculateRelativePath()
        {
            relativePath = absPath.Replace(rootPath + "/", "");
        }
    }
}

