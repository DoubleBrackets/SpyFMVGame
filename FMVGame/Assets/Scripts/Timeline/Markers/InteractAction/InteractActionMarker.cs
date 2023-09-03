using DoubleOhPew.Interactions.Core;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class InteractActionMarker : Marker, INotification
{
    [SerializeReference]
    public IInteractAction action;

    public PropertyName id { get; }
}