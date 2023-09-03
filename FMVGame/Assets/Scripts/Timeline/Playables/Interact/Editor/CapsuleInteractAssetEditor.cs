using DoubleOhPew.Interactions.Timeline;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;

[CustomEditor(typeof(CapsuleInteractAsset))]
public class CapsuleInteractAssetEditor : OdinEditor
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
        var template = (target as CapsuleInteractAsset)?.template;

        var tempPose = template.pose;
        var prevPose = template.pose;

        Handles.color = Color.green;

        Vector3 pos = tempPose.position;
        var rot = Quaternion.Euler(0, 0, tempPose.zAngle);
        var scale = (Vector3)tempPose.size;

        Handles.TransformHandle(ref pos, ref rot, ref scale);

        tempPose.size.x = scale.x;
        tempPose.size.y = scale.y;

        tempPose.position = pos;

        tempPose.zAngle = prevPose.zAngle + Mathf.DeltaAngle(prevPose.zAngle, rot.eulerAngles.z);

        if (Mathf.Abs(tempPose.zAngle - prevPose.zAngle) < 1f)
        {
            tempPose.zAngle = prevPose.zAngle;
        }

        // We need to handle the dirty checks ourselves to prevent unwanted keyframing 
        // When editing animation curves
        if (tempPose != template.pose)
        {
            Undo.RecordObject(target, "Edit Capsule Interact Pose");
            template.pose = tempPose;
            Repaint();
            TimelineEditor.Refresh(RefreshReason.ContentsModified);
        }
    }
}