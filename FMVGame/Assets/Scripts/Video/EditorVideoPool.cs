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

    // We use a queue to make sure the newest instance isn't reset
    // When we edit the hitbox through timeline, it deletes and recreates the playable
    // If it reuses the same video from the pool, it'll basically remove the visual the editor is using to trace the hitbox
    // The queue makes it such that the old instance remains unused, and stays visible
    private Queue<VideoPlayerHandler> objectPool = new();

    private List<VideoPlayerHandler> activeObjects = new();

    private VideoPlayerHandler heldVideoPlayer;

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

        objectPool = new Queue<VideoPlayerHandler>();
        activeObjects = new List<VideoPlayerHandler>();

        heldVideoPlayer = null;

        foreach (var videoPlayer in sourceVideoPlayers)
        {
            videoPlayer.Initialize();
            objectPool.Enqueue(videoPlayer);
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

    public VideoPlayerHandler GetFromPool()
    {
        if (objectPool.Count == 0)
        {
            Debug.LogError("Pool empty, cannot get a new instance!");
            return null;
        }


        var instance = objectPool.Dequeue();
        instance.RenderingSurface.transform.position = new Vector3(0, 0, 0);
        activeObjects.Add(instance);
        instance.OnRetrievedFromPool();
        return instance;
    }

    public void ReturnToPool(VideoPlayerHandler instance)
    {
        var foundInstance = activeObjects.Find(x => x == instance);
        if (foundInstance != null)
        {
            instance.OnReturnedToPool();
            activeObjects.Remove(instance);
            objectPool.Enqueue(instance);
        }
        else
        {
            Debug.LogError($"Tried to return instance {instance} that is not being used. What the heck??");
        }
    }

    public void ReturnToPoolSoft(VideoPlayerHandler instance)
    {
        var foundInstance = activeObjects.Find(x => x == instance);
        if (foundInstance != null)
        {
            instance.Pause();
            instance.RenderingSurface.transform.position = new Vector3(0, 0, 1);
            if (heldVideoPlayer)
            {
                activeObjects.Remove(heldVideoPlayer);
                objectPool.Enqueue(heldVideoPlayer);
            }

            heldVideoPlayer = instance;
        }
        else
        {
            Debug.LogError($"Tried to return instance {instance} that is not being used. What the heck??");
        }
    }
}