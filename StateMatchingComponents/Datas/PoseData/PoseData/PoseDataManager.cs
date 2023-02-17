using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Linq;

using StateMatching.Helper;

namespace StateMatching.Data
{
    public class PoseDataManager : GroupExtensionExecuter<PoseData,PoseData>
    {
        //--------------Inspector Interface--------
        #region Reference
        [FoldoutGroup("Reference", Order = -100)]
        [TitleGroup("Reference/Prefabs"), SerializeField] GameObject poseDataItemsPrefab;
        [TitleGroup("Reference/Prefabs"), SerializeField] GameObject importancePresetsHolderPrefab;


        [TitleGroup("Reference/Reference"), SerializeField] HumanoidInfoData infoController;
        [TitleGroup("Reference/Reference"), SerializeField] GameObject poseDataComponents;
        [TitleGroup("Reference/Reference"), SerializeField] GameObject poseDataGroups;
        [TitleGroup("Reference/Reference"), SerializeField] GameObject poseDataItems;
        [TitleGroup("Reference/Reference"), SerializeField] GameObject importancePresetsHolder;

        [TitleGroup("Reference/Data"), SerializeField] float positionDifferenceMultier = 200;
        [TitleGroup("Reference/Data"), SerializeField] float rotationDifferenceMultier = 1;
        [TitleGroup("Reference/Data"), SerializeField][ReadOnly] List<GameObject> bodyPartRoots;
        [TitleGroup("Reference/Data"), SerializeField][ReadOnly] List<BodyPartInfoHolder> bodyParts;
        [TitleGroup("Reference/Data"), SerializeField][ReadOnly]
        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "presetName")]
        List<ImportancePreset> importancePresets;
        #endregion

        #region 1 Create Pose
        [FoldoutGroup("Create Pose", Order = -99)]
        [Button(ButtonStyle.Box)]
        [GUIColor(0.4f, 1, 0.4f)]
        void CreateOrUpdatePose(string poseName = "new pose", int poseAtFrame = 0 , string poseInGroup = null)
        {
            PoseData newPose = CreatePoseInstance(poseName, poseAtFrame);
            if (string.IsNullOrWhiteSpace(poseInGroup)) groupController.AddItem(poseName, newPose);
            else groupController.AddItem(poseName, newPose, poseInGroup);

        }
        private PoseData CreatePoseInstance(string poseName, int poseAtFrame)
        {
            List<Vector3> partPositions = new List<Vector3>();
            List<Vector3> rootPositions = new List<Vector3>();
            List<Quaternion> rotations = new List<Quaternion>();
            List<string> names = new List<string>();

            for (int i = 0; i < bodyPartRoots.Count; i++)
            {
                if (!bodyParts[i].writeInPose) continue;
                Vector3 partLocalPosition = transform.InverseTransformPoint(bodyParts[i].transform.position);
                Vector3 rootLocalPosition = transform.InverseTransformPoint(bodyPartRoots[i].transform.position);
                Quaternion localRotation = Quaternion.Inverse(transform.rotation) * bodyPartRoots[i].transform.rotation;
                rotations.Add(localRotation);
                partPositions.Add(partLocalPosition);
                rootPositions.Add(rootLocalPosition);
                names.Add(bodyParts[i].GetName());

            }
            PoseData newPose = new PoseData();
            newPose.Initialize(names, partPositions, rootPositions, rotations, poseName, poseAtFrame);
            return newPose;

        }
        #endregion//1 Create Pose

        #region Edit Pose
        #region 1 Veriables
        List<string> poseNames
        {
            get
            {
                List<string> newPoseNames = new List<string>();
                foreach (PoseData p in groupController.items)
                {
                    string newName = "\"" + p.GetPoseName() + "\" at " + p.GetStartFrame() + " frame";
                    newPoseNames.Add(newName);
                }
                return newPoseNames;
            }
        }

        [BoxGroup("IF0/CompairDebug")]
        [FoldoutGroup("Edit Pose", Order = -98)]
        [TitleGroup("Edit Pose/Pose Preview")]
        [ShowIfGroup("Edit Pose/Pose Preview/poseNameSelectEnabled", Condition = "@editMode == 0")]
        [ValueDropdown("poseNames")]
        [field: SerializeField] string selectPose;

        [ShowIfGroup("Edit Pose/Pose Preview/poseNameSelectDisabled", Condition = "@editMode != 0")]
        [SerializeField][ReadOnly] string currentEditPose;
        int editMode = 0;

        string _selectedPoseName
        {
            get
            {
                if (selectPose == null) return null;
                string[] parts = selectPose.Split("\"");
                if (parts.Length >= 2)
                {
                    return parts[1];
                }
                else return null;
            }
        }
        int _selectedPoseAtFrame
        {
            get
            {
                if (selectPose == null) return -1;
                string[] parts = selectPose.Split("\"");
                if (parts.Length < 3) return -1;
                string[] parts2 = parts[2].Split(' ');
                if (parts2.Length < 3) return -1;
                return int.Parse(parts2[2]);
            }
        }

        PoseData _currentSelectPose { get { return GetPose(_selectedPoseName, _selectedPoseAtFrame); } }
        List<float> _currentSelectImportanceList { get { return _currentSelectPose.GetImportances(); } }

        #endregion// 1 Veriables

        #region 1 Pose Preview

        [TitleGroup("Edit Pose/Pose Preview")]
        [ShowIfGroup("Edit Pose/Pose Preview/apply Pose", Condition = "@selectPose != null && selectPose.Length != 0")]
        [Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f)]
        void ApplyPoseTransform()
        {
            if (_currentSelectPose == null) return;
            List<Quaternion> rotations = _currentSelectPose.GetRootRotations();
            List<Vector3> positions = _currentSelectPose.GetPartPositions();
            List<string> partNames = _currentSelectPose.GetPartNames();
            int size = partNames.Count;




            int i = 0;
            foreach (Quaternion rotation in _currentSelectPose.GetRootRotations())
            {
                Quaternion GlobalRotations = transform.rotation * rotation;
                bodyPartRoots[i].transform.rotation = GlobalRotations;
                i++;
            }
            i = 0;
            foreach (Vector3 position in _currentSelectPose.GetRootPositions())
            {
                Vector3 moveToLocation = this.transform.TransformPoint(position);
                bodyPartRoots[i].transform.position = moveToLocation;
                i++;
            }


        }

        void _ApplyTransform(PoseData pose)
        {
            if (pose == null) return;
            List<Quaternion> rotations = pose.GetRootRotations();
            List<Vector3> positions = pose.GetPartPositions();
            List<string> partNames = pose.GetPartNames();
            int size = partNames.Count;




            int i = 0;
            foreach (Quaternion rotation in pose.GetRootRotations())
            {
                Quaternion GlobalRotations = transform.rotation * rotation;
                bodyPartRoots[i].transform.rotation = GlobalRotations;
                i++;
            }
            i = 0;
            foreach (Vector3 position in pose.GetRootPositions())
            {
                Vector3 moveToLocation = this.transform.TransformPoint(position);
                bodyPartRoots[i].transform.position = moveToLocation;
                i++;
            }
        }
        #endregion//1 Pose Preview
        #region 1 Importance Edit
        [TitleGroup("Edit Pose/Edit Importance", GroupName = "@\"Edit importance of \"+ selectPose")]
        [ShowIf("Edit Pose/Edit Importance/edit", Condition = "@editMode == 0 && selectPose != null && selectPose.Length != 0")]
        [Button(Name = "Edit body part importance", ButtonHeight = 35), GUIColor(0.4f, 1, 0.4f)]
        void StartEditBodyPartImportance()
        {
            currentEditPose = selectPose;
            editMode = 1;
            List<float> currentImportance = _currentSelectPose.GetImportances();
            for (int i = 0; i < bodyParts.Count; i++)
            {
                bodyParts[i].StartImportanceEdit(currentImportance[i]);
            }
        }
        [TitleGroup("Edit Pose/Edit Importance")]
        [ShowIf("Edit Pose/Edit Importance/edit", Condition = "@editMode == 1")]
        [Button(Name = "End edit importance & apply change", ButtonHeight = 35), GUIColor(0.4f, 1f, 0.4f)]
        void EndEditBodyPartImportance()
        {
            editMode = 0;
            List<float> newImportance = new List<float>();
            for (int i = 0; i < bodyParts.Count; i++)
            {
                newImportance.Add(bodyParts[i].EndImportanceEdit());
            }
            _currentSelectPose.SetImportance(newImportance);
        }
        [TitleGroup("Edit Pose/Edit Importance")]
        [ShowIf("Edit Pose/Edit Importance/edit", Condition = "@editMode == 1")]
        [Button(Name = "End edit importance & revert change", ButtonHeight = 35), GUIColor(1f, 0.4f, 0.4f)]
        void CancleEditBodyPartImportance()
        {
            editMode = 0;
            for (int i = 0; i < bodyParts.Count; i++) { bodyParts[i].EndImportanceEdit(); }
        }
        #endregion//1 Importance Edit
        #region 1 Importance Preset
        List<string> importancePresetsNames;
        [ShowIfGroup("Edit Pose/Importance Preset", Condition = "@editMode == 1")]
        [TitleGroup("Edit Pose/Importance Preset/Importance Presets")]
        [ValueDropdown("importancePresetsNames"), SerializeField]
        string selectImportancePreset = null;
        ImportancePreset _currentSelectedImportancePreset { get { return GetImportancePreset(selectImportancePreset); } }

        void CreateImportancePresetAndAddToHolder(string presetName, List<float> importance, bool debugLog = true)
        {
            ImportancePreset preset = GetImportancePreset(presetName);
            if (preset == null)
            {
                ImportancePreset newPreset = importancePresetsHolder.AddComponent<ImportancePreset>();
                newPreset.Initialize(presetName, importance);
                importancePresets.Add(newPreset);
                if(debugLog)Debug.Log("New Preset: \"" + presetName + "\" Created");
            }
            else
            {
                preset.SetImportance(importance);
                if (debugLog) Debug.Log("Preset: \"" + presetName + "\" Updated");
            }
            UpdateImportaqncePresetList();
        }
        [TitleGroup("Reference/Data"), Button,GUIColor(0.4f,1,0.4f)]
        void UpdateImportaqncePresetList()
        {
            importancePresets = importancePresetsHolder.GetComponents<ImportancePreset>().ToList();
            List<string> names = new List<string>();
            foreach (ImportancePreset preset in importancePresets) names.Add(preset.presetName);
            importancePresetsNames = names;
        }
        [TitleGroup("Edit Pose/Importance Preset/Importance Presets")]
        [Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f)]
        void ApplyImportancePreset()
        {
            if (_currentSelectedImportancePreset == null) return;
            List<float> importance = _currentSelectedImportancePreset.GetImportance();
            for (int i = 0; i < bodyParts.Count; i++)
            {
                if (i < importance.Count) bodyParts[i].SetImportance(importance[i]);
                else bodyParts[i].SetImportance(0);
            }
        }
        [TitleGroup("Edit Pose/Importance Preset/Importance Presets")]
        [Button(ButtonStyle.Box)]
        [GUIColor(1, 1, 0.4f)]
        void CreateImportancePreset(string newPresetName)
        {
            CreateImportancePresetAndAddToHolder(newPresetName, _currentSelectImportanceList);
        }
        #endregion//1 Importance Preset


        #endregion//Edit Pose


        //--------------Inspector Interface End--------

        //--------------Functining group-----------

        #region initialize destory

        public override void Initialize<_T>(_T instance = null, StateMatchingRoot stateMatchingRoot = null)
        {
            base.Initialize(instance, stateMatchingRoot);
            InitializeVeriables();
            FindInfoController();
            CreateComponent(ref importancePresetsHolder, "ImportancePresetsHolder", this.transform, importancePresetsHolderPrefab);
            UpdateBodyPartsData();
            AddDefaultImportancePreset();
        }
        public override void PreDestroy()
        {
            EndEditMode();
            Helpers.RemoveGameObject(poseDataItems);
            Helpers.RemoveGameObject(poseDataGroups);
            Helpers.RemoveGameObject(importancePresetsHolder);
            Helpers.RemoveGameObject(poseDataComponents);
            base.PreDestroy();

        }
        void FindInfoController()
        {
            if(root.dataController.humanoidInfoDatas.extension == null)
            {
                root.dataController.humanoidInfoDatas.CreateExtension();
            }
            infoController = root.dataController.humanoidInfoDatas.extension;
            infoController.poseDataManager = this;
        }
        public void UpdateBodyPartsData()
        {
            bodyParts?.Clear();
            bodyParts = new List<BodyPartInfoHolder>();
            bodyPartRoots?.Clear();
            bodyPartRoots = new List<GameObject>();
            List<BodyPartInfoHolder> controllerParts = infoController.GetAllBodyParts();
            controllerParts.RemoveAll(item => item.writeInPose == false);
            foreach (BodyPartInfoHolder part in controllerParts)
            {
                if (!part.writeInPose) continue;
                bodyParts.Add(part);
                bodyPartRoots.Add(part.transform.parent.gameObject);
            }
        }
        void InitializeVeriables()
        {
            importancePresetsNames = new List<string>();
            importancePresets = new List<ImportancePreset>();
        }
        void AddDefaultImportancePreset()
        {
            List<float> preset_limbs = new List<float>();
            List<float> preset_arms = new List<float>();
            List<float> preset_lArm = new List<float>();
            List<float> preset_rArm = new List<float>();
            List<float> preset_Legs = new List<float>();
            List<float> preset_lLeg = new List<float>();
            List<float> preset_rLeg = new List<float>();
            List<float> preset_zero = new List<float>();
            List<float> preset_full = new List<float>();

            foreach (HumanBodyBones bone in Enum.GetValues(typeof(HumanBodyBones)))
            {
                string currentName = bone.ToString();
                if (currentName == "LeftEye") break;
                BodyPartInfoHolder currentPart = GetPart(currentName);
                if (currentPart == null) continue;
                #region Add importance float to lists
                preset_zero.Add(0);
                preset_full.Add(10);
                if (bone == HumanBodyBones.LeftShoulder)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(10);
                    preset_lArm.Add(10);
                    preset_rArm.Add(0);
                    preset_Legs.Add(0);
                    preset_lLeg.Add(0);
                    preset_rLeg.Add(0);
                }
                else if (bone == HumanBodyBones.LeftUpperArm)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(10);
                    preset_lArm.Add(10);
                    preset_rArm.Add(0);
                    preset_Legs.Add(0);
                    preset_lLeg.Add(0);
                    preset_rLeg.Add(0);
                }
                else if (bone == HumanBodyBones.LeftLowerArm)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(10);
                    preset_lArm.Add(10);
                    preset_rArm.Add(0);
                    preset_Legs.Add(0);
                    preset_lLeg.Add(0);
                    preset_rLeg.Add(0);
                }
                else if (bone == HumanBodyBones.LeftHand)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(10);
                    preset_lArm.Add(10);
                    preset_rArm.Add(0);
                    preset_Legs.Add(0);
                    preset_lLeg.Add(0);
                    preset_rLeg.Add(0);
                }
                else if (bone == HumanBodyBones.RightShoulder)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(10);
                    preset_lArm.Add(0);
                    preset_rArm.Add(10);
                    preset_Legs.Add(0);
                    preset_lLeg.Add(0);
                    preset_rLeg.Add(0);
                }
                else if (bone == HumanBodyBones.RightUpperArm)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(10);
                    preset_lArm.Add(0);
                    preset_rArm.Add(10);
                    preset_Legs.Add(0);
                    preset_lLeg.Add(0);
                    preset_rLeg.Add(0);
                }
                else if (bone == HumanBodyBones.RightLowerArm)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(10);
                    preset_lArm.Add(0);
                    preset_rArm.Add(10);
                    preset_Legs.Add(0);
                    preset_lLeg.Add(0);
                    preset_rLeg.Add(0);
                }
                else if (bone == HumanBodyBones.RightHand)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(10);
                    preset_lArm.Add(0);
                    preset_rArm.Add(10);
                    preset_Legs.Add(0);
                    preset_lLeg.Add(0);
                    preset_rLeg.Add(0);
                }
                else if (bone == HumanBodyBones.LeftUpperLeg)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(0);
                    preset_lArm.Add(0);
                    preset_rArm.Add(0);
                    preset_Legs.Add(10);
                    preset_lLeg.Add(10);
                    preset_rLeg.Add(0);
                }
                else if (bone == HumanBodyBones.LeftLowerLeg)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(0);
                    preset_lArm.Add(0);
                    preset_rArm.Add(0);
                    preset_Legs.Add(10);
                    preset_lLeg.Add(10);
                    preset_rLeg.Add(0);
                }
                else if (bone == HumanBodyBones.LeftFoot)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(0);
                    preset_lArm.Add(0);
                    preset_rArm.Add(0);
                    preset_Legs.Add(10);
                    preset_lLeg.Add(10);
                    preset_rLeg.Add(0);
                }
                else if (bone == HumanBodyBones.LeftToes)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(0);
                    preset_lArm.Add(0);
                    preset_rArm.Add(0);
                    preset_Legs.Add(10);
                    preset_lLeg.Add(10);
                    preset_rLeg.Add(0);
                }
                else if (bone == HumanBodyBones.RightUpperLeg)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(0);
                    preset_lArm.Add(0);
                    preset_rArm.Add(0);
                    preset_Legs.Add(10);
                    preset_lLeg.Add(0);
                    preset_rLeg.Add(10);
                }
                else if (bone == HumanBodyBones.RightLowerLeg)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(0);
                    preset_lArm.Add(0);
                    preset_rArm.Add(0);
                    preset_Legs.Add(10);
                    preset_lLeg.Add(0);
                    preset_rLeg.Add(10);
                }
                else if (bone == HumanBodyBones.RightFoot)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(0);
                    preset_lArm.Add(0);
                    preset_rArm.Add(0);
                    preset_Legs.Add(10);
                    preset_lLeg.Add(0);
                    preset_rLeg.Add(10);
                }
                else if (bone == HumanBodyBones.RightToes)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(0);
                    preset_lArm.Add(0);
                    preset_rArm.Add(0);
                    preset_Legs.Add(10);
                    preset_lLeg.Add(0);
                    preset_rLeg.Add(10);
                }
                else if (bone == HumanBodyBones.Hips)
                {
                    preset_limbs.Add(10);
                    preset_arms.Add(0);
                    preset_lArm.Add(0);
                    preset_rArm.Add(0);
                    preset_Legs.Add(10);
                    preset_lLeg.Add(10);
                    preset_rLeg.Add(10);
                }
                else
                {
                    preset_limbs.Add(0);
                    preset_arms.Add(0);
                    preset_lArm.Add(0);
                    preset_rArm.Add(0);
                    preset_Legs.Add(0);
                    preset_lLeg.Add(0);
                    preset_rLeg.Add(0);
                }
                #endregion
            }
            CreateImportancePresetAndAddToHolder("Zero", preset_zero,false);
            CreateImportancePresetAndAddToHolder("All", preset_full, false);
            CreateImportancePresetAndAddToHolder("Llimbs", preset_limbs, false);
            CreateImportancePresetAndAddToHolder("Arms", preset_arms, false);
            CreateImportancePresetAndAddToHolder("Left Arm", preset_lArm, false);
            CreateImportancePresetAndAddToHolder("Right Arm", preset_rArm, false);
            CreateImportancePresetAndAddToHolder("Legs", preset_Legs, false);
            CreateImportancePresetAndAddToHolder("Left Leg", preset_lLeg, false);
            CreateImportancePresetAndAddToHolder("Right Leg", preset_rLeg, false);


        }

        void CreateComponent(ref GameObject component, string _name, Transform parent,GameObject prefab = null)
        {
            if (component) return;
            if (prefab) component = Instantiate(prefab);
            else component = new GameObject();
            component.name = _name;
            component.transform.SetParent(parent);
            component.transform.position = Vector3.zero;
            component.transform.rotation = Quaternion.identity;
        }
        void EndEditMode()
        {
            if (editMode != 0)
            {
                foreach (BodyPartInfoHolder part in bodyParts)
                {
                    part.EndImportanceEdit();
                }
            }
        }
        #endregion

        #region Compair
        public float CompairRotation(PoseData posedata)
        {
            if (importanceDevider(posedata) == 0) return 999999;
            int size = bodyPartRoots.Count;
            float different = 0;

            List<Quaternion> rotationsData = posedata.GetRootRotations();
            List<float> importanceData = posedata.GetImportances();
            for (int i = 0; i < size; i++)
            {
                Quaternion currentLocalRotation = Quaternion.Inverse(transform.rotation) * bodyPartRoots[i].transform.rotation;
                different +=
                    Quaternion.Angle(currentLocalRotation, rotationsData[i])
                    * importanceData[i];
            }
            different /= importanceDevider(posedata);
            return different;

        }
        public float CompairPosition(PoseData posedata)
        {
            if (importanceDevider(posedata) == 0) return 999999;
            int size = bodyPartRoots.Count;
            float different = 0;

            List<float> importanceData = posedata.GetImportances();
            List<Vector3> posePositionDatas = posedata.GetPartPositions();
            for (int i = 0; i < size; i++)
            {
                Vector3 localPosition = transform.InverseTransformPoint(bodyParts[i].transform.position);
                Vector3 vectorDifference = localPosition - posePositionDatas[i];
                different += vectorDifference.magnitude * importanceData[i];
            }
            different /= importanceDevider(posedata);
            return different;
        }

        float importanceDevider(PoseData pose)
        {
            int size = pose.GetImportances().Count;
            float sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += pose.GetImportances()[i];
            }
            return sum;
        }
        #endregion//Compair

        //--------------Functining group End-----------

        #region Unity Events

        [ShowIfGroup("IF0", Condition = "@editMode == 0", Order = 100)]
        [BoxGroup("IF0/CompairDebug")]
        [field: SerializeField] bool compairDebug;
        void Start()
        {

        }

        void Update()
        {

        }
        private void OnDrawGizmos()
        {
            if (!compairDebug) return;
            if (!_currentSelectPose) return;
            rotationDifference = CompairRotation(_currentSelectPose) * rotationDifferenceMultier;
            positionDifference = CompairPosition(_currentSelectPose) * positionDifferenceMultier;

        }
        [BoxGroup("IF0/CompairDebug")] public float rotationDifference;
        [BoxGroup("IF0/CompairDebug")] public float positionDifference;
        #endregion//Unity Events
        List<string> groupNames
        {
            get
            {
                List<string> newList = new List<string>();
                List<Group<PoseData,PoseData>> groups = groupController.groups ;
                foreach (PoseGroup g in groups)
                {
                    newList.Add(g.groupName);
                }
                return newList;
            }
        }
        [BoxGroup("Demo"), ValueDropdown("groupNames"),SerializeField]
        string selectGroup;
        [BoxGroup("Demo"),Button]
        void FindCloestTransition()
        {
            List<PoseData> datas = groupController.GetGroup(selectGroup).items;
            List<float> difference = new List<float>();
            for (int i = 0; i < datas.Count; i++)
            {
                float dif = 0;
                dif += CompairPosition(datas[i]);
                dif += CompairRotation(datas[i]);
                difference.Add(dif);
            }
            _ApplyTransform(datas[difference.IndexOf(difference.Min())]);
        }
        #region get
        
        public PoseData GetPose(string name, int startFrame)
        {
            foreach (PoseData p in groupController.items)
            {
                if (p.GetPoseName() == name && p.GetStartFrame() == startFrame) return p;
            }
            return null;
        }
        public ImportancePreset GetImportancePreset(string name)
        {
            foreach (ImportancePreset preset in importancePresets)
            {
                if (preset.GetName() == name) return preset;
            }
            return null;
        }

        public BodyPartInfoHolder GetPart(string partName)
        {
            foreach (BodyPartInfoHolder part in bodyParts)
            {
                if (part.GetName() == partName) return part;
            }
            return null;
        }


        #endregion
        public override Type GetGroupControllerType()
        {
            return typeof(PoseGroupController);
        }

        public override Type GetGroupPreviewType()
        {
            return typeof(PoseDataGroupPreview);
        }
    }
}

