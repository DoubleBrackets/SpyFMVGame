using UnityEngine;

public class CircleHitbox : Hitbox<CircleCollider2D>
{
    public void SetTransform(Vector2 pos, float radius)
    {
        attachedCollider.radius = radius;
        transform.position = pos;
    }
}