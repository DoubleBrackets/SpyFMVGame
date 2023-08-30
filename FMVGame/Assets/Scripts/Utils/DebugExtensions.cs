using UnityEditor;
using UnityEngine;

public static class DebugExtensions
{
    public static void DrawBoxColliderGizmo2D(BoxCollider2D boxCollider2D, Color c)
    {
        Gizmos.color = c;
        // Bruh
        var transformMatrix = Matrix4x4.TRS(
            boxCollider2D.bounds.center,
            Quaternion.Euler(0, 0, boxCollider2D.transform.eulerAngles.z),
            boxCollider2D.size
        );

        Vector2 point1 = transformMatrix.MultiplyPoint3x4(new Vector2(0.5f, 0.5f));
        Vector2 point2 = transformMatrix.MultiplyPoint3x4(new Vector2(0.5f, -0.5f));
        Vector2 point3 = transformMatrix.MultiplyPoint3x4(new Vector2(-0.5f, -0.5f));
        Vector2 point4 = transformMatrix.MultiplyPoint3x4(new Vector2(-0.5f, 0.5f));

        Gizmos.DrawLine(point1, point2);
        Gizmos.DrawLine(point2, point3);
        Gizmos.DrawLine(point3, point4);
        Gizmos.DrawLine(point1, point4);
    }

    public static void DrawHandleRectangle(Vector2 position, Vector2 size, float zAngle, Color c)
    {
#if UNITY_EDITOR
        Handles.color = c;
        // Bruh
        var transformMatrix = Matrix4x4.TRS(
            position,
            Quaternion.Euler(0, 0, zAngle),
            size
        );

        Vector2 point1 = transformMatrix.MultiplyPoint3x4(new Vector2(0.5f, 0.5f));
        Vector2 point2 = transformMatrix.MultiplyPoint3x4(new Vector2(0.5f, -0.5f));
        Vector2 point3 = transformMatrix.MultiplyPoint3x4(new Vector2(-0.5f, -0.5f));
        Vector2 point4 = transformMatrix.MultiplyPoint3x4(new Vector2(-0.5f, 0.5f));

        Handles.DrawLine(point1, point2);
        Handles.DrawLine(point2, point3);
        Handles.DrawLine(point3, point4);
        Handles.DrawLine(point1, point4);
#endif
    }
}