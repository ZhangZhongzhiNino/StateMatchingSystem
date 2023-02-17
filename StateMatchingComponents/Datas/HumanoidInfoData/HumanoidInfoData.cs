using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEditor;

using StateMatching;
using StateMatching.Helper;
namespace StateMatching.Data
{
    public class HumanoidInfoData : ExtensionExecuter                    //MonoBehaviour, IStateMatchingComponent
    {
        #region 0 Body Part List
        [PropertySpace(SpaceBefore = 5, SpaceAfter = 5), ReadOnly]
        [OnValueChanged("updateBodyParts")]
        [SerializeField]
        private List<BodyPartInfoHolder> bodyParts;

        #endregion//0 Body Part List
        #region 0 Reference
        [FoldoutGroup("Reference")]
        #region 1 Prefabs
        [TitleGroup("Reference/Prefabs")][SerializeField][Required] GameObject bodyPartPrefab;
        [TitleGroup("Reference/Prefabs")][SerializeField][Required] GameObject rootPrefab;
        [TitleGroup("Reference/Prefabs")][SerializeField][Required] GameObject centerOfMassPrefab;
        [TitleGroup("Reference/Prefabs")][SerializeField][Required] GameObject footCenterPrefab;

        #endregion//1 Prefabs
        #region 2 Data
        [TitleGroup("Reference/Data")][SerializeField] SpecialPart centerOfMass;
        [TitleGroup("Reference/Data")][SerializeField] FootCenter rightFootCenter;
        [TitleGroup("Reference/Data")][SerializeField] FootCenter leftFootCenter;
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 20)]
        [TitleGroup("Reference/Data")][SerializeField] SpecialPart _partDataRoot;
        SpecialPart partDataRoot
        {
            get
            {
                if (_partDataRoot == null) createRoot();
                return _partDataRoot;
            }
            set { _partDataRoot = value; }
        }

        #endregion//1 Reference

        #endregion//0 Reference
        [FoldoutGroup("Set Up")]
        #region 0 Set Up Body Parts
        [FoldoutGroup("Set Up/Auto Set Up",order: -2)]
        #region 1 Auto Create
        [TitleGroup("Set Up/Auto Set Up/Auto Set Up")]
        [Button(ButtonSizes.Large), GUIColor(0.4f, 1, 0.4f)]
        private void AutoSetUpBodyPartInfo()
        {
            if (root.animator == null) root.FindUnityComponent<Animator>(ref root.animator);
            if (root.animator == null) return;
            if (bodyParts.Count != 0) ClearAllBodyPart(false);
            foreach (HumanBodyBones bone in Enum.GetValues(typeof(HumanBodyBones)))
            {
                GameObject obj = root.animator?.GetBoneTransform(bone)?.gameObject;
                if (obj == null || bone.ToString() == "LeftEye") break;
                bodyParts.Add(TrySetInfo(obj, bone.ToString(), defaultMass, gizmoSize, editDisplayOffset));
            }
            MoveCenterOfMass();
            centerOfMass.setGizmoSize(gizmoSize);
            if (poseDataManager != null) poseDataManager?.UpdateBodyPartsData();
            MarkAllBodyPartDefaultPosition(false);
            FindAllBodyPartMirrorPart();
            createRoot();
        }

        void createRoot()
        {
            if (_partDataRoot != null) return;
            GameObject _root = Instantiate(rootPrefab, transform.position, transform.rotation);
            _root.transform.parent = this.transform;
            _partDataRoot = _root.GetComponent<SpecialPart>();
        }

        [TitleGroup("Set Up/Auto Set Up/Auto Set Up"), Button(ButtonSizes.Large), GUIColor(1, 0.4f, 0.4f)]
        private void ClearAllBodyPart()
        { 
            ClearAllBodyPart(true);
            ClearAllBodyPart(false);
        }
        private BodyPartInfoHolder TrySetInfo(GameObject root, string name, float mass, float size, float offset, bool customPart = false)
        {
            GameObject newBodypart = Instantiate(bodyPartPrefab);
            newBodypart.GetComponent<BodyPartInfoHolder>().Initialize(name, mass, size, offset, this, _customPart: customPart);
            newBodypart.name = name;
            newBodypart.transform.SetParent(root.transform);
            newBodypart.transform.localPosition = Vector3.zero;
            newBodypart.transform.localScale = new Vector3(1, 1, 1);


            return newBodypart.GetComponent<BodyPartInfoHolder>();
        }

        #endregion//1 Auto Create
        #region 1 Position Auto Adjust
        [ShowIfGroup("Set Up/Auto Set Up/Auto Adjust", Condition = "@bodyParts.Count != 0")]
        [TitleGroup("Set Up/Auto Set Up/Auto Adjust/Auto Adjust Body Part Locations")]
        [Button(ButtonSizes.Large)]
        [GUIColor(0.4f, 1, 0.4f)]
        void AutoAdjustBodyPartPosition()
        {
            List<string> armsR = new List<string> { "RightShoulder", "RightUpperArm", "RightLowerArm", "RightHand" };
            List<string> armsL = new List<string> { "LeftShoulder", "LeftUpperArm", "LeftLowerArm", "LeftHand" };
            List<string> legsR = new List<string> { "RightUpperLeg", "RightLowerLeg", "RightFoot" };
            List<string> legsL = new List<string> { "LeftUpperLeg", "LeftLowerLeg", "LeftFoot" };
            List<string> root = new List<string> { "Spine", "Chest", "UpperChest", "Neck" };
            AdjustWeightCenterPosition(armsR);
            AdjustWeightCenterPosition(armsL);
            AdjustWeightCenterPosition(legsR);
            AdjustWeightCenterPosition(legsL);
            AdjustWeightCenterPosition(root);
            foreach (BodyPartInfoHolder i in bodyParts)
            {
                i.SetCurrentToDefaultPosition();
            }
            MarkAllBodyPartDefaultPosition(false);
        }
        void AdjustWeightCenterPosition(List<string> names)
        {
            List<BodyPartInfoHolder> parts = new List<BodyPartInfoHolder>();
            foreach (string n in names)
            {
                BodyPartInfoHolder instance = GetBodyPartInfo(n);
                if (instance != null) parts.Add(instance);
            }
            for (int i = 0; i < (parts.Count - 1); i++)
            {
                MoveToMiddlePosition(parts[i], parts[i + 1]);
            }
        }
        void MoveToMiddlePosition(MonoBehaviour a, MonoBehaviour b)
        {
            a.transform.position = (a.transform.position + b.transform.position) / 2;
        }

        [TitleGroup("Set Up/Auto Set Up/Auto Adjust/Auto Adjust Body Part Locations")]
        [Button(ButtonSizes.Large), PropertySpace(SpaceBefore = 0, SpaceAfter = 15)]
        [GUIColor(1, 1, 0.4f)]
        void ResetBodyPartsLocation()
        {
            foreach (BodyPartInfoHolder part in bodyParts)
            {
                part.transform.localPosition = Vector3.zero;
            }
            foreach (BodyPartInfoHolder i in bodyParts)
            {
                i.SetCurrentToDefaultPosition();
            }
            MarkAllBodyPartDefaultPosition(true);
            MarkAllBodyPartDefaultPosition(false);
        }


        #endregion//1 Bodypart Auto Adjust
        [FoldoutGroup("Set Up/Create Custom Part",order: -1)]
        #region 1 Add Custom Body Parts
        [TitleGroup("Set Up/Create Custom Part/Add Custom Parts")]
        [SerializeField] private List<GameObject> bodyPartsSetUpList;

        [TitleGroup("Set Up/Create Custom Part/Add Custom Parts"), GUIColor(0.4f, 1, 0.4f)]
        [Button(ButtonSizes.Large)]
        private void ProcessesBodyPartsSetUpList()
        {
            List<BodyPartInfoHolder> partToCreate = new List<BodyPartInfoHolder>();
            foreach (GameObject obj in bodyPartsSetUpList)
            {
                partToCreate.Add(TrySetInfo(obj, "Custome Part", defaultMass, gizmoSize, editDisplayOffset, customPart: true));

            }
            foreach (BodyPartInfoHolder part in partToCreate) part.MarkDefaultPosition();
            bodyParts.AddRange(partToCreate);
            bodyPartsSetUpList.Clear();
            if (poseDataManager == null) return;
            poseDataManager?.UpdateBodyPartsData();
        }

        [TitleGroup("Set Up/Create Custom Part/Add Custom Parts"), Button(ButtonSizes.Large), GUIColor(1, 0.4f, 0.4f)]
        private void ClearAllCustomBodyParts()
        {
            ClearAllBodyPart(true);
        }
        [TitleGroup("Set Up/Create Custom Part/Add Custom Parts")]
        [Button(ButtonSizes.Large), GUIColor(1, 0.4f, 0.4f), PropertySpace(SpaceBefore = 0, SpaceAfter = 15)]
        private void ClearBodyPart(string bodyPartName)
        {
            List<BodyPartInfoHolder> toRemove = new List<BodyPartInfoHolder>();

            foreach (BodyPartInfoHolder part in bodyParts)
            {
                if (part.GetName() == bodyPartName)
                {
                    toRemove.Add(part);
                }
            }
            foreach (BodyPartInfoHolder part in toRemove)
            {
                bodyParts.Remove(part);
                Helpers.RemoveGameObject(part.gameObject);
            }
            if (poseDataManager == null) return;
            poseDataManager?.UpdateBodyPartsData();
        }
        #endregion//1 Add Custom Body Parts
        #region 1Helpers
        void MarkAllBodyPartDefaultPosition(bool customPart)
        {
            foreach (BodyPartInfoHolder part in bodyParts)
            {
                if (part.customPart != customPart) continue;
                part.MarkDefaultPosition();
            }
        }
        void FindAllBodyPartMirrorPart()
        {
            foreach (BodyPartInfoHolder part in bodyParts)
            {
                part.TryFindMirrorBodyPart();
            }
        }
        private void ClearAllBodyPart(bool custom)
        {
            foreach (BodyPartInfoHolder part in bodyParts)
            {
                if (part.customPart != custom) continue;
                Helpers.RemoveGameObject(part?.gameObject);
            }
            bodyParts.RemoveAll(item => item.customPart == custom);
            if (custom == false)
            {
                if (rightFootCenter != null) Helpers.RemoveGameObject(rightFootCenter.gameObject);
                if (leftFootCenter != null) Helpers.RemoveGameObject(leftFootCenter.gameObject);
            }
            if (poseDataManager != null) poseDataManager?.UpdateBodyPartsData();

        }
        #endregion//1 Helpers
        #endregion//0 Set Up Body Parts
        [FoldoutGroup("Edit Values")]
        #region 0 Value Edit
        [TabGroup("Edit Values/0", "Body Part")]
        #region 1 Body Part
        #region 2 Value
        [TitleGroup("Edit Values/0/Body Part/Default Value")]
        [OnValueChanged("ApplySize")]
        [Range(10, 0.1f)]
        [field: SerializeField] float gizmoSize = 0.5f;

        [TitleGroup("Edit Values/0/Body Part/Default Value")]
        [field: SerializeField]
        [OnValueChanged("ApplyMass")]
        [Range(1f, 150f)]
        float defaultMass = 70;

        [TitleGroup("Edit Values/0/Body Part/Default Value")]
        [field: SerializeField]
        [OnValueChanged("ApplyOffset")]
        [Range(0f, 10f)]
        float editDisplayOffset = 0;

        #endregion//2 Default Value
        #region 2 Functions
        float prevMass = 70;
        [TitleGroup("Edit Values/0/Body Part/Default Value")]
        [Button, PropertySpace(SpaceBefore = 0, SpaceAfter = 5)]
        [GUIColor(1f, 1f, 0.4f)]
        void ResetBodyPartValue()
        {
            gizmoSize = 0.5f;
            defaultMass = 70;
            editDisplayOffset = 0;
            ApplyNewGizmoInfo();
            prevMass = defaultMass;
        }
        public void ApplyNewGizmoInfo()
        {
            if (bodyParts.Count != 0)
            {
                IEnumerator<BodyPartInfoHolder> parts = bodyParts.GetEnumerator();
                while (parts.MoveNext())
                {
                    parts.Current.Initialize(parts.Current.GetName(), defaultMass, gizmoSize, editDisplayOffset, this);
                }
                centerOfMass?.setOffest(editDisplayOffset);
                centerOfMass?.setGizmoSize(gizmoSize);
                rightFootCenter?.SetOffset(editDisplayOffset);
                leftFootCenter?.SetOffset(editDisplayOffset);
            }
        }
        public void ApplySize()
        {
            if (bodyParts.Count != 0)
            {
                IEnumerator<BodyPartInfoHolder> parts = bodyParts.GetEnumerator();
                while (parts.MoveNext())
                {
                    parts.Current.SetSize(gizmoSize);
                }
                centerOfMass?.setGizmoSize(gizmoSize);
            }
        }
        void ApplyMass()
        {
            if (bodyParts.Count != 0)
            {
                IEnumerator<BodyPartInfoHolder> parts = bodyParts.GetEnumerator();
                while (parts.MoveNext())
                {
                    if (parts.Current.GetMass() == prevMass) parts.Current.SetMass(defaultMass);
                }
                prevMass = defaultMass;
            }
        }
        void ApplyOffset()
        {
            if (bodyParts.Count != 0)
            {
                IEnumerator<BodyPartInfoHolder> parts = bodyParts.GetEnumerator();
                while (parts.MoveNext())
                {
                    parts.Current.SetOffset(editDisplayOffset);
                }
                centerOfMass?.setOffest(editDisplayOffset);
                rightFootCenter?.SetOffset(editDisplayOffset);
                leftFootCenter?.SetOffset(editDisplayOffset);
            }
        }

        #endregion//2 Functions
        #endregion//1 Body Part
        [TabGroup("Edit Values/0", "Foot")]
        #region 1 Foot
        #region 2 Variables
        [TitleGroup("Edit Values/0/Foot/Values")]
        [field: SerializeField]
        [OnValueChanged("ApplyFootRadious")]
        [Range(0.01f, 1)]
        float footRadious = 0.14f;

        [TitleGroup("Edit Values/0/Foot/Values")]
        [field: SerializeField]
        [OnValueChanged("ApplyGroundCheckRadious")]
        [Range(0.001f, 0.1f)]
        float groundCheckRadious = 0.03f;

        [TitleGroup("Edit Values/0/Foot/Values")]
        [field: SerializeField]
        [Range(-0.1f, -0.001f)]
        float footCenterYoffset = -0.08f;

        [TitleGroup("Edit Values/0/Foot/Values")]
        [field: SerializeField]
        [OnValueChanged("ApplyGroundMask")]
        LayerMask groundMask = 1;

        #endregion//2 Variables
        #region 2 Functions
        [TitleGroup("Edit Values/0/Foot/Values")]
        [Button]
        [GUIColor(1f, 1f, 0.4f)]
        private void ResetFootValues()
        {
            footRadious = 0.14f;
            groundCheckRadious = 0.03f;
            footCenterYoffset = -0.08f;
            ApplyFootRadious();
            ApplyGroundCheckRadious();
        }
        private void ApplyFootRadious()
        {
            leftFootCenter?.SetRadious(footRadious);
            rightFootCenter?.SetRadious(footRadious);
        }
        private void ApplyGroundCheckRadious()
        {
            leftFootCenter?.SetGroundCheckRadious(groundCheckRadious);
            rightFootCenter?.SetGroundCheckRadious(groundCheckRadious);
        }
        private void ApplyGroundMask()
        {
            leftFootCenter?.SetGroundMask(groundMask);
            rightFootCenter?.SetGroundMask(groundMask);
        }

        #endregion//2 Functions
        #endregion//1 Foot
        #endregion//0 Value Edit
        #region 0 Gizmos
        [FoldoutGroup("Display")]
        [HorizontalGroup("Display/0", Width = 0.5f)]
        [BoxGroup("Display/0/Gizmo")]
        [Button]
        [GUIColor(0.4f, 1, 0.4f)]
        void turnOnGizmos()
        {
            if (bodyParts.Count != 0)
            {
                IEnumerator<BodyPartInfoHolder> parts = bodyParts.GetEnumerator();
                while (parts.MoveNext())
                {
                    parts.Current.GizmosOn();
                }

                centerOfMass?.GizmoOn();
                rightFootCenter?.GizmoOn();
                leftFootCenter?.GizmoOn();
            }
        }

        [BoxGroup("Display/0/Gizmo")]
        [Button]
        [GUIColor(1, 0.4f, 0.4f)]
        void turnOffGizmos()
        {
            if (bodyParts.Count != 0)
            {
                IEnumerator<BodyPartInfoHolder> parts = bodyParts.GetEnumerator();
                while (parts.MoveNext())
                {
                    parts.Current.GizmosOff();
                }
                centerOfMass.GizmoOff();
                rightFootCenter?.GizmoOff();
                leftFootCenter?.GizmoOff();
            }
        }

        [BoxGroup("Display/0/Handle")]
        [Button]
        [GUIColor(0.4f, 1, 0.4f)]
        void turnOnHandles()
        {
            if (bodyParts.Count != 0)
            {
                IEnumerator<BodyPartInfoHolder> parts = bodyParts.GetEnumerator();
                while (parts.MoveNext())
                {
                    parts.Current.HandleOn();
                }
                centerOfMass?.HandleOn();
                leftFootCenter?.HandleOn();
                rightFootCenter?.HandleOn();
            }
        }

        [BoxGroup("Display/0/Handle")]
        [Button]
        [GUIColor(1, 0.4f, 0.4f)]
        void turnOffHandles()
        {
            if (bodyParts.Count != 0)
            {
                IEnumerator<BodyPartInfoHolder> parts = bodyParts.GetEnumerator();
                while (parts.MoveNext())
                {
                    parts.Current.HandleOff();
                }
                centerOfMass?.HandleOff();
                leftFootCenter?.HandleOn();
                rightFootCenter?.HandleOn();
            }
        }
        #endregion//0 Gizmos
        #region 0 Datas
        [FoldoutGroup("Datas", Order = 99)]
        [TitleGroup("Datas/Center of Mass Momentum", Order = -10)] public Vector3 COM_velocity;
        [TitleGroup("Datas/Center of Mass Momentum")] public Vector3 COM_acceleration;
        [TitleGroup("Datas/Center of Mass Momentum")] public Quaternion torque;
        #endregion
        #region 0 Extensions
        [FoldoutGroup("Extensions", Order = 100)]
        #region 1 Pose Data Manager

        [FoldoutGroup("Extensions/Pose Data Manager", Order = -10)]
        [field: SerializeField]
        PoseDataManager poseDataManager;

        [FoldoutGroup("Extensions/Pose Data Manager")]
        [ShowIfGroup("Extensions/Pose Data Manager/0", Condition = "@poseDataManager == null")]
        [Button]
        [GUIColor(0.4f, 1, 0.4f)]
        private void SetUpPoseDataManager()
        {
            if (poseDataManager != null) return;
            poseDataManager = this.gameObject.AddComponent<PoseDataManager>();
            poseDataManager.Initialize();
        }

        [FoldoutGroup("Extensions/Pose Data Manager")]
        [ShowIfGroup("Extensions/Pose Data Manager/1", Condition = "@poseDataManager != null")]
        [Button]
        [GUIColor(1, 0.4f, 0.4f)]
        private void DestroyPoseDataManager()
        {
            if (poseDataManager == null) return;
            poseDataManager.PreDestroy();
            Helpers.RemoveComponent(poseDataManager);
        }

        #endregion//1 Pose Data Manager
        #endregion//0 Extensions


        #region 0 Unity routines
        #region 1 Routines
        void Start() { }
        private void OnEnable()
        {
            StartCoroutine("PhysicsCoroutine");
        }
        private void FixedUpdate()
        {
            MoveCenterOfMass();
            UpdateFootCenter();
        }
        private void OnDrawGizmos()
        {
            if (EditorApplication.isPlaying) return;
            MoveCenterOfMass();
            UpdateFootCenter();
        }

        #endregion//1 Routines
        #region 1 Helpers
        #region 2 Center Of Mass Helpers
        private Vector3 CalculateCenterOfMass()
        {
            Vector3 sumPosition = new Vector3(0, 0, 0);
            if (bodyParts == null) return sumPosition;
            if (bodyParts.Count == 0) return sumPosition;
            float sumMass = 0;
            IEnumerator<BodyPartInfoHolder> parts = bodyParts.GetEnumerator();
            while (parts.MoveNext())
            {

                sumPosition += parts.Current.transform.position * parts.Current.GetMass();
                sumMass += parts.Current.GetMass();
            }
            sumPosition /= sumMass;
            return sumPosition;
        }

        private void MoveCenterOfMass()
        {
            if (bodyParts == null) return;
            if (centerOfMass == null)
            {
                GameObject cm = Instantiate(centerOfMassPrefab);
                cm.transform.SetParent(this.transform);
                centerOfMass = cm.GetComponent<SpecialPart>();
                centerOfMass.setOffest(editDisplayOffset);
                cm.transform.localPosition = Vector3.zero;
                cm.transform.localRotation = Quaternion.identity;
                if (bodyParts.Count != 0) cm.transform.position = CalculateCenterOfMass();
            }
            else
            {
                if (bodyParts.Count == 0) return;
                Vector3 newComPosition = CalculateCenterOfMass();
                centerOfMass.transform.position = newComPosition;
            }
        }

        #endregion//2 Center Of Mass Helpers
        #region 2 Foot Helpers
        private void UpdateFootCenter()
        {
            if (bodyParts == null) return;
            if (bodyParts.Count == 0) return;
            Vector3 rightFootPosition = new Vector3(0, 0, 0);
            Vector3 rightToesPosition = new Vector3(0, 0, 0);
            Vector3 leftFootPosition = new Vector3(0, 0, 0);
            Vector3 leftToesPosition = new Vector3(0, 0, 0);
            GameObject rightFootObj = this.gameObject;
            GameObject leftFootObj = this.gameObject;
            IEnumerator<BodyPartInfoHolder> parts = bodyParts.GetEnumerator();
            while (parts.MoveNext())
            {
                if (parts.Current.GetName() == "RightFoot")
                {
                    rightFootObj = parts.Current.gameObject;
                    rightFootPosition = parts.Current.transform.position;
                }
                else if (parts.Current.GetName() == "RightToes") rightToesPosition = parts.Current.transform.position;
                if (parts.Current.GetName() == "LeftFoot")
                {
                    leftFootObj = parts.Current.gameObject;
                    leftFootPosition = parts.Current.transform.position;
                }
                else if (parts.Current.GetName() == "LeftToes") leftToesPosition = parts.Current.transform.position;
            }
            Vector3 rightFootCenterPosition = (rightFootPosition + rightToesPosition) / 2;
            rightFootCenterPosition.y = rightToesPosition.y + footCenterYoffset;
            Vector3 leftFootCenterPosition = (leftFootPosition + leftToesPosition) / 2;
            leftFootCenterPosition.y = leftToesPosition.y + footCenterYoffset;

            if (rightFootCenter == null || leftFootCenter == null)
            {
                GameObject P_rightFootCenter = Instantiate(footCenterPrefab);
                rightFootCenter = P_rightFootCenter.GetComponent<FootCenter>();
                P_rightFootCenter.name = "RightFootCenter";
                P_rightFootCenter.transform.position = rightFootCenterPosition;
                P_rightFootCenter.transform.rotation = rightFootObj.transform.rotation;
                P_rightFootCenter.transform.SetParent(rightFootObj.transform);
                FootCenter C_rightFootCenter = P_rightFootCenter.GetComponent<FootCenter>();
                C_rightFootCenter.Iniciate(footRadious, groundCheckRadious, groundMask, editDisplayOffset, this.gameObject);

                GameObject P_leftFootCenter = Instantiate(footCenterPrefab);
                leftFootCenter = P_leftFootCenter.GetComponent<FootCenter>();
                P_leftFootCenter.name = "LeftFootCenter";
                P_leftFootCenter.transform.position = leftFootCenterPosition;
                P_leftFootCenter.transform.rotation = leftFootObj.transform.rotation;
                P_leftFootCenter.transform.SetParent(leftFootObj.transform);
                FootCenter C_leftFootCenter = P_leftFootCenter.GetComponent<FootCenter>();
                C_leftFootCenter.Iniciate(footRadious, groundCheckRadious, groundMask, editDisplayOffset, this.gameObject);

            }
            else
            {
                rightFootCenter.transform.position = rightFootCenterPosition;
                leftFootCenter.transform.position = leftFootCenterPosition;

            }
        }

        #endregion//2 Foot Helpers

        #endregion 1 Helpers

        #endregion//0 Unity routines
        #region 0 Physics Calculations 
        [BoxGroup("Datas/Calculations Related")][field: SerializeField] float physicsCalculationInterval;
        private Vector3 COM_prevVolocity;
        private Vector3 COM_prevPosition;
        IEnumerator PhysicsCoroutine()
        {
            if (physicsCalculationInterval <= 0) physicsCalculationInterval = 0.4f;
            PhysicsCalculationStepsIniciate();
            while (true)
            {
                PhysicsCalculations();
                PhysicsCalculationStepsIniciate();
                yield return new WaitForSeconds(physicsCalculationInterval);
            }
        }
        void PhysicsCalculationStepsIniciate()
        {
            COM_prevVolocity = COM_velocity;
            COM_prevPosition = centerOfMass.transform.position;
        }
        void PhysicsCalculations()
        {
            Vector3 COM_CurrentPosition = centerOfMass.transform.position;
            COM_velocity = (COM_CurrentPosition - COM_prevPosition) / physicsCalculationInterval;
            COM_acceleration = (COM_velocity - COM_prevVolocity) / physicsCalculationInterval;
        }


        void updateBodyParts()
        {
            poseDataManager?.UpdateBodyPartsData();
        }
        #endregion//0 Physics Calculations (Center of Mass)

        #region 0 Get functions

        public BodyPartInfoHolder GetBodyPartInfo(string bodyPartName)
        {
            foreach (BodyPartInfoHolder part in bodyParts)
            {
                if (part.GetName() == bodyPartName) return part;
            }
            return null;
        }

        public List<BodyPartInfoHolder> GetAllBodyParts()
        {
            return bodyParts;
        }

        public SpecialPart GetCenterOfMass()
        {
            return centerOfMass;
        }
        public FootCenter GetRightFootCenter()
        {
            return rightFootCenter;
        }
        public FootCenter GetLeftFootCenter()
        {
            return leftFootCenter;
        }

        public float GetDefaultGizmoSize()
        {
            return gizmoSize;
        }
        public float GetDefaultMass()
        {
            return defaultMass;
        }
        public SpecialPart GetRoot() { return partDataRoot; }
        #endregion//0 Get functions
        #region 0 Set Functions
        public void SetGizmoSize(float newSize)
        {
            gizmoSize = newSize;
        }
        #endregion

        #region Initialize & Destroy
        public override void Initialize<T>(T instance = null, StateMatchingRoot _stateMatchingRoot = null)
        {
            base.Initialize(instance, _stateMatchingRoot);
            MoveCenterOfMass();
            createRoot();
            if (bodyParts == null) bodyParts = new List<BodyPartInfoHolder>();
        }

        public override void PreDestroy()
        {
            ClearAllExtensions();
            ClearAllBodyPart();
            if (partDataRoot) Helpers.RemoveGameObject(partDataRoot.gameObject);
            base.PreDestroy();
        }
        void ClearAllExtensions()
        {
            if (poseDataManager != null) DestroyPoseDataManager();
        }

        
        #endregion
    }
}

