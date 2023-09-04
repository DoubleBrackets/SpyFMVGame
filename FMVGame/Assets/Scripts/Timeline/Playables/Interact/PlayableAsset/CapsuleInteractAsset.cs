using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DoubleOhPew.Interactions.Timeline
{
    public class CapsuleInteractAsset : PlayableAsset, ITimelineClipAsset
    {
        public CapsuleInteractBehaviour template;

        public ClipCaps clipCaps => ClipCaps.None;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<CapsuleInteractBehaviour>.Create(graph, template);
            return playable;
        }
    }
}