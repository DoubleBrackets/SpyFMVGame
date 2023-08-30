using UnityEngine;
using UnityEngine.Playables;

namespace FMVCore.Interactable
{
    public class InteractablePlayableAsset : PlayableAsset
    {
        public InteractablePlayableBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<InteractablePlayableBehaviour>.Create(graph, template);

            var interactablePlayableBehaviour = playable.GetBehaviour();

            return playable;
        }
    }
}