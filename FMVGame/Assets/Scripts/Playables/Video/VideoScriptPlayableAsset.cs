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
        public ExposedReference<VideoPlayerSurface> videoPlayerSurface;

        [SerializeField, NotKeyable]
		public VideoClip videoClip;

        [SerializeField, NotKeyable]
        public bool mute = false;

        [SerializeField, NotKeyable]
        public bool loop = true;

        [SerializeField, NotKeyable]
        public double preloadTime = 0.3;

        [SerializeField, NotKeyable]
        public double clipInTime = 0.0;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
            ScriptPlayable<VideoPlayableBehaviour> playable =
                ScriptPlayable<VideoPlayableBehaviour>.Create(graph);

            VideoPlayableBehaviour playableBehaviour = playable.GetBehaviour();

            playableBehaviour.videoPlayerSurface = videoPlayerSurface.Resolve(graph.GetResolver());
            playableBehaviour.videoClip = videoClip;
            playableBehaviour.mute = mute;
            playableBehaviour.loop = loop;
            playableBehaviour.preloadTime = preloadTime;
            playableBehaviour.clipInTime = clipInTime;

            return playable;
		}
	}
}
