using System.IO;
using UnityEditor;
using UnityEngine;


namespace Nino.NewStateMatching
{
    public static class AssetUtility
    {
        public static bool SaveAsset(UnityEngine.Object obj, string path)
        {
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path);

            if (!AssetDatabase.Contains(obj))
            {
                AssetDatabase.CreateAsset(obj, assetPathAndName);
                return true;
            }
            else
            {
                AssetDatabase.SaveAssetIfDirty(obj);
                return false;
            }
        }
        public static bool CreateFolder(string path, string folder)
        {
            Debug.Log(path);
            if (!Directory.Exists(path)) return false;
            if (!Directory.Exists(path + "/" + folder)) Directory.CreateDirectory(path + "/" + folder);
            return Directory.Exists(path + "/" + folder);
        }
    }

}
