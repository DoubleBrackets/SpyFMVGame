using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Variant of prefab component pool that uses key handles instead of the instance itself
/// Used for preloading, mainly
/// </summary>
/// <typeparam name="T"></typeparam>
public class KeyPrefabComponentPool<T> where T : Component, IPoolable
{
    private readonly Stack<T> objectPool = new();

    private readonly Dictionary<string, T> activeObjects = new();

    public KeyPrefabComponentPool(T prefab, uint staticPoolSize, Transform parentTransform)
    {
        for (var i = 0; i < staticPoolSize; i++)
        {
            var instance = Object.Instantiate(prefab, parentTransform);
            instance.Initialize();
            objectPool.Push(instance);
        }
    }

    public T GetFromPool(string key)
    {
        // Already in active objects (e.g from preloading)
        if (activeObjects.ContainsKey(key))
        {
            return activeObjects[key];
        }

        // Empty
        if (objectPool.Count == 0)
        {
            Debug.LogError("Pool empty, cannot get a new instance!");
            return null;
        }

        // New object; add to active pool
        var instance = objectPool.Pop();
        activeObjects.Add(key, instance);
        instance.OnRetrievedFromPool();
        return instance;
    }

    public void ReturnToPool(string key)
    {
        activeObjects.TryGetValue(key, out var instance);
        if (instance != null)
        {
            instance.OnReturnedToPool();
            activeObjects.Remove(key);
            objectPool.Push(instance);
        }
        else
        {
            Debug.LogError($"Tried to return instance {instance} that is not being used. What the heck??");
        }
    }

    public IEnumerable<T> GetAll() => activeObjects.Values.Concat(objectPool);
}