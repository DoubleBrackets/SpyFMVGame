using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine.Timeline;

[CustomEditor(typeof(JumpMarker))]
public class JumpMarkerInspector : Editor
{
    private const string k_TimeLabel = "Timeline will jump at time {0}";
    private const string k_NoJumpLabel = "{0} is deactivated.";
    private const string k_AddMarker = "No Destination Marker has been found on this track. Add one to use this marker.";
    private const string k_JumpTo = "Jump to";
    private const string k_None = "None";

    private SerializedProperty m_DestinationMarker;
    private SerializedProperty m_EmitOnce;
    private SerializedProperty m_EmitInEditor;
    private SerializedProperty m_triggerNotifications;
    private SerializedProperty m_Time;

    private void OnEnable()
    {
        m_DestinationMarker = serializedObject.FindProperty("destinationMarker");
        m_EmitOnce = serializedObject.FindProperty("emitOnce");
        m_EmitInEditor = serializedObject.FindProperty("emitInEditor");
        m_Time = serializedObject.FindProperty("m_Time");
        m_triggerNotifications = serializedObject.FindProperty("triggerNotifications");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var marker = target as JumpMarker;

        using (var changeScope = new EditorGUI.ChangeCheckScope())
        {
            EditorGUILayout.PropertyField(m_Time);
            EditorGUILayout.Space();

            var destinationMarkers = DestinationMarkersFor(marker);
            if (!destinationMarkers.Any())
            {
                DrawNoJump();
            }
            else
            {
                DrawJumpOptions(destinationMarkers);
            }

            if (changeScope.changed)
            {
                serializedObject.ApplyModifiedProperties();
                TimelineEditor.Refresh(RefreshReason.ContentsModified);
            }
        }
    }

    private void DrawNoJump()
    {
        EditorGUILayout.HelpBox(k_AddMarker, MessageType.Info);

        using (new EditorGUI.DisabledScope(true))
        {
            EditorGUILayout.Popup(k_JumpTo, 0, new[] { k_None });
            EditorGUILayout.PropertyField(m_EmitOnce);
            EditorGUILayout.PropertyField(m_EmitInEditor);
            EditorGUILayout.PropertyField(m_triggerNotifications);
        }
    }

    private void DrawJumpOptions(IList<DestinationMarker> destinationMarkers)
    {
        var destinationMarker = DrawDestinationPopup(destinationMarkers);
        DrawTimeLabel(destinationMarker);
        EditorGUILayout.PropertyField(m_EmitOnce);
        EditorGUILayout.PropertyField(m_EmitInEditor);
        EditorGUILayout.PropertyField(m_triggerNotifications);
    }

    private DestinationMarker DrawDestinationPopup(IList<DestinationMarker> destinationMarkers)
    {
        var popupIndex = 0;
        var destinationMarkerIndex = destinationMarkers.IndexOf(m_DestinationMarker.objectReferenceValue as DestinationMarker);
        if (destinationMarkerIndex != -1)
        {
            popupIndex = destinationMarkerIndex + 1;
        }

        DestinationMarker destinationMarker = null;
        using (var changeScope = new EditorGUI.ChangeCheckScope())
        {
            var newIndex = EditorGUILayout.Popup(k_JumpTo, popupIndex, GeneratePopupOptions(destinationMarkers).ToArray());

            if (newIndex > 0)
            {
                destinationMarker = destinationMarkers.ElementAt(newIndex - 1);
            }

            if (changeScope.changed)
            {
                m_DestinationMarker.objectReferenceValue = destinationMarker;
            }
        }

        return destinationMarker;
    }

    private static void DrawTimeLabel(DestinationMarker destinationMarker)
    {
        if (destinationMarker != null)
        {
            if (destinationMarker.active)
            {
                EditorGUILayout.HelpBox(string.Format(k_TimeLabel, destinationMarker.time.ToString("0.##")), MessageType.Info);
            }
            else
            {
                EditorGUILayout.HelpBox(string.Format(k_NoJumpLabel, destinationMarker.name), MessageType.Warning);
            }
        }
    }

    private static IList<DestinationMarker> DestinationMarkersFor(Marker marker)
    {
        var destinationMarkers = new List<DestinationMarker>();
        var parent = marker.parent;
        if (parent != null)
        {
            destinationMarkers.AddRange(parent.GetMarkers().OfType<DestinationMarker>().ToList());
        }

        return destinationMarkers;
    }

    private static IEnumerable<string> GeneratePopupOptions(IEnumerable<DestinationMarker> markers)
    {
        yield return k_None;

        foreach (var marker in markers)
        {
            yield return marker.name;
        }
    }
}