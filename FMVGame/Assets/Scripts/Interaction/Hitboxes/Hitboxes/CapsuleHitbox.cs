using UnityEngine;

public class CapsuleHitbox : Hitbox<CapsuleCollider2D>
{
    public void SetTransform(Vector2 pos, Vector2 size, float rotZ)
    {
        var transf = transform;
        transf.rotation = Quaternion.Euler(0, 0, rotZ);
        attachedCollider.size = size;
        transf.position = pos;
    }
}