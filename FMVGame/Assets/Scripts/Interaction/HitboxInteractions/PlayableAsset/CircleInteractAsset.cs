using UnityEngine;
using UnityEngine.Playables;

namespace DoubleOhPew.Interactions.Timeline
{
    public class CircleInteractAsset : PlayableAsset
    {
        public CircleInteractBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<CircleInteractBehaviour>.Create(graph, template);
            return playable;
        }
    }
}