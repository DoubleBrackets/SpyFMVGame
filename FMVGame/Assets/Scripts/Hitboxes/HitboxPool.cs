using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class HitboxPool : MonoBehaviour
{
    public static HitboxPool Instance;
    private const uint POOL_SIZE = 15;

    [SerializeField]
    private Hitbox hitboxPrefab;

    private readonly Stack<Hitbox> hitboxPool = new();

    [ShowInInspector]
    private readonly List<Hitbox> usedHitboxes = new();


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one instance of HitboxPool exists! This should be a singleton");
            return;
        }

        Instance = this;

        InitializePool();
    }

    public Hitbox GetHitbox()
    {
        if (hitboxPool.Count == 0)
        {
            Debug.LogError("HitboxPool empty, unable to supply. How many hitboxes are you using, jeez");
            return null;
        }

        var grabbedHitbox = hitboxPool.Pop();
        grabbedHitbox.gameObject.SetActive(true);
        grabbedHitbox.Reset();
        usedHitboxes.Add(grabbedHitbox);
        return grabbedHitbox;
    }

    public void ReturnHitbox(Hitbox hitbox)
    {
        var returnedHitbox = usedHitboxes.Find(x => x == hitbox);
        if (returnedHitbox != null)
        {
            hitbox.gameObject.SetActive(false);
            usedHitboxes.Remove(hitbox);
            hitboxPool.Push(hitbox);
        }
        else
        {
            Debug.LogError($"Tried to return a hitbox {hitbox} that is not being used. What the heck??");
        }
    }

    private void InitializePool()
    {
        for (var i = 0; i < POOL_SIZE; i++)
        {
            var hitbox = Instantiate(hitboxPrefab, transform);
            hitbox.gameObject.SetActive(false);
            hitboxPool.Push(hitbox);
        }
    }
}