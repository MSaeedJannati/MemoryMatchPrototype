using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GizmoFactory;

public class Gizmo : MonoBehaviour
{
    #region Variables
    [SerializeField] Color Colour;
    [SerializeField] shape Shape;
    [SerializeField] renderType RenderType;
    [SerializeField] float Radius;
    Transform mTransform;
    bool hasTransform;
    #endregion
    #region monobehaviuor CallBacks
    private void OnDrawGizmos()
    {
        Gizmos.color = Colour;
        CheckForTransform();
        drawGizmo();
    }

    #endregion
    #region Functions
    public void CheckForTransform()
    {
        if (hasTransform)
            return;
        mTransform = GetComponent<Transform>();
        hasTransform = false;
    }
    public void drawGizmo()
    {

        switch (Shape)
        {
            case shape.SPHERE:
                if (RenderType == renderType.WIRE_FRAME)
                {
                    Gizmos.DrawWireSphere(mTransform.position, Radius);
                }
                else
                {
                    Gizmos.DrawSphere(mTransform.position, Radius);
                }
                break;
            case shape.CUBE:
                if (RenderType == renderType.WIRE_FRAME)
                {
                    Gizmos.DrawWireCube(mTransform.position, Radius * Vector3.one);
                }
                else
                {
                    Gizmos.DrawCube(mTransform.position, Radius * Vector3.one);
                }
                break;
        }
    }
    #endregion
}
#region GizmoTypeFactory
namespace GizmoFactory
{
  

    #region Enums
    public enum shape
    {
        SPHERE,
        CUBE
    }
    public enum renderType
    {
        WIRE_FRAME,
        SHADED
    }
    #endregion
}
#endregion