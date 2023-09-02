using System;
using UnityEngine;

[Serializable]
public struct CapsuleTriggerPose : IHandlesDrawable
{
    public Vector2 position;
    public Vector2 size;
    public float zAngle;

    public void DrawHandles(Color c)
    {
        DebugExtensions.DrawCapsule2DHandle(position, size, zAngle, c);
    }

    public void DrawGizmos(Color c)
    {
        DebugExtensions.DrawCapsule2DGizmo(position, size, zAngle, c);
    }

    public static bool operator ==(CapsuleTriggerPose a, CapsuleTriggerPose b)
        => a.position == b.position && a.size == b.size && a.zAngle == b.zAngle;

    public static bool operator !=(CapsuleTriggerPose a, CapsuleTriggerPose b) => !(a == b);
}

public class CapsuleTrigger : HitboxTrigger<CapsuleTriggerPose, CapsuleHitbox, CapsuleCollider2D>
{
    public override void UpdatePose(CapsuleTriggerPose capsuleTriggerPose)
    {
        if (hitBox == null)
        {
            return;
        }

        pose = capsuleTriggerPose;
        hitBox.SetTransform(capsuleTriggerPose.position, capsuleTriggerPose.size, capsuleTriggerPose.zAngle);
    }

    public override void Initialize()
    {
        hitBox = HitboxPools.Instance.capsuleHitboxPool.GetFromPool();
    }

    public override void Dispose()
    {
        HitboxPools.Instance.capsuleHitboxPool.ReturnToPool(hitBox);
        hitBox = null;
    }
}