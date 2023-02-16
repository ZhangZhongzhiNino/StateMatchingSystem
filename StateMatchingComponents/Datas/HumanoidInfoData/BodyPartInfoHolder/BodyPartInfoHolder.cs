using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace StateMatching.Data
{
    public class BodyPartInfoHolder : MonoBehaviour
    {
    
        [BoxGroup("Importance", Order = -100), ShowIfGroup("Importance/importanceEditMode"), SerializeField, Range(0, 10)] float importance;

        [BoxGroup("Values")][field: SerializeField] float mass;
        [HorizontalGroup("Values/size")][field: SerializeField] float size;
        [BoxGroup("Symmetrize")]
        [BoxGroup("Values")][field: SerializeField] string partName;
        [BoxGroup("Values")] public bool writeInPose;
        [BoxGroup("Values")] public bool customPart;

        [BoxGroup("Custom Gizmo")][field: SerializeField] bool customGizmo;
        [BoxGroup("Custom Gizmo")]
        [field: SerializeField]
        [ShowIfGroup("Custom Gizmo/customGizmo")]
        [Range(20, 200)] float customSize = 2;
        [ShowIfGroup("Custom Gizmo/customGizmo")]
        [field: SerializeField]
        [ColorPalette] Color customColor = Color.red;
        [BoxGroup("Symmetrize")]

        [TitleGroup("Symmetrize/Find mirror part")][field: SerializeField] [ReadOnly] BodyPartInfoHolder mirrorBodyPart;
        [TitleGroup("Symmetrize/Find mirror part")][field: SerializeField] string thisPartIdentifier = "";
        [TitleGroup("Symmetrize/Find mirror part")][field: SerializeField] string anotherPartIdentifier = "";

        [ShowIfGroup("Symmetrize/haveMirrorPart",Condition = "@mirrorBodyPart !=null")]
        [TitleGroup("Symmetrize/haveMirrorPart/Symmetrize Settings")][HorizontalGroup("Symmetrize/haveMirrorPart/Symmetrize Settings/1", Width =0.5f)]

    
        [VerticalGroup("Symmetrize/haveMirrorPart/Symmetrize Settings/1/1")][field: SerializeField] bool LocalXinverse = false;
        [VerticalGroup("Symmetrize/haveMirrorPart/Symmetrize Settings/1/1")][field: SerializeField] bool LocalYinverse = true;
        [VerticalGroup("Symmetrize/haveMirrorPart/Symmetrize Settings/1/1")][field: SerializeField] bool LocalZinverse = true;

        [BoxGroup("Display")][field: SerializeField] bool drawGizmo;
        [BoxGroup("Display")][field: SerializeField] bool drawHandles;

    

        float offset;
        [BoxGroup("Others Value")][field: SerializeField] HumanoidInfoData infoController;

        Vector3 defaultPosition = Vector3.zero;

        #region Unity Functions
    
        void Start()
        {

        }
        private void OnDrawGizmos()
        {
            if (!drawGizmo && !drawHandles) return;

            Vector3 position = transform.position;
            position.x += offset;
            float radius = mass * size * 0.001f;

            if (!writeInPose)
            {
                Color c = Color.blue;
                if (customGizmo)
                {
                    radius = customSize * 0.001f;
                    c.a = 0.6f;
                }
                Handles.color = c;
                Gizmos.color = c;
            }
            else
            {
                if (customGizmo)
                {
                    customColor.a = 1;
                    Handles.color = customColor;
                    Gizmos.color = customColor;
                    radius = customSize * 0.001f;
                }
                else if(!importanceEditMode)
                {
                    Handles.color = new Color(1, 1, 1, 1);
                    Gizmos.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    float f = MapRange(importance);
                    Handles.color = new Color(1.05f - f, 1.05f - f, 1.05f - f, 1);
                    Gizmos.color = new Color(1.05f - f, 1.05f - f, 1.05f - f, 1);

                
                }
            }
            


            if (drawGizmo) Gizmos.DrawSphere(position, radius);
            if (drawHandles) Handles.SphereHandleCap(0, position, Quaternion.identity, radius, EventType.Repaint);

        }
        float MapRange(float value, float from1=0, float to1=10, float from2=0, float to2=1)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }


        #endregion

        #region Value Edits
        [HorizontalGroup("Values/size")]
        [Button]
        [GUIColor(0.4f,1f,0.4f)]
        void matchAllSize()
        {
            if (infoController == null) return;
            HumanoidInfoData controller = infoController.GetComponent<HumanoidInfoData>();
            controller.SetGizmoSize(size);
            controller.ApplySize();
        }
        void offsetTransform(GameObject obj, float targetOffset)
        {
            Vector3 p = obj.transform.position;
            p.x += targetOffset;
            obj.transform.position = p;
        }
        [TitleGroup("Values/Reset")]
        [HorizontalGroup("Values/Reset/-1")]
        [Button(ButtonSizes.Large)]
        [GUIColor(0.4f, 1f, 1f)]
        public void ResetThisValues()
        {
            mass = infoController.GetDefaultMass();
            size = infoController.GetDefaultGizmoSize();
        }
        [HorizontalGroup("Values/Reset/-1")]
        [Button(ButtonSizes.Large)]
        [GUIColor(0.4f, 1f, 0.4f)]
        public void ResetAllValues()
        {
            List<BodyPartInfoHolder> L = infoController.GetAllBodyParts();
            foreach (BodyPartInfoHolder i in L)
            {
                i.ResetThisValues();
            }
        }
    
        [HorizontalGroup("Values/Reset/0")]
        [Button(ButtonSizes.Large)]
        [GUIColor(0.4f, 1f, 1f)]
        public void ResetThisPosition()
        {
            transform.localPosition = defaultPosition;
            size = infoController.GetDefaultGizmoSize();
            mass = infoController.GetDefaultMass();
        }
        [HorizontalGroup("Values/Reset/0")]
        [Button(ButtonSizes.Large)]
        [GUIColor(0.4f, 1f, 0.4f)]
        void ResetAllToPosition()
        {
            List<BodyPartInfoHolder> L = infoController.GetAllBodyParts();
            foreach(BodyPartInfoHolder i in L)
            {
                i.ResetThisPosition();
            }
        }
        [HorizontalGroup("Values/Reset/1")]
        [Button(ButtonSizes.Large)]
        [GUIColor(1f, 0.5f, 1f)]
        public void MarkDefaultPosition(){defaultPosition = transform.localPosition;}
        [HorizontalGroup("Values/Reset/1")]
        [Button(ButtonSizes.Large)]
        [GUIColor(1f, 1f, 0.4f)]
        void MarkAllDefaultPosition()
        {
            List<BodyPartInfoHolder> L = infoController.GetAllBodyParts();
            foreach (BodyPartInfoHolder i in L)
            {
                i.SetCurrentToDefaultPosition();
            }
        }
        public void SetCurrentToDefaultPosition()
        {
            defaultPosition = transform.localPosition;
        }

        #endregion//Edit Values

        #region Symmetrize
        private void AutoFindIdentifier()
        {
            if(partName.Contains("Right"))
            {
                thisPartIdentifier = "Right";
                anotherPartIdentifier = "Left";
            }
            else if (partName.Contains("Left"))
            {
                thisPartIdentifier = "Left";
                anotherPartIdentifier = "Right";
            }
        }

        [HorizontalGroup("Symmetrize/Find mirror part/1")]
        [Button(ButtonSizes.Large)]
        [GUIColor(0.4f, 1, 0.4f)]
        public void TryFindMirrorBodyPart()
        {
            if (thisPartIdentifier?.Length == 0 || anotherPartIdentifier?.Length == 0) AutoFindIdentifier();
            if (thisPartIdentifier?.Length == 0 || anotherPartIdentifier?.Length == 0) return;
            string anotherPartName = anotherPartIdentifier + partName.Remove(0, thisPartIdentifier.Length);
            mirrorBodyPart = infoController.GetBodyPartInfo(anotherPartName);
            if (mirrorBodyPart == null) return;
            mirrorBodyPart.SetThisPartIdentifier(anotherPartIdentifier);
            mirrorBodyPart.SetAnotherPartIdentifier(thisPartIdentifier);
            mirrorBodyPart.SetMirrorPart(this);
        }
        [HorizontalGroup("Symmetrize/Find mirror part/1")]
        [Button(ButtonSizes.Large)]
        [GUIColor(1f, 0.4f, 0.4f)]
        void ClearMirrorBodyPart()
        {
            thisPartIdentifier = "";
            anotherPartIdentifier = "";
            if (mirrorBodyPart == null) return;
            mirrorBodyPart.mirrorBodyPart = null;
            mirrorBodyPart = null;
        }
        [HorizontalGroup("Symmetrize/haveMirrorPart/Symmetrize Settings/1")]
        [Button(ButtonHeight =60)]
        [GUIColor(0.4f, 1f, 0.4f)]
        private void ApplySymmetrize()
        {
            if (mirrorBodyPart == null) return;


            Vector3 localPosition = infoController.GetRoot().transform.InverseTransformPoint(transform.position);

            if (LocalXinverse) localPosition.x = -localPosition.x;
            if (LocalYinverse) localPosition.y = -localPosition.y;
            if (LocalZinverse) localPosition.z = -localPosition.z;

            Vector3 toPosition = infoController.GetRoot().transform.TransformPoint(localPosition);

            mirrorBodyPart.Initialize(mass, size, toPosition);

        }

        #endregion//Symmetrize

        #region Importance

        bool importanceEditMode;

        [Button(ButtonSizes.Large),GUIColor(0.4f, 1f, 0.4f)]
        [ShowIfGroup("Importance/ApplyImportanceToSymmetrizedPart", Condition = "@importanceEditMode &&mirrorBodyPart != null")]
        private void ApplyImportanceToSymmetrizedPart()
        {
            mirrorBodyPart.SetImportance(importance);
        }

    
        #endregion

        #region Get Set Initialize
        #region Initialize
        public void Initialize (string newName,float newMass,float newSize,float newOffset,HumanoidInfoData controller,bool _customPart = false)
        {
            SetMass(newMass);
            SetSize(newSize);
            SetName(newName);
            SetOffset(newOffset);
            infoController = controller;
            drawGizmo = true;
            transform.rotation = controller.transform.rotation;
            customPart = _customPart;
        }
        public void Initialize(float newMass, float newSize, Vector3 globalPosition)
        {
            SetMass(newMass);
            SetSize(newSize);

            transform.position = globalPosition;
        }
        #endregion
        #region Get 
        public float GetMass() { return mass; }
        public float GetSize() { return size; }
        public string GetName() { return partName; }
        public float GetImportance() { return importance; }
        #endregion//Get
        #region Set
        public void SetMass(float newMass) => mass = newMass;
        public void SetSize(float newSize) => size = newSize;
        public void SetName(string newName) => partName = newName;
        public void SetOffset(float newOffset) => offset = newOffset;
        public void SetThisPartIdentifier(string newIdentifier) => thisPartIdentifier = newIdentifier;
        public void SetAnotherPartIdentifier(string newIdentifier) => anotherPartIdentifier = newIdentifier;
        public void SetMirrorPart(BodyPartInfoHolder mirrorPart) => mirrorBodyPart = mirrorPart;
        public void SetImportance(float _importance) => importance = _importance;
        #endregion//Set
        #region Set - Switch
        public void GizmosOn() { drawGizmo = true; }
        public void GizmosOff() { drawGizmo = false; }
        public void HandleOn() { drawHandles = true; }
        public void HandleOff() { drawHandles = false; }
        public void StartImportanceEdit(float _importance) { importanceEditMode = true; importance = _importance;}
        public float EndImportanceEdit() { importanceEditMode = false; return importance; }
        #endregion




        #endregion


    }
}


