using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ShotDirector : MonoBehaviour
{
    [SerializeField]
    private TimelineAsset setupTimeline;

    [SerializeField]
    private PlayableDirector director;

    private void Awake()
    {
        director.playableAsset = setupTimeline;
        director.Play();
    }
}