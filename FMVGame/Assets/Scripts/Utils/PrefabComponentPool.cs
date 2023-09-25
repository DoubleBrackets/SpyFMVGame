using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrefabComponentPool<T> where T : Component, IPoolable
{
    private readonly Stack<T> objectPool = new();

    private readonly List<T> activeObjects = new();

    public PrefabComponentPool(T prefab, uint staticPoolSize, Transform parentTransform)
    {
        for (var i = 0; i < staticPoolSize; i++)
        {
            var instance = Object.Instantiate(prefab, parentTransform);
            instance.Initialize();
            objectPool.Push(instance);
        }
    }

    public T GetFromPool()
    {
        if (objectPool.Count == 0)
        {
            Debug.LogError("Pool empty, cannot get a new instance!");
            return null;
        }

        var instance = objectPool.Pop();
        activeObjects.Add(instance);
        instance.OnRetrievedFromPool();
        return instance;
    }

    public void ReturnToPool(T instance)
    {
        var returnedInstance = activeObjects.Find(x => x == instance);
        if (returnedInstance != null)
        {
            instance.OnReturnedToPool();
            activeObjects.Remove(instance);
            objectPool.Push(instance);
        }
        else
        {
            Debug.LogError($"Tried to return instance {instance} that is not being used. What the heck??");
        }
    }

    public IEnumerable<T> GetAll() => activeObjects.Concat(objectPool);
}