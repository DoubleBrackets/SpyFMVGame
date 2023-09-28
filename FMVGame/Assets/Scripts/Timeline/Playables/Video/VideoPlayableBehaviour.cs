using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

namespace FMVCore.Video
{
    [Serializable]
    public class VideoPlayableBehaviour : PlayableBehaviour
    {
        public VideoClip videoClip;
        public bool mute;

        public bool loop = true;

        [InfoBox("Offset time where the video will hold. Only used if loop is false")]
        public float holdTimeOffset;

        [InfoBox("How far ahead to preload")]
        public double preloadTime = 2.5;

        [InfoBox("Video clip start time")]
        public double clipInTime = 0.1;

        private bool initialSetup;
        private bool isPreparing;

        private VideoPlayerHandler videoPlayerHandler;
        private VideoPlayer videoPlayer => videoPlayerHandler.VideoPlayer;

        public void PrepareVideo()
        {
            if (!initialSetup)
            {
                Debug.Log($"{"Grabbing " + videoClip.originalPath}");
                GetVideoHandlerFromPool(videoClip.originalPath);
                videoPlayerHandler.SetupPlayerWithClip(videoClip, loop, mute);
                initialSetup = true;
            }

            if (videoPlayerHandler == null || videoClip == null)
            {
                return;
            }

            if (!isPreparing && !videoPlayer.isPrepared)
            {
                videoPlayerHandler.PrepareVideoAtTime(clipInTime, a => { isPreparing = false; });
                isPreparing = true;
            }
        }

        public override void PrepareFrame(Playable playable, FrameData info)
        {
            if (videoPlayerHandler == null || videoClip == null)
            {
                return;
            }

            // Force sync every frame
            SyncVideoToPlayable(playable);

            // Pause while scrubbing
            if (info.deltaTime == 0)
            {
                // Play to make sure the time updates instantly
                PlayVideo();
                PauseVideo();
            }
            else
            {
                PlayVideo();
            }
        }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (videoPlayerHandler == null)
            {
                return;
            }

            PlayVideo();
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (videoPlayerHandler == null)
            {
                return;
            }

            // Stricter return to pool during play mode
            if (Application.isPlaying)
            {
                ReturnVideoHandlerToPool(playable, videoClip.originalPath);
            }

            PauseVideo();
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
        }

        public override void OnGraphStart(Playable playable)
        {
        }

        public override void OnGraphStop(Playable playable)
        {
            PauseVideo();
        }

        public override void OnPlayableCreate(Playable playable)
        {
        }


        public override void OnPlayableDestroy(Playable playable)
        {
            if (videoPlayerHandler == null)
            {
                return;
            }

            ReturnVideoHandlerToPool(playable, videoClip.originalPath);
        }


        private void GetVideoHandlerFromPool(string key)
        {
            if (Application.isPlaying)
            {
                videoPlayerHandler = VideoPlayerPool.Instance.GetVideoPlayer(key);
            }
            else
            {
                videoPlayerHandler = EditorVideoPool.Instance.GetFromPool(key);
            }
        }

        private void ReturnVideoHandlerToPool(Playable playable, string key)
        {
            Debug.Log($"{"Returning " + videoClip.originalPath}");
            if (Application.isPlaying)
            {
                VideoPlayerPool.Instance.ReturnVideoPlayer(key);
            }
            else
            {
                if (playable.GetTime() > 0 && playable.GetTime() <= playable.GetDuration())
                {
                    EditorVideoPool.Instance.ReturnToPoolSoft(key);
                }
                else
                {
                    EditorVideoPool.Instance.ReturnToPool(key);
                }
            }

            initialSetup = false;
            isPreparing = false;
            videoPlayerHandler = null;
        }

        public void PlayVideo()
        {
            if (videoPlayerHandler == null)
            {
                return;
            }

            videoPlayerHandler.Play();
        }

        public void PauseVideo()
        {
            if (videoPlayerHandler == null)
            {
                return;
            }

            videoPlayer.Pause();
        }

        public void StopVideo()
        {
            if (videoPlayerHandler == null)
            {
                return;
            }

            videoPlayerHandler.Stop();
        }

        private void SyncVideoToPlayable(Playable playable)
        {
            var rawTime = clipInTime + playable.GetTime() * videoPlayer.playbackSpeed;
            if (loop)
            {
                videoPlayer.time = rawTime % videoPlayer.clip.length;
            }
            else
            {
                videoPlayer.time = Math.Min(rawTime, videoPlayer.clip.length + holdTimeOffset);
            }
        }
    }
}