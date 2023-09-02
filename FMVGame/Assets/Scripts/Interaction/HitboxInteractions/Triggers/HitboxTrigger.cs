using DoubleOhPew.Interactions.Core;
using UnityEngine;

public abstract class HitboxTrigger<TTriggerPose, THitbox, TCollider> : IInteractTrigger<TTriggerPose>
    where TTriggerPose : IHandlesDrawable where THitbox : Hitbox<TCollider> where TCollider : Collider2D
{
    protected THitbox hitBox;
    protected TTriggerPose pose;
    public abstract void UpdatePose(TTriggerPose pose);

    public bool EvaluateInteraction(InteractionInfo interactionInfo)
    {
        if (interactionInfo.interactionType == InteractionInfo.InteractionType.Click)
        {
            return hitBox.EvaluateHitPoint(interactionInfo.mouseWorldPos);
        }

        return false;
    }

    public abstract void Initialize();
    public abstract void Dispose();

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