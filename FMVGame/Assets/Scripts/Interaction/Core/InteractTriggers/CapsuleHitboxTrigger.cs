using System;
using DoubleOhPew.Interactions.Core;
using UnityEngine;

[Serializable]
public struct CapsuleHitboxTriggerPose : IHandlesDrawable
{
    public Vector2 position;
    public Vector2 size;
    public float zAngle;

    public void DrawHandles(Color c)
    {
        DebugExtensions.DrawHandleCapsule2D(position, size, zAngle, c);
    }

    public void DrawGizmos(Color c)
    {
        DebugExtensions.DrawCapsuleColliderGizmo2D(position, size, zAngle, c);
    }
}

public class CapsuleHitboxTrigger : IInteractTrigger<CapsuleHitboxTriggerPose>
{
    private CapsuleHitbox hitBox;
    private CapsuleHitboxTriggerPose pose;

    public void UpdatePose(CapsuleHitboxTriggerPose CapsuleHitboxTriggerPose)
    {
        if (hitBox == null)
        {
            return;
        }

        pose = CapsuleHitboxTriggerPose;
        hitBox.SetTransform(CapsuleHitboxTriggerPose.position, CapsuleHitboxTriggerPose.size, CapsuleHitboxTriggerPose.zAngle);
    }

    public bool EvaluateInteraction(InteractionInfo interactionInfo)
    {
        if (interactionInfo.interactionType == InteractionInfo.InteractionType.Click)
        {
            return hitBox.EvaluateHitPoint(interactionInfo.mouseWorldPos);
        }

        return false;
    }

    public void Initialize()
    {
        hitBox = HitboxPools.Instance.capsuleHitboxPool.GetFromPool();
    }

    public void Dispose()
    {
        HitboxPools.Instance.capsuleHitboxPool.ReturnToPool(hitBox);
        hitBox = null;
    }

    public void Enable()
    {
        hitBox.SetHitboxActive(true);
        hitBox.OnDrawGizmo += DrawGizmo;
    }

    public void Disable()
    {
        hitBox.SetHitboxActive(false);
        hitBox.OnDrawGizmo -= DrawGizmo;
    }

    private void DrawGizmo()
    {
        pose.DrawGizmos(Color.red);
    }
}