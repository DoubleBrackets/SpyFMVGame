using UnityEngine;

public class VideoPlayerPool : MonoBehaviour
{
    public static VideoPlayerPool Instance;

    [SerializeField]
    private VideoPlayerHandler prefab;

    [SerializeField]
    private VideoPlayerSizeConfig defaultSizeConfig;

    [SerializeField]
    private uint poolSize;

    private KeyPrefabComponentPool<VideoPlayerHandler> pool;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Duplicate singleton");
            return;
        }

        Instance = this;

        pool = new KeyPrefabComponentPool<VideoPlayerHandler>(prefab, poolSize, transform);

        foreach (var videoPlayer in pool.GetAll())
        {
            videoPlayer.SetSizing(defaultSizeConfig);
        }
    }

    public VideoPlayerHandler GetVideoPlayer(string key) => pool.GetFromPool(key).SetSizing(defaultSizeConfig);

    public void ReturnVideoPlayer(string key) => pool.ReturnToPool(key);
}