using DoubleOhPew.Interactions.Core;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineJumpAction : IInteractAction, INotification
{
    public float jumpTime;
    public bool triggerNotifications;

    public void TriggerAction(InteractionInfo info)
    {
        // Timeline output 0 is usually the marker track, so we'll use that to push the notif
        info.sourcePlayable.GetGraph().GetOutput(0).PushNotification(info.sourcePlayable, this);
    }

    public PropertyName id { get; }
}