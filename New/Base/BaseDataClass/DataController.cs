using Sirenix.OdinInspector;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;



namespace Nino.NewStateMatching
{
    [InlineEditor]
    public abstract class DataController<Item,ItemCollection> : DataScriptableObject 
        where Item: NewStateMatching.Item
        where ItemCollection: NewStateMatching.Collection<Item>
    {
        [ReadOnly,LabelWidth(80),PropertyOrder(-101)]public string dataType;
        [FoldoutGroup("Hint",Order = -99),TextArea(minLines:5,maxLines:20),SerializeField] string hint;
        [FoldoutGroup("Note",Order =-98), TextArea(minLines: 5, maxLines: 20), SerializeField] string note;
        [FoldoutGroup("Data",Order=-97),PropertyOrder(-100),PropertySpace(SpaceAfter = 20, SpaceBefore = 10)]public ItemCollection collection;


        [FoldoutGroup("Data Output Reference",Order = -96)] public Dictionary<string, Item> dic_items;
        [FoldoutGroup("Data Output Reference")] public Dictionary<string, List<Item>> dic_groups;
        [FoldoutGroup("Data Output Reference")] public Dictionary<string, List<Item>> dic_tags;
        [FoldoutGroup("Data Output Reference"),Button(ButtonSizes.Large),GUIColor(0.4f,1,0.4f)] void UpdateReferences()
        {
            collection.RemoveRedundantTagsInAllItems();
            dic_items = new Dictionary<string, Item>();
            dic_groups = new Dictionary<string, List<Item>>();
            dic_tags = new Dictionary<string, List<Item>>();
            foreach(Item item in collection.items)
            {
                if (!dic_items.Keys.Contains(item.itemName)) dic_items.Add(item.itemName, item);
                if (!dic_groups.Keys.Contains(item.group)) dic_groups.Add(item.group, new List<Item>());
                dic_groups[item.group].Add(item);
                foreach(string tag in item.tags)
                {
                    if (!dic_tags.Keys.Contains(tag)) dic_tags.Add(tag, new List<Item>());
                    dic_tags[tag].Add(item);
                }
            }
        }


        [FoldoutGroup("Save",Order = -95), FolderPath(RequireExistingPath = true), LabelWidth(100),SerializeField , PropertySpace(SpaceBefore = 10)] string savedPath = "Assets";
        [FoldoutGroup("Save"),Button(Style = ButtonStyle.Box,ButtonHeight = 40),GUIColor(0.4f,1,0.4f), PropertySpace(SpaceAfter = 20)] public void SaveDataToFolder()
        {
            string rootPath = savedPath;
            if (!AssetDatabase.Contains(this))
            {
                if (AssetUtility.CreateFolder(rootPath, dataType)) rootPath = rootPath + "/" + dataType;
                else throw new Exception("Please give a valid address");
            }
            if(!AssetUtility.SaveAsset(this, rootPath + "/" + dataType + "_Controller.asset"))
            {
                rootPath = AssetDatabase.GetAssetPath(this);
                rootPath = System.IO.Path.GetDirectoryName(rootPath);
            }
            AssetUtility.SaveAsset(collection, rootPath + "/" + dataType + "_DataCollection.asset");
            if (AssetUtility.CreateFolder(rootPath, "Datas")) rootPath = rootPath + "/Datas";
            else throw new Exception("Path Error");
            foreach (Item i in collection.items)
            {
                AssetUtility.SaveAsset(i, rootPath + "/" + i.itemName + ".asset");
            }
            savedPath = AssetDatabase.GetAssetPath(this);
            UnityEditor.EditorGUIUtility.PingObject(this);
            savedPath = System.IO.Path.GetDirectoryName(rootPath);
            
        }


        protected override void InitializeBaseScriptableObject()
        {
            dataType = WriteDataType();
            string BasicHint = "\n\n______________________\n" +
                "\nDataController Explain:" +
                "\nThis scriptable object hold " + dataType + " type of data." +
                "\nYou can |create|edit|remove| data in this section." +
                "\n\nDataController Sections:" +
                "\nNote: This section will not compail, It's only for you to take note." +
                "\n\nData: Section to edit data." +
                "\nData_Items: Section to edit all data" +
                "\n\nData_Group and Tags_Items: Different find methods will put different items in temp list, you can edit items in temp list. " +
                "\nData_Group and Tags_Groups || Tags: The place to remove or rename groups or tags. You can get basic information about groups or tags in QuickLook." +
                "\n\nData Output Reference: Dictionaries hold references to data and are sorted by item name, item group, and item tags. This is a helper section for accessing the data. You could also edit data at this section. It is important to update references at this section after change." +
                "\n\nSave: The place to save data to the folder. If data is already saved to the folder this will save instance changes to the scriptable object in the folder.";
            hint = WriteHint() + BasicHint;
            note = "";
            collection = ScriptableObject.CreateInstance<ItemCollection>();
            dic_items = new Dictionary<string, Item>();
            dic_groups = new Dictionary<string, List<Item>>();
            dic_tags = new Dictionary<string, List<Item>>();
        }
        protected abstract string WriteHint();
        protected abstract string WriteDataType();

        
    }
}

