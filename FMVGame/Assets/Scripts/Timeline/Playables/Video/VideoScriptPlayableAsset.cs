using System;
using UnityEngine;
using UnityEngine.Playables;

namespace FMVCore.Video
{
    [Serializable]
    public class VideoScriptPlayableAsset : PlayableAsset
    {
        [SerializeField]
        private VideoPlayableBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            var playable =
                ScriptPlayable<VideoPlayableBehaviour>.Create(graph, template);

            return playable;
        }
    }
}