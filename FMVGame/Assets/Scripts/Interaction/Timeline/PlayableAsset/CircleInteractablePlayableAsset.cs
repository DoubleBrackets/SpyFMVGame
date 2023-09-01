using UnityEngine;
using UnityEngine.Playables;

namespace DoubleOhPew.Interactions.Timeline
{
    public class CircleInteractablePlayableAsset : PlayableAsset
    {
        public CircleColliderInteractableBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<CircleColliderInteractableBehaviour>.Create(graph, template);
            return playable;
        }
    }
}