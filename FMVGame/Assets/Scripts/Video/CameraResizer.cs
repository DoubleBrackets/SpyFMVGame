using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    [SerializeField]
    private VideoPlayerSizeConfig defaultSizeConfig;

    [SerializeField]
    private Camera targetCamera;

    private void Update()
    {
        TryResize();
    }

    private void OnValidate()
    {
        TryResize();
    }

    private void TryResize()
    {
        var newResolution = new Vector2(targetCamera.scaledPixelWidth, targetCamera.scaledPixelHeight);
        var orthoHeight = defaultSizeConfig.GetCameraOrthoHeight(newResolution);
        if (Math.Abs(orthoHeight - targetCamera.orthographicSize) > 0.0001)
        {
            targetCamera.orthographicSize = orthoHeight;
        }
    }

    [Button("Resize")]
    private void ContextMenuResize()
    {
        TryResize();
    }
}