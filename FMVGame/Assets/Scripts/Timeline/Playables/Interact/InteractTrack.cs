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
        protected override void OnCreateClip(TimelineClip clip)
        {
            var playableAssetType = clip.asset.GetType();
            if (playableAssetType == typeof(BoxInteractAsset))
            {
                TimelineUtilities.CreateEmptyCurvesOnTemplate(
                    clip, playableAssetType,
                    ("pose.zAngle", 0),
                    ("pose.position.x", 0),
                    ("pose.position.y", 0),
                    ("pose.size.x", 0),
                    ("pose.size.y", 0)
                );
            }
            else if (playableAssetType == typeof(CircleInteractAsset))
            {
                TimelineUtilities.CreateEmptyCurvesOnTemplate(
                    clip, playableAssetType,
                    ("pose.radius", 0),
                    ("pose.position.x", 0),
                    ("pose.position.y", 0)
                );
            }
            else if (playableAssetType == typeof(CapsuleInteractAsset))
            {
                TimelineUtilities.CreateEmptyCurvesOnTemplate(
                    clip, playableAssetType,
                    ("pose.zAngle", 0),
                    ("pose.position.x", 0),
                    ("pose.position.y", 0),
                    ("pose.size.x", 0),
                    ("pose.size.y", 0)
                );
            }


            base.OnCreateClip(clip);
        }
    }
}