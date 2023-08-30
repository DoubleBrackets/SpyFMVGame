using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider2D;

    public bool EvaluateHitPoint(Vector2 worldPoint)
    {
        return boxCollider2D.enabled && boxCollider2D.OverlapPoint(worldPoint);
    }

    public void SetTransform(Vector2 pos, Vector2 size, float rotZ)
    {
        var transf = transform;
        transf.rotation = Quaternion.Euler(0, 0, rotZ);
        boxCollider2D.size = size;
        transf.position = pos;
    }

    public void SetHitboxActive(bool active)
    {
        boxCollider2D.enabled = active;
    }

    // Start enabled
    public void Reset()
    {
        boxCollider2D.enabled = true;
    }

    private void OnDrawGizmos()
    {
        if (boxCollider2D.enabled)
        {
            DebugExtensions.DrawBoxColliderGizmo2D(boxCollider2D, Color.red);
        }
    }
}