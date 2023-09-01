using UnityEngine;
using UnityEngine.Playables;

namespace DoubleOhPew.Interactions.Timeline
{
    public class BoxInteractablePlayableAsset : PlayableAsset
    {
        public BoxColliderInteractableBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<BoxColliderInteractableBehaviour>.Create(graph, template);
            return playable;
        }
    }
}