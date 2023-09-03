using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Video;

namespace FMVCore.Video
{
    [Serializable]
    public class VideoScriptPlayableAsset : PlayableAsset
    {
        [SerializeField, NotKeyable]
        public VideoClip videoClip;

        [SerializeField, NotKeyable]
        public bool mute;

        [SerializeField, NotKeyable]
        public bool loop = true;

        [SerializeField, NotKeyable]
        public double preloadTime = 0.3;

        [SerializeField, NotKeyable]
        public double clipInTime;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            var playable =
                ScriptPlayable<VideoPlayableBehaviour>.Create(graph);

            var playableBehaviour = playable.GetBehaviour();

            playableBehaviour.videoClip = videoClip;
            playableBehaviour.mute = mute;
            playableBehaviour.loop = loop;
            playableBehaviour.preloadTime = preloadTime;
            playableBehaviour.clipInTime = clipInTime;

            return playable;
        }
    }
}