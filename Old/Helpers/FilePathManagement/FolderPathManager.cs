using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using System.IO;
namespace Nino.StateMatching.Helper
{
    public class FolderPathManager : MonoBehaviour
    {
        [FolderPath] public string RootPath = "Asset/Nino/StateMatchingSystem";
        [InlineEditor]public FolderPathList folderPathList;

        public string GetPath(string name)
        {
            foreach(FolderPath p in folderPathList.pathes)
            {
                if(p.folderName == name)
                {
                    return Path.Combine(RootPath, p.relativePath);
                }
            }
            return null;
        }
    }

}
