using System;
using UnityEngine;

[Serializable]
public struct BoxTriggerPose : IHandlesDrawable
{
    public Vector2 position;
    public Vector2 size;
    public float zAngle;

    public void DrawHandles(Color c)
    {
        DebugExtensions.DrawBox2DHandle(position, size, zAngle, c);
    }

    public void DrawGizmos(Color c)
    {
        DebugExtensions.DrawBox2DGizmo(position, size, zAngle, c);
    }
}

public class BoxTrigger : HitboxTrigger<BoxTriggerPose, BoxHitbox, BoxCollider2D>
{
    public override void UpdatePose(BoxTriggerPose boxTriggerPose)
    {
        if (hitBox == null)
        {
            return;
        }

        pose = boxTriggerPose;
        hitBox.SetTransform(boxTriggerPose.position, boxTriggerPose.size, boxTriggerPose.zAngle);
    }

    public override void Initialize()
    {
        hitBox = HitboxPools.Instance.boxHitboxPool.GetFromPool();
    }

    public override void Dispose()
    {
        HitboxPools.Instance.boxHitboxPool.ReturnToPool(hitBox);
        hitBox = null;
    }
}