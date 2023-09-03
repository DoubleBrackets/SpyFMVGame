using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

public class FMVGameDirector : MonoBehaviour
{
    [Serializable]
    public struct DebugSetup
    {
        public bool loadDebugSegment;
        public PlayableDirector director;
        public FMVSegmentSetupSO debugSegmentSetupSo;
    }

    public static FMVGameDirector Instance;

    [SerializeField]
    private SegmentController controller;

    [SerializeField, Toggle("loadDebugSegment")]
    private DebugSetup debugSetup;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("An instance already exists");
        }

        Instance = this;
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        debugSetup.director.playableAsset = debugSetup.debugSegmentSetupSo.TimelineAsset;
#endif
    }

    private void Start()
    {
#if UNITY_EDITOR
        if (debugSetup.loadDebugSegment)
        {
            PlaySegment(debugSetup.debugSegmentSetupSo);
        }
#endif
    }

    public void PlaySegment(FMVSegmentSetupSO segmentSetup)
    {
        controller.LoadSegment(segmentSetup);
    }
}