using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DoubleOhPew.Interactions.Timeline
{
    public class BoxInteractAsset : PlayableAsset, ITimelineClipAsset
    {
        public BoxInteractBehaviour template;

        public ClipCaps clipCaps => ClipCaps.None;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<BoxInteractBehaviour>.Create(graph, template);
            return playable;
        }
    }
}