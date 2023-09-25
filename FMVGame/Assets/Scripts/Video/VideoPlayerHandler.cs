using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerHandler : MonoBehaviour, IPoolable
{
    [field: SerializeField]
    public Renderer RenderingSurface { get; private set; }

    [field: SerializeField]
    public VideoPlayer VideoPlayer { get; private set; }

    [Button("Play")]
    public void Play()
    {
        RenderingSurface.enabled = true;
        VideoPlayer.Play();
    }

    [Button("Stop")]
    public void Stop()
    {
        RenderingSurface.enabled = false;
        VideoPlayer.Stop();
    }

    [Button("Pause")]
    public void Pause()
    {
        VideoPlayer.Pause();
    }

    public void Initialize()
    {
        OnReturnedToPool();
    }

    public void OnRetrievedFromPool()
    {
        gameObject.SetActive(true);
    }

    public void OnReturnedToPool()
    {
        Stop();
        gameObject.SetActive(false);
        VideoPlayer.clip = null;
    }

    public VideoPlayerHandler SetSizing(VideoPlayerSizeConfig sizeConfig)
    {
        VideoPlayer.transform.localScale = sizeConfig.GetVideoPlayerSurfaceScale();
        return this;
    }
}