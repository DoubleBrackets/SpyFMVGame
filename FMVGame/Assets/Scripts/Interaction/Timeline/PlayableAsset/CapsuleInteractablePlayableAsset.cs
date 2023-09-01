using UnityEngine;
using UnityEngine.Playables;

namespace DoubleOhPew.Interactions.Timeline
{
    public class CapsuleInteractablePlayableAsset : PlayableAsset
    {
        public CapsuleColliderInteractableBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<CapsuleColliderInteractableBehaviour>.Create(graph, template);
            return playable;
        }
    }
}