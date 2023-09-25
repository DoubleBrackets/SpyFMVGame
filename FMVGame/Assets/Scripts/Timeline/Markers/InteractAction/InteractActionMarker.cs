using DoubleOhPew.Interactions.Core;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class InteractActionMarker : Marker, INotification, INotificationOptionProvider
{
    [SerializeReference]
    public IInteractAction action;

    [SerializeField] public bool emitOnce;
    [SerializeField] public bool emitInEditor;
    [SerializeField] public bool retroactive;

    public PropertyName id { get; }

    NotificationFlags INotificationOptionProvider.flags =>
        (emitOnce ? NotificationFlags.TriggerOnce : default) |
        (emitInEditor ? NotificationFlags.TriggerInEditMode : default) |
        (retroactive ? NotificationFlags.Retroactive : default);
}