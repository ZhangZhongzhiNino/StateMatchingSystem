using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Nino.StateMatching.Data
{
    public class ImportancePreset : MonoBehaviour
    {
        public string presetName;
        List<float> importance;
        #region get
        public string GetName() { return presetName; }
        public List<float> GetImportance() { return new List<float>(importance); }
        #endregion
        #region set
        public void SetName(string _name) { presetName = _name; }
        public void SetImportance(List<float> _importance) { importance = _importance; }
        #endregion
        public ImportancePreset(string _presetName, List<float> _importance)
        {
            presetName = _presetName;
            importance = _importance;
        }
        public void Initiate(string _presetName, List<float> _importance)
        {
            presetName = _presetName;
            importance = _importance;
        }
    }
}

