using System;
using DoubleOhPew.Interaction.Core;
using UnityEngine;

[Serializable]
public struct BoxColliderTriggerPose : IHandlesDrawable
{
    public Vector2 position;
    public Vector2 size;
    public float zRot;

    public void DrawHandles(Color c)
    {
        DebugExtensions.DrawHandleRectangle(position, size, zRot, c);
    }
}

public class BoxColliderTrigger : IInteractTrigger<BoxColliderTriggerPose>
{
    private Hitbox hitBox;

    private bool enabled;

    public void UpdatePose(BoxColliderTriggerPose boxColliderTriggerPose)
    {
        if (hitBox == null)
        {
            return;
        }

        hitBox.SetTransform(boxColliderTriggerPose.position, boxColliderTriggerPose.size, boxColliderTriggerPose.zRot);
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
        // We don't want actual hitboxes when scrubbing with timeline
        // Bit of a jank hack, should handle on playable side later
        if (Application.isPlaying)
        {
            hitBox = HitboxPool.Instance.GetHitbox();
        }
    }

    public void Dispose()
    {
        if (Application.isPlaying)
        {
            HitboxPool.Instance.ReturnHitbox(hitBox);
        }
    }

    public void Enable()
    {
        enabled = true;
        if (hitBox)
        {
            hitBox.SetHitboxActive(true);
        }
    }

    public void Disable()
    {
        enabled = false;
        if (hitBox)
        {
            hitBox.SetHitboxActive(false);
        }
    }
}