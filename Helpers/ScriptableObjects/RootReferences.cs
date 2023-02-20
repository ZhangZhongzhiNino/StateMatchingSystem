using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using Nino.StateMatching.Data;
using Nino.StateMatching.Action;
using Nino.StateMatching.Helper;
using Nino.StateMatching.Input;
using Nino.StateMatching.StateMachine;
using Nino.StateMatching.Variable;
using Nino.StateMatching.InternalEvent;

namespace Nino.StateMatching.Helper.Data
{
    public class RootReferences : ScriptableObject
    {
        [FoldoutGroup("State Matching Reference")]
        [TitleGroup("State Matching Reference/Game Object")] public GameObject inputObj;
        [TitleGroup("State Matching Reference/Game Object")] public GameObject internalEventObj;
        [TitleGroup("State Matching Reference/Game Object")] public GameObject variableObj;
        [TitleGroup("State Matching Reference/Game Object")] public GameObject dataObj;
        [TitleGroup("State Matching Reference/Game Object")] public GameObject actionObj;
        [TitleGroup("State Matching Reference/Game Object")] public GameObject stateMachineObj;
        [TitleGroup("State Matching Reference/Script Reference")] public InputCategory inputCategory;
        [TitleGroup("State Matching Reference/Script Reference")] public InternalEventCategory internalEventCategory;
        [TitleGroup("State Matching Reference/Script Reference")] public VariableCategory variableCategory;
        [TitleGroup("State Matching Reference/Script Reference")] public DataCategory dataCategory;
        [TitleGroup("State Matching Reference/Script Reference")] public ActionCategory actionCategory;
        [TitleGroup("State Matching Reference/Script Reference")] public StateMachineCategory stateMachineCategory;
        [TitleGroup("State Matching Reference/Other Reference")] public ActionRoot actionRoot;
        [TitleGroup("State Matching Reference/Other Reference")] public EditModeUpdater editModeUpdater;
        [TitleGroup("State Matching Reference/Other Reference")] public FolderPathManager folderPathManager;
    }
}

