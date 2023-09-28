using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Pool for video players in the editor, using premade instances
/// Since we don't want to deal instantiating and destroying video players in the editor
/// </summary>
[ExecuteInEditMode]
public class EditorVideoPool : MonoBehaviour
{
    public static EditorVideoPool Instance;

    [SerializeField]
    private List<VideoPlayerHandler> sourceVideoPlayers;

    [SerializeField]
    private VideoPlayerSizeConfig defaultSizeConfig;

    private Stack<VideoPlayerHandler> objectPool = new();

    private Dictionary<string, VideoPlayerHandler> activeObjects = new();

    private string softReturnKey = string.Empty;

    private void OnEnable()
    {
#if !UNITY_EDITOR
        Destroy(gameObject);
        return;
#endif

        Instance = this;
        UpdateVideoSizing();
        ResetPool();
    }

    [Button("Reset Pool")]
    private void ResetPool()
    {
        Instance = this;

        objectPool = new Stack<VideoPlayerHandler>();
        activeObjects = new Dictionary<string, VideoPlayerHandler>();

        softReturnKey = string.Empty;

        foreach (var videoPlayer in sourceVideoPlayers)
        {
            videoPlayer.Initialize();
            objectPool.Push(videoPlayer);
        }
    }

    [Button("Update Video Renderer Sizing")]
    private void UpdateVideoSizing()
    {
        foreach (var videoPlayer in sourceVideoPlayers)
        {
            videoPlayer.SetSizing(defaultSizeConfig);
        }
    }

    public VideoPlayerHandler GetFromPool(string key)
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

    public void ReturnToPoolSoft(string key)
    {
        activeObjects.TryGetValue(key, out var instance);
        if (instance != null)
        {
            instance.Pause();
            instance.RenderingSurface.transform.position = new Vector3(0, 0, 1);

            if (softReturnKey != key)
            {
                activeObjects.TryGetValue(softReturnKey, out var currentSoftInstance);
                if (currentSoftInstance != null)
                {
                    activeObjects.Remove(softReturnKey);
                    objectPool.Push(currentSoftInstance);
                }

                softReturnKey = key;
            }
        }
        else
        {
            Debug.LogError($"Tried to return instance {instance} that is not being used. What the heck??");
        }
    }
}