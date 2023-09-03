using System;
using UnityEngine;

public class HitboxPools : MonoBehaviour
{
    public static HitboxPools Instance;

    [SerializeField]
    private uint poolSize;

    [Serializable]
    public class HitboxPoolConfig<THitbox> where THitbox : MonoBehaviour, IPoolable
    {
        public THitbox hitboxPrefab;
        public Transform hitboxParentTransform;
        public PrefabComponentPool<THitbox> CreatePool(uint poolSize) => new(hitboxPrefab, poolSize, hitboxParentTransform);
    }

    public HitboxPoolConfig<BoxHitbox> boxHitboxPoolConfig;
    public PrefabComponentPool<BoxHitbox> boxHitboxPool;

    public HitboxPoolConfig<CapsuleHitbox> capsuleHitboxPoolConfig;
    public PrefabComponentPool<CapsuleHitbox> capsuleHitboxPool;

    public HitboxPoolConfig<CircleHitbox> circleHitboxPoolConfig;
    public PrefabComponentPool<CircleHitbox> circleHitboxPool;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one instance of HitboxPool exists! This should be a singleton");
            return;
        }

        Instance = this;

        boxHitboxPool = boxHitboxPoolConfig.CreatePool(poolSize);
        capsuleHitboxPool = capsuleHitboxPoolConfig.CreatePool(poolSize);
        circleHitboxPool = circleHitboxPoolConfig.CreatePool(poolSize);
    }
}