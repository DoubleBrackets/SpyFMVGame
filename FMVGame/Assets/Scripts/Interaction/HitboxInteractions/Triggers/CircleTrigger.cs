using System;
using UnityEngine;

[Serializable]
public struct CircleTriggerPose : IHandlesDrawable
{
    public Vector2 position;
    public float radius;

    public void DrawHandles(Color c)
    {
        DebugExtensions.DrawCircle2DHandle(position, radius, c);
    }

    public void DrawGizmos(Color c)
    {
        DebugExtensions.DrawCircle2DGizmo(position, radius, c);
    }

    public static bool operator ==(CircleTriggerPose a, CircleTriggerPose b)
        => a.position == b.position && a.radius == b.radius;

    public static bool operator !=(CircleTriggerPose a, CircleTriggerPose b) => !(a == b);
}

public class CircleTrigger : HitboxTrigger<CircleTriggerPose, CircleHitbox, CircleCollider2D>
{
    public override void UpdatePose(CircleTriggerPose circleTriggerPose)
    {
        if (hitBox == null)
        {
            return;
        }

        pose = circleTriggerPose;
        hitBox.SetTransform(circleTriggerPose.position, circleTriggerPose.radius);
    }

    public override void Initialize()
    {
        hitBox = HitboxPools.Instance.circleHitboxPool.GetFromPool();
    }

    public override void Dispose()
    {
        HitboxPools.Instance.circleHitboxPool.ReturnToPool(hitBox);
        hitBox = null;
    }
}