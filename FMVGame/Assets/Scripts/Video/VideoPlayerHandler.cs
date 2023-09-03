using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerHandler : MonoBehaviour, IPoolable
{
    [field: SerializeField]
    public Renderer RenderingSurface { get; private set; }

    [field: SerializeField]
    public VideoPlayer VideoPlayer { get; private set; }

    public void Play()
    {
        VideoPlayer.Play();
        RenderingSurface.enabled = true;
    }

    public void Stop()
    {
        VideoPlayer.Stop();
        RenderingSurface.enabled = false;
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
        gameObject.SetActive(false);
    }
}