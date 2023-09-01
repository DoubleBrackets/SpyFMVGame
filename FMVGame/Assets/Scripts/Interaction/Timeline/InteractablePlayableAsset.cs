using DoubleOhPew.Interactions.Timeline;
using UnityEngine;
using UnityEngine.Playables;

namespace DoubleOhPew.Interaction.Timeline
{
    public class InteractablePlayableAsset : PlayableAsset
    {
        public BoxColliderInteractableBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<BoxColliderInteractableBehaviour>.Create(graph, template);

            var interactablePlayableBehaviour = playable.GetBehaviour();

            return playable;
        }
    }
}