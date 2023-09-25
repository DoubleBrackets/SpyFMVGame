using UnityEngine;

/// <summary>
/// Class used as a temporary video player when editing timelines
/// Since we don't want to deal with the video player pool outside of play mode
/// </summary>
[ExecuteInEditMode]
public class EditorVideoPlayer : MonoBehaviour
{
    public static EditorVideoPlayer Instance;

    [SerializeField]
    private VideoPlayerHandler videoPlayer;

    [SerializeField]
    private VideoPlayerSizeConfig defaultSizeConfig;

    public VideoPlayerHandler VideoPlayer => videoPlayer;

    private void OnEnable()
    {
        Instance = this;
        VideoPlayer.SetSizing(defaultSizeConfig);
    }

    public VideoPlayerHandler PrepareAndGetVideoPlayer()
    {
        VideoPlayer.SetSizing(defaultSizeConfig);
        return VideoPlayer;
    }
}