using System;
using UnityEngine;

public class Hitbox<TCollider> : Hitbox where TCollider : Collider2D
{
    [SerializeField]
    protected TCollider attachedCollider;

    public event Action OnDrawGizmo;

    public bool EvaluateHitPoint(Vector2 worldPoint) => attachedCollider.enabled && attachedCollider.OverlapPoint(worldPoint);

    public void SetHitboxActive(bool active)
    {
        attachedCollider.enabled = active;
    }

    private void OnDrawGizmos()
    {
        if (attachedCollider.enabled)
        {
            OnDrawGizmo?.Invoke();
        }
    }
}

public class Hitbox : MonoBehaviour, IPoolable
{
    public void Initialize()
    {
        gameObject.SetActive(false);
    }

    public void OnRetrievedFromPool()
    {
        gameObject.SetActive(true);
    }

    public void OnReturnedToPool()
    {
        gameObject.SetActive(false);
    }
}