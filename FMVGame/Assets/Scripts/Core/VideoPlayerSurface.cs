using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerSurface : MonoBehaviour
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
}
