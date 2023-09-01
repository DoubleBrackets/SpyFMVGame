using System;
using DoubleOhPew.Interactions.Core;
using UnityEngine;

[Serializable]
public struct BoxHitboxTriggerPose : IHandlesDrawable
{
    public Vector2 position;
    public Vector2 size;
    public float zAngle;

    public void DrawHandles(Color c)
    {
        DebugExtensions.DrawRectangle2DHandle(position, size, zAngle, c);
    }

    public void DrawGizmos(Color c)
    {
        DebugExtensions.DrawBox2DGizmo(position, size, zAngle, c);
    }
}

public class BoxHitboxTrigger : IInteractTrigger<BoxHitboxTriggerPose>
{
    private BoxHitbox hitBox;
    private BoxHitboxTriggerPose pose;

    public void UpdatePose(BoxHitboxTriggerPose boxHitboxTriggerPose)
    {
        if (hitBox == null)
        {
            return;
        }

        pose = boxHitboxTriggerPose;
        hitBox.SetTransform(boxHitboxTriggerPose.position, boxHitboxTriggerPose.size, boxHitboxTriggerPose.zAngle);
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
        hitBox = HitboxPools.Instance.boxHitboxPool.GetFromPool();
    }

    public void Dispose()
    {
        HitboxPools.Instance.boxHitboxPool.ReturnToPool(hitBox);
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