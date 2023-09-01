using System;
using DoubleOhPew.Interactions.Core;
using UnityEngine;

[Serializable]
public struct CircleHitboxTriggerPose : IHandlesDrawable
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
}

public class CircleHitboxTrigger : IInteractTrigger<CircleHitboxTriggerPose>
{
    private CircleHitbox hitBox;
    private CircleHitboxTriggerPose pose;

    public void UpdatePose(CircleHitboxTriggerPose CircleHitboxTriggerPose)
    {
        if (hitBox == null)
        {
            return;
        }

        pose = CircleHitboxTriggerPose;
        hitBox.SetTransform(CircleHitboxTriggerPose.position, CircleHitboxTriggerPose.radius);
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
        hitBox = HitboxPools.Instance.circleHitboxPool.GetFromPool();
    }

    public void Dispose()
    {
        HitboxPools.Instance.circleHitboxPool.ReturnToPool(hitBox);
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