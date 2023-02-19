using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nino.StateMatching.Helper;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Nino.StateMatching.Input
{
    public class InputPresetExtensionExecuter : InputExtensionExecuter
    {
        #region Prefabs
        [FoldoutGroup("Prefabs")]
        [FolderPath]
        public string prefabFolder = "Assets/Nino/StateMatchingSystem/StateMatchingComponents/Input/InputPresets/Prefabs";
        [FoldoutGroup("Prefabs")] 
        public List<GameObject> presets = new List<GameObject>();
        [FoldoutGroup("Prefabs")]
        [Button]
        void UpdatePresets()
        {
            presets.RemoveAll(item => item == null);
            string[] guids = AssetDatabase.FindAssets("t:prefab", new string[] { prefabFolder });
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if(!presets.Contains(go)) presets.Add(go);
            }
        }
        private void OnValidate()
        {
            UpdatePresets();
        }
        #endregion
        List<string> presetsName
        {
            get
            {
                if (presets == null || presets.Count == 0) return null;
                List<string> nameList = new List<string>();
                foreach (GameObject obj in presets)
                {
                    nameList.Add(obj.name);
                }
                return nameList;
            }
        }
        GameObject selectedPreset
        {
            get
            {
                if (string.IsNullOrEmpty(selectPrefab)) return null; 
                foreach(GameObject obj in presets)
                {
                    if (obj.name == selectPrefab) return obj;
                }
                return null;
            }
        }

        [FoldoutGroup("Create Input System"),ValueDropdown("presetsName"),SerializeField]
        string selectPrefab;
        [FoldoutGroup("Create Input System")]
        [Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f)]
        void CreateInputSystem()
        {
            GameObject prefabToAdd = selectedPreset;
            if (root.inputCategory.inputSystemContainerExtension.executer == null) root.inputCategory.inputSystemContainerExtension.CreateExtension();
            if (root.inputCategory.inputSystemContainerExtension.executer.Contain(prefabToAdd.name) )return;
            GameObject newObj = GameObject.Instantiate(selectedPreset);
            newObj.transform.SetParent(root.inputCategory.inputSystemContainerExtension.executer.transform);
            GeneralUtility.ResetLocalTransform(newObj);
            newObj.name = prefabToAdd.name;
        }
    }
}
