using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using System.IO;
namespace Nino.StateMatching.Helper
{
    [CustomEditor(typeof(FolderPathList))]
    public class FolerPathListEditor : OdinEditor
    {
        private void OnEnable()
        {
            string iconPath = Path.Combine(Path.GetDirectoryName(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this))), "Icon.png");
            Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>(iconPath);
            EditorGUIUtility.SetIconForObject(target, icon);
        }
    }
}

