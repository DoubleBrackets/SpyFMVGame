using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "VideoPlayerSizeConfig", fileName = "VideoPlayerSizeConfig")]
public class VideoPlayerSizeConfig : ScriptableObject
{
    [InfoBox("Configure the video player resolution here. " +
             "This should be used to calculate the scale of the video player surface." +
             "This should also be used to calculate Camera ortho sizing dynamically", InfoMessageType.None), Header("Aspect Ratio"), SerializeField]
    private float videoWidth;

    [SerializeField]
    private float videoHeight;

    [Header("Sizing"), SerializeField]
    private float surfaceWidth;

    /// <summary>
    /// Return the ortho height needed to frame the video player
    /// </summary>
    /// <param name="screenResolution"></param>
    /// <returns></returns>
    public float GetCameraOrthoHeight(Vector2 screenResolution)
    {
        var screenAspectRatio = screenResolution.y / screenResolution.x;
        var videoAspectRatio = videoHeight / videoWidth;

        // Width bound (screen is narrower than video)
        if (screenAspectRatio > videoAspectRatio)
        {
            return surfaceWidth * screenAspectRatio / 2;
        }

        // Height bound (screen is wider than window)
        return surfaceWidth * videoAspectRatio / 2;
    }

    /// <summary>
    /// Resize video player to match constraints
    /// </summary>
    public Vector3 GetVideoPlayerSurfaceScale() =>
        new(
            surfaceWidth,
            surfaceWidth * videoHeight / videoWidth,
            1
        );
}