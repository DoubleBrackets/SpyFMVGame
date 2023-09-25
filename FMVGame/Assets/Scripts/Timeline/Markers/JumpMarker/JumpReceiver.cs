using UnityEngine;
using UnityEngine.Playables;

public class JumpReceiver : MonoBehaviour, INotificationReceiver
{
    private PlayableDirector director;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        var jumpMarker = notification as JumpMarker;

        // Pause and resume to avoid triggering notifications
        if (jumpMarker)
        {
            var destinationMarker = jumpMarker.destinationMarker;
            if (destinationMarker != null && destinationMarker.active)
            {
                director.Pause();
                director.time = destinationMarker.time;
                director.Resume();
            }
        }

        var jumpAction = notification as TimelineJumpAction;
        if (jumpAction != null)
        {
            director.Pause();
            director.time = jumpAction.jumpTime;
            director.Resume();
        }
    }
}