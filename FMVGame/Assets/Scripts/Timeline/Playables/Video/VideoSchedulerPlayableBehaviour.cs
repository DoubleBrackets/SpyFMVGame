using System;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace FMVCore.Video
{
    public sealed class VideoSchedulerPlayableBehaviour : PlayableBehaviour
    {
        internal PlayableDirector director { get; set; }

        internal IEnumerable<TimelineClip> clips { get; set; }

        public override void PrepareFrame(Playable playable, FrameData info)
        {
            if (clips == null)
            {
                return;
            }

            var inputPort = 0;
            foreach (var clip in clips)
            {
                var scriptPlayable =
                    (ScriptPlayable<VideoPlayableBehaviour>)playable.GetInput(inputPort);

                var videoPlayableBehaviour = scriptPlayable.GetBehaviour();

                if (videoPlayableBehaviour != null)
                {
                    var preloadTime = Math.Max(0.0, videoPlayableBehaviour.preloadTime);

                    // Stop the video while its out of duration
                    if (director.time >= clip.start + clip.duration ||
                        director.time <= clip.start - preloadTime)
                    {
                        videoPlayableBehaviour.StopVideo();
                    }
                    else if (director.time > clip.start - preloadTime)
                    {
                        videoPlayableBehaviour.PrepareVideo();
                    }
                }

                ++inputPort;
            }
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame(playable, info, playerData);
        }
    }
}