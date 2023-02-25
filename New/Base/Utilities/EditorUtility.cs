using UnityEditor;
using UnityEngine;


namespace Nino.NewStateMatching
{
    public static class EditorUtility
    {
        public static void OpenHierarchy(GameObject obj, bool open)
        {
            if (obj == null) return;
            EditorApplication.ExecuteMenuItem("Window/Panels/6 Hierarchy");
            var hierarchyWindow = EditorWindow.focusedWindow;
            var expandMethodInfo = hierarchyWindow.GetType().GetMethod("SetExpandedRecursive");
            expandMethodInfo.Invoke(hierarchyWindow, new object[] { obj.GetInstanceID(), open });
            if (open)
            {
                foreach (Transform t in obj.transform)
                {
                    if (t == obj.transform) continue;
                    OpenHierarchy(t.gameObject, false);
                }
            }
        }
    }

}
