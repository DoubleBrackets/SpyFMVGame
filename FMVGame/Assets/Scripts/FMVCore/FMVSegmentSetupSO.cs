using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "FMVSegmentSetup", fileName = "SegmentSetup")]
public class FMVSegmentSetupSO : ScriptableObject
{
    [field: SerializeField]
    public TimelineAsset TimelineAsset { get; set; }

#if UNITY_EDITOR
    [MenuItem("Assets/CreateSegmentSetup", true)]
    private static bool CreateSegmentSetupValidation() =>
        Selection.activeObject is TimelineAsset;

    [MenuItem("Assets/CreateSegmentSetup", false, 1000)]
    private static void CreateSegmentSetupMenuItem()
    {
        var sourceTimeline = Selection.activeObject as TimelineAsset;
        if (sourceTimeline == null)
        {
            return;
        }

        var path = Path.GetDirectoryName(AssetDatabase.GetAssetPath(sourceTimeline));
        var name = sourceTimeline.name;

        var segmentSetup = CreateInstance<FMVSegmentSetupSO>();
        segmentSetup.TimelineAsset = sourceTimeline;
        segmentSetup.name = name;

        path += $"/{name}.asset";

        AssetDatabase.CreateAsset(segmentSetup, path);
        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/Open In Scene", true)]
    private static bool OpenInSceneValidation() =>
        Selection.activeObject is TimelineAsset || Selection.activeObject is FMVSegmentSetupSO;

    [MenuItem("Assets/Open In Scene", false, 1000)]
    private static void OpenInSceneMenuItem()
    {
        var sourceTimeline = Selection.activeObject as TimelineAsset;
        if (sourceTimeline == null)
        {
            sourceTimeline = (Selection.activeObject as FMVSegmentSetupSO).TimelineAsset;
        }

        var target = FindObjectsOfType<PlayableDirector>().First(x => x.gameObject.tag == "Debug");

        if (target)
        {
            target.playableAsset = sourceTimeline;
            Selection.activeObject = target;
        }
    }

#endif
}