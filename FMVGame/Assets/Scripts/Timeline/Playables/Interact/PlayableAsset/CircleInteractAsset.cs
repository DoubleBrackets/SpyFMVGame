using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DoubleOhPew.Interactions.Timeline
{
    public class CircleInteractAsset : PlayableAsset, ITimelineClipAsset
    {
        public CircleInteractBehaviour template;

        public ClipCaps clipCaps => ClipCaps.None;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<CircleInteractBehaviour>.Create(graph, template);
            return playable;
        }
    }
}