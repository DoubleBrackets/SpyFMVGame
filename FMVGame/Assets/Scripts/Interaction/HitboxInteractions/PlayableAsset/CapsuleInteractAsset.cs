using UnityEngine;
using UnityEngine.Playables;

namespace DoubleOhPew.Interactions.Timeline
{
    public class CapsuleInteractAsset : PlayableAsset
    {
        public CapsuleInteractBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<CapsuleInteractBehaviour>.Create(graph, template);
            return playable;
        }
    }
}