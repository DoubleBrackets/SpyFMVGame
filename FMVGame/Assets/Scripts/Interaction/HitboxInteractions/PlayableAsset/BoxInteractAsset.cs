using UnityEngine;
using UnityEngine.Playables;

namespace DoubleOhPew.Interactions.Timeline
{
    public class BoxInteractAsset : PlayableAsset
    {
        public BoxInteractBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<BoxInteractBehaviour>.Create(graph, template);
            return playable;
        }
    }
}