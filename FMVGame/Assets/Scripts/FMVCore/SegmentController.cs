using UnityEngine;
using UnityEngine.Playables;

public class SegmentController : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector director;

    public void LoadSegment(FMVSegmentSetupSO segmentSetup)
    {
        director.playableAsset = segmentSetup.TimelineAsset;
        director.Play();
    }
}