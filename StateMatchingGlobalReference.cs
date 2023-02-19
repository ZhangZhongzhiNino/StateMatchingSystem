using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Nino.StateMatching
{
    public class StateMatchingGlobalReference : MonoBehaviour
    {
        public List<StateMatchingRoot> stateMatchingSystems;
        public List<string> stateMatchingInstanceNames
        {
            get
            {
                List<string> stringList = new List<string>();
                if (stateMatchingSystems == null) return stringList;
                int i = 0;
                foreach (StateMatchingRoot root in stateMatchingSystems)
                {
                    stringList.Add(i.ToString() + ": "+ root.stateMatchingName);
                    i++;
                }
                return stringList;
            }
        }
        public StateMatchingRoot GetSystemInstance(string instanceName)
        {
            if (stateMatchingSystems == null) return null;
            foreach(StateMatchingRoot root in stateMatchingSystems)
            {
                if (root.stateMatchingName == instanceName) return root;
            }
            int i =  getIndex(instanceName);
            if (i != -1) return stateMatchingSystems[i];
            return null;
        }
        public int getIndex(string instanceName)
        {
            int i = 0;
            foreach(string s in stateMatchingInstanceNames)
            {
                if (s == instanceName) return i;
                i++;
            }
            return -1;
        }
    }

}
