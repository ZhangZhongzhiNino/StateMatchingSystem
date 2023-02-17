using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.Data
{
    public class PoseData : MonoBehaviour,IGroupItem<PoseData, PoseData>
    {
        string odinListName { get { return "\"" + poseName + "\"" + " at " + poseAtFrame + " frame"; } }

        public string itemName { get { return poseName; } set => poseName = value; }

        public PoseData value { get { return this; } set { AssignItem(value); } }

        [SerializeField] List<string> partNames;
        [SerializeField] List<Vector3> partPositions;
        [SerializeField] List<Vector3> rootPositions;
        [SerializeField] List<Quaternion> rotations;
        [SerializeField] List<float> importance;
        [SerializeField] string poseName;
        [SerializeField] int poseAtFrame;

        public PoseData()
        {
            partNames = new List<string>();
            partPositions = new List<Vector3>();
            rootPositions = new List<Vector3>();
            rotations = new List<Quaternion>();
            importance = new List<float>();
            poseName = "";
            poseAtFrame = -1;
        }
        public void Initialize(List<string> _partNames, List<Vector3> _partPositions, List<Vector3> _rootPositions, List<Quaternion> _rotations, string _name, int _poseAtFrame)
        {
            partNames = _partNames;
            partPositions = _partPositions;
            rootPositions = _rootPositions;
            rotations = _rotations;
            poseName = _name;
            poseAtFrame = _poseAtFrame;
            if (importance == null) importance = new List<float>();
            if (importance.Count != partPositions.Count)
            {
                for (int i = 0; i < partPositions.Count; i++)
                {
                    importance.Add(10);
                }
            }
        }

        public float GetStartFrame() { return poseAtFrame; }
        public string GetPoseName() { return poseName; }
        public List<Quaternion> GetRootRotations() { return new List<Quaternion>(rotations); }
        public List<Vector3> GetPartPositions() { return new List<Vector3>(partPositions); }
        public List<Vector3> GetRootPositions() { return new List<Vector3>(rootPositions); }
        public List<float> GetImportances() { return new List<float>(importance); }
        public List<string> GetPartNames() { return new List<string>(partNames); }

        public Quaternion GetRotation(string partName)
        {

            for (int i = 0; i < partNames.Count; i++)
            {

                if (partNames[i] == partName) return rotations[i];
            }
            return Quaternion.identity;
        }
        public Vector3 GetPartPosition(string partName)
        {

            for (int i = 0; i < partNames.Count; i++)
            {

                if (partNames[i] == partName) return partPositions[i];
            }
            return Vector3.zero;
        }
        public float GetImportance(string partName)
        {

            for (int i = 0; i < partNames.Count; i++)
            {

                if (partNames[i] == partName) return importance[i];
            }
            return 0;
        }

        public int GetPartIndex(string partName)
        {
            for (int i = 0; i < partNames.Count; i++)
            {

                if (partNames[i] == partName) return i;
            }
            return -1;
        }
        public Quaternion GetRotation(int index) { return rotations[index]; }
        public Vector3 GetPosition(int index) { return partPositions[index]; }
        public float GetImportance(int index) { return importance[index]; }


        public void SetStartFrame(int _startFrame) { poseAtFrame = _startFrame; }
        public void SetPoseName(string _animationName) { poseName = _animationName; }
        public void SetRotations(List<Quaternion> _rotations) { rotations = _rotations; }
        public void SetPartPositions(List<Vector3> _positions) { partPositions = _positions; }
        public void SetRootPositions(List<Vector3> _positions) { rootPositions = _positions; }
        public void SetImportance(List<float> _importance) { importance = _importance; }
        public void SetPartNames(List<string> _partNames) { partNames = _partNames; }

        public void AssignItem(PoseData item)
        {
            partNames = item.GetPartNames();
            partPositions=item.GetPartPositions();
            rootPositions=item.GetRootPositions();
            rotations = item.GetRootRotations();
            importance=item.GetImportances();
            poseName=item.GetPoseName();
            poseAtFrame=item.poseAtFrame;
        }
    }
}

