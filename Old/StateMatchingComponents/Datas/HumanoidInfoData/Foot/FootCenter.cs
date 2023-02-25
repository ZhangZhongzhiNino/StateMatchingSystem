using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FootCenter : MonoBehaviour
{
    [field: SerializeField] float radious;
    [field: SerializeField] float groundCheckRadious;
    [field: SerializeField] float offset;
    [field: SerializeField] LayerMask groundMask;

    [field: SerializeField] bool onGround;
    [field: SerializeField] GameObject controller;
    [field: SerializeField] bool drawGizmo;
    [field: SerializeField] bool drawHandle;

    void Start() { }
    private void FixedUpdate()
    {
        GroundCheck();
    }
    private void OnDrawGizmos()
    {
        if (!EditorApplication.isPlaying) GroundCheck();
        if (drawGizmo) DrawGizmo();
        if (drawHandle) DrawHandle();
    }

    void DrawGizmo()
    {
        if (onGround) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;
        Vector3 position = transform.position;
        Gizmos.DrawSphere(position, groundCheckRadious);
        if (onGround) Handles.DrawWireArc(position, Vector3.up, transform.forward, 360, radious);
        position.x += offset;
        Gizmos.DrawSphere(position, groundCheckRadious);
        if (onGround) Handles.DrawWireArc(position, Vector3.up, transform.forward, 360, radious);
    }

    void DrawHandle()
    {
        Vector3 position = transform.position;
        if (onGround) Handles.color = Color.green;
        else Handles.color = Color.red;
        Handles.SphereHandleCap(0, position, Quaternion.identity, groundCheckRadious, EventType.Repaint);
        position.x += offset;
        Handles.SphereHandleCap(0, position, Quaternion.identity, groundCheckRadious, EventType.Repaint);
    }
    #region Get Set Initiate
    public void SetRadious(float newRadious) => radious = newRadious;
    public void SetGroundCheckRadious(float newGroundCheckRadious) => groundCheckRadious = newGroundCheckRadious;
    public void SetGroundMask(LayerMask newGroundMask) => groundMask = newGroundMask;
    public void SetOffset(float newOffset) => offset = newOffset;
    public void setController(GameObject newController) => controller = newController;

    public float GetRadious() { return radious; }

    public void GizmoOn() => drawGizmo = true;
    public void GizmoOff() => drawGizmo = false;

    public void HandleOn() => drawHandle = true;
    public void HandleOff() => drawHandle = false;
    public void Iniciate(float newRadious, float newGroundCheckRadious, 
        LayerMask newGroundMask, float newOffset, GameObject newController)
    {
        SetRadious(newRadious);
        SetGroundCheckRadious(newGroundCheckRadious);
        SetGroundMask(newGroundMask);
        SetOffset(newOffset);
        setController(newController);
    }
    #endregion
    void GroundCheck()
    {
        if (Physics.CheckSphere(transform.position, groundCheckRadious, groundMask))
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

}
