using DoubleOhPew.Interactions.Timeline;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoxInteractablePlayableAsset))]
public class BoxInteractablePlayableAssetEditor : Editor
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
        var template = (target as BoxInteractablePlayableAsset)?.template;

        var pose = template.pose;
    }
}

[CustomEditor(typeof(CircleInteractablePlayableAsset))]
public class CircleInteractablePlayableAssetEditor : Editor
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
        var so = new SerializedObject(target);

        so.Update();

        var template = (target as CircleInteractablePlayableAsset)?.template;

        var pose = template.pose;

        Handles.color = Color.red;

        // We need to handle the dirty checks ourselves to prevent unwanted keyframing 
        // When editing animation curves
        var newRadius = Handles.RadiusHandle(Quaternion.AngleAxis(0, Vector3.forward), pose.position, pose.radius);
        Vector2 newPos = Handles.PositionHandle(pose.position, Quaternion.identity);

        if (newRadius != pose.radius)
        {
            Undo.RecordObject(target, "Edit Circle Interactable Pose");
            template.pose.radius = newRadius;
        }

        if (newPos != pose.position)
        {
            Undo.RecordObject(target, "Edit Circle Interactable Pose");
            template.pose.position = newPos;
        }

        if (so.ApplyModifiedProperties())
        {
            Repaint();
        }
    }
}