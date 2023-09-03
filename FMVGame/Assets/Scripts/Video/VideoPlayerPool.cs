using UnityEngine;

public class VideoPlayerPool : MonoBehaviour
{
    public static VideoPlayerPool Instance;

    [SerializeField]
    private VideoPlayerHandler prefab;

    [SerializeField]
    private uint poolSize;

    private PrefabComponentPool<VideoPlayerHandler> pool;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Duplicate singleton");
            return;
        }

        Instance = this;

        pool = new PrefabComponentPool<VideoPlayerHandler>(prefab, poolSize, transform);
    }

    public VideoPlayerHandler GetVideoPlayer() => pool.GetFromPool();
    public void ReturnVideoPlayer(VideoPlayerHandler videoPlayerHandler) => pool.ReturnToPool(videoPlayerHandler);
}