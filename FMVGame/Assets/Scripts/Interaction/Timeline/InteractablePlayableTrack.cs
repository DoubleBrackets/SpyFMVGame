using System;
using UnityEngine.Timeline;

namespace DoubleOhPew.Interactions.Timeline
{
    [Serializable,
     TrackClipType(typeof(BoxInteractAsset)),
     TrackClipType(typeof(CapsuleInteractAsset)),
     TrackClipType(typeof(CircleInteractAsset)),
     TrackBindingType(typeof(InteractionManager)),
     TrackColor(1, 0.776f, 0.255f)]
    public class InteractablePlayableTrack : PlayableTrack
    {
    }
}