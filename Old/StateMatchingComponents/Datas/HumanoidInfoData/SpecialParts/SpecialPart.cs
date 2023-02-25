using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
public class SpecialPart : MonoBehaviour
{
    [field: SerializeField] float gizmoSize;
    [field: SerializeField] float offset;
    [field: SerializeField] bool drawGizmo;
    [field: SerializeField] bool drawHandle;
    [field: SerializeField][ColorPalette] Color customColor = Color.yellow;
    void Start(){ }
    public void setGizmoSize(float newGizmoSize) => gizmoSize = newGizmoSize; 
    public void setOffest(float newOffset)
    {
        offset = newOffset;
    }
    public void GizmoOn()
    {
        drawGizmo = true;
    }
    public void GizmoOff()
    {
        drawGizmo = false;
    }
    public void HandleOn() => drawHandle = true;
    public void HandleOff() => drawHandle = false;

    private void OnDrawGizmos()
    {
        
        Vector3 position = transform.position;
        position.x += offset;
        float radious = gizmoSize * 0.07f;

        if (drawGizmo) DrawGizmo(position,radious);
        if (drawHandle) DrawHandle(position, radious);
    }
    void DrawGizmo(Vector3 position, float radious)
    {
        Gizmos.color = customColor;
        Gizmos.DrawSphere(position, radious);

    }
    void DrawHandle(Vector3 position, float radious)
    {
        Handles.color = customColor;
        Handles.SphereHandleCap(0, position, Quaternion.identity, radious, EventType.Repaint);
    }
}
