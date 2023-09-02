using DoubleOhPew.Interactions.Timeline;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoxInteractAsset))]
public class BoxInteractAssetEditor : Editor
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

        var template = (target as CapsuleInteractAsset)?.template;

        var tempPose = template.pose;
        var prevPose = template.pose;
        prevPose.zAngle %= 360f;

        Handles.color = Color.green;

        DebugExtensions.CalculateCapsulePoints(tempPose.position, tempPose.size, tempPose.zAngle,
            out var p1, out var p2);


        tempPose.size.x = 2 * Handles.RadiusHandle(Quaternion.AngleAxis(prevPose.zAngle, Vector3.forward), p1, prevPose.size.x / 2f);
        tempPose.size.x = 2 * Handles.RadiusHandle(Quaternion.AngleAxis(prevPose.zAngle, Vector3.forward), p2, tempPose.size.x / 2f);

        tempPose.zAngle = Handles.DoRotationHandle(Quaternion.Euler(0, 0, prevPose.zAngle), prevPose.position).eulerAngles.z % 360f;

        tempPose.position = Handles.PositionHandle(prevPose.position, Quaternion.identity);


        if (Mathf.Abs(tempPose.zAngle - prevPose.zAngle) < 5f)
        {
            tempPose.zAngle = prevPose.zAngle;
        }

        // We need to handle the dirty checks ourselves to prevent unwanted keyframing 
        // When editing animation curves
        if (tempPose != template.pose)
        {
            Undo.RecordObject(target, "Edit Capsule Interact Pose");
            template.pose = tempPose;
        }

        if (so.ApplyModifiedProperties())
        {
            Repaint();
        }
    }
}