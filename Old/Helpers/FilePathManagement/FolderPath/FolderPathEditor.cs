using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using System.IO;
namespace Nino.StateMatching.Helper
{
    [CustomEditor(typeof(FolderPath))]
    public class FolderPathEditor: OdinEditor
    {
        private void OnEnable()
        {
            string iconPath =  Path.Combine(Path.GetDirectoryName(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this))), "Icon.png");
            Debug.Log(iconPath);
            Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>(iconPath);
            EditorGUIUtility.SetIconForObject(target, icon);
        }
    }
}

