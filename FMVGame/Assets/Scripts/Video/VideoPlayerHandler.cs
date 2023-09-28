using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerHandler : MonoBehaviour, IPoolable
{
    [field: SerializeField]
    public Renderer RenderingSurface { get; private set; }

    [field: SerializeField]
    public VideoPlayer VideoPlayer { get; private set; }

    private bool isSetUp;

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

    /// <summary>
    /// If this handler has not been set up yet, set it up with the given clip
    /// Player handlers are intended to be set up only once; if you want to set it up again with a different clip
    /// use a new instance of a video player handler from the pool
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="loop"></param>
    /// <param name="mute"></param>
    public void SetupPlayerWithClip(VideoClip clip, bool loop, bool mute)
    {
        if (isSetUp)
        {
            return;
        }

        VideoPlayer.source = VideoSource.VideoClip;
        VideoPlayer.clip = clip;
        VideoPlayer.playOnAwake = false;
        VideoPlayer.waitForFirstFrame = true;
        VideoPlayer.isLooping = loop;

        for (ushort i = 0; i < clip.audioTrackCount; ++i)
        {
            if (VideoPlayer.audioOutputMode == VideoAudioOutputMode.Direct)
            {
                VideoPlayer.SetDirectAudioMute(i, mute);
                VideoPlayer.SetDirectAudioVolume(i, 1);
            }
            else if (VideoPlayer.audioOutputMode == VideoAudioOutputMode.AudioSource)
            {
                var audioSource = VideoPlayer.GetTargetAudioSource(i);
                if (audioSource != null)
                {
                    audioSource.mute = mute;
                }
            }
        }

        isSetUp = true;
    }

    public void PrepareVideoAtTime(double time, Action<VideoPlayer> prepareComplete = null)
    {
        VideoPlayer.time = time;

        void HandlePrepareComplete(VideoPlayer source)
        {
            source.prepareCompleted -= HandlePrepareComplete;

            // Playing and pausing prevents some weird delays
            source.Play();
            source.Pause();
            prepareComplete?.Invoke(source);
        }

        VideoPlayer.prepareCompleted += HandlePrepareComplete;
        VideoPlayer.Prepare();
    }

    public void Initialize()
    {
        OnReturnedToPool();
    }

    public void OnRetrievedFromPool()
    {
        isSetUp = false;
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