using DoubleOhPew.Interactions.Timeline;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CircleInteractAsset))]
public class CircleInteractAssetEditor : Editor
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

        var template = (target as CircleInteractAsset)?.template;

        var tempPose = template.pose;

        Handles.color = Color.green;


        tempPose.radius = Handles.RadiusHandle(Quaternion.AngleAxis(0, Vector3.forward), tempPose.position, tempPose.radius);
        tempPose.position = Handles.PositionHandle(tempPose.position, Quaternion.identity);

        // We need to handle the dirty checks ourselves to prevent unwanted keyframing 
        // When editing animation curves
        if (tempPose != template.pose)
        {
            Undo.RecordObject(target, "Edit Circle Interact Pose");
            template.pose = tempPose;
        }

        if (so.ApplyModifiedProperties())
        {
            Repaint();
        }
    }
}