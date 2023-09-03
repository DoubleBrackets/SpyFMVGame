using UnityEngine;
using UnityEngine.Playables;

public class InteractActionNotificationReceiver : MonoBehaviour, INotificationReceiver
{
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        var actionMarker = notification as InteractActionMarker;
        if (actionMarker != null)
        {
            actionMarker.action.TriggerAction(new InteractionInfo
            {
                interactionType = InteractionInfo.InteractionType.TimelineMarker
            });
        }
    }
}