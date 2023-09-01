using DoubleOhPew.Interaction.Timeline;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractablePlayableAsset))]
public class InteractablePlayableAssetEditor : Editor
{
    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnSceneGUI(SceneView obj)
    {
        var template = (target as InteractablePlayableAsset)?.template;

        DebugExtensions.DrawHandleRectangle(template.pose.position, template.pose.size, template.pose.zRot, Color.red);
    }
}