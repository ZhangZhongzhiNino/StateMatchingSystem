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
        #region Prefabs Folder Path
        [FoldoutGroup("Prefab Folder Path")]
        [FolderPath]
        public string prefabFolder = "Assets/Nino/StateMatchingSystem/StateMatchingComponents/Input/InputPresets/Prefabs";
        [FoldoutGroup("Prefab Folder Path")]
        [Button(ButtonSizes.Large), GUIColor(1, 1, 0.4f)]
        void RestoreFolderPath()
        {
            prefabFolder = root.rootReferences.folderPathManager.GetPath("Input Prefabs");
        }
        #endregion
        #region Prefabs
        [FoldoutGroup("Prefabs")] 
        public List<GameObject> presets = new List<GameObject>();
        [FoldoutGroup("Prefabs")]
        public bool autoUpdate = true;
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
        #endregion
        #region Create Preset
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
            if (root.rootReferences.inputCategory.inputSystemContainerExtension.executer == null) root.rootReferences.inputCategory.inputSystemContainerExtension.CreateExtension();
            if (root.rootReferences.inputCategory.inputSystemContainerExtension.executer.Contain(prefabToAdd.name) )return;
            GameObject newObj = GameObject.Instantiate(selectedPreset);
            newObj.transform.SetParent(root.rootReferences.inputCategory.inputSystemContainerExtension.executer.transform);
            GeneralUtility.ResetLocalTransform(newObj);
            newObj.name = prefabToAdd.name;
        }
        #endregion

        public override void EditModeUpdateCalls()
        {
            if (autoUpdate) UpdatePresets();
        }

        public override string GetActionGroupName()
        {
            return "Input Preset";
        }

        public override void InitiateActions()
        {
            
        }
        
    }
}

