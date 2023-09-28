using UnityEngine;
using UnityEngine.Playables;

public class JumpReceiver : MonoBehaviour, INotificationReceiver
{
    private PlayableDirector director;

    public static double jumpDestinationTimeIgnoreMarkers;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        jumpDestinationTimeIgnoreMarkers = 0;
    }

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        var jumpMarker = notification as JumpMarker;

        // Pause notification receivers

        if (jumpMarker && jumpMarker.time >= jumpDestinationTimeIgnoreMarkers)
        {
            var destinationMarker = jumpMarker.destinationMarker;
            if (destinationMarker != null && destinationMarker.active)
            {
                jumpDestinationTimeIgnoreMarkers = 0;
                if (!jumpMarker.triggerNotifications)
                {
                    jumpDestinationTimeIgnoreMarkers = destinationMarker.time;
                }

                origin.GetGraph().GetRootPlayable(0).SetTime(destinationMarker.time);
            }
        }

        var jumpAction = notification as TimelineJumpAction;
        if (jumpAction != null)
        {
            jumpDestinationTimeIgnoreMarkers = 0;
            if (!jumpAction.triggerNotifications)
            {
                jumpDestinationTimeIgnoreMarkers = jumpAction.jumpTime;
            }

            origin.GetGraph().GetRootPlayable(0).SetTime(jumpAction.jumpTime);
        }
    }
}