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
            if (videoPlayerHandler == null || videoClip == null)
            {
                return;
            }

            if (!initialSetup)
            {
                videoPlayer.source = VideoSource.VideoClip;
                videoPlayer.clip = videoClip;
                videoPlayer.playOnAwake = false;
                videoPlayer.waitForFirstFrame = true;
                videoPlayer.isLooping = loop;

                for (ushort i = 0; i < videoClip.audioTrackCount; ++i)
                {
                    if (videoPlayer.audioOutputMode == VideoAudioOutputMode.Direct)
                    {
                        videoPlayer.SetDirectAudioMute(i, mute);
                        videoPlayer.SetDirectAudioVolume(i, 1);
                    }
                    else if (videoPlayer.audioOutputMode == VideoAudioOutputMode.AudioSource)
                    {
                        var audioSource = videoPlayer.GetTargetAudioSource(i);
                        if (audioSource != null)
                        {
                            audioSource.mute = mute;
                        }
                    }
                }

                initialSetup = true;
            }

            if (!isPreparing && !videoPlayer.isPrepared)
            {
                videoPlayer.time = clipInTime;
                videoPlayer.Prepare();
                videoPlayer.prepareCompleted += HandlePrepareComplete;
                isPreparing = true;
            }
        }

        private void HandlePrepareComplete(VideoPlayer source)
        {
            source.prepareCompleted -= HandlePrepareComplete;

            // Playing and pausing prevents some weird delays
            source.Play();
            source.Pause();
            isPreparing = false;
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
            GetVideoHandlerFromPool();
        }


        public override void OnPlayableDestroy(Playable playable)
        {
            ReturnVideoHandlerToPool(playable);
        }


        private void GetVideoHandlerFromPool()
        {
            if (Application.isPlaying)
            {
                videoPlayerHandler = VideoPlayerPool.Instance.GetVideoPlayer();
            }
            else
            {
                videoPlayerHandler = EditorVideoPool.Instance.GetFromPool();
            }
        }

        private void ReturnVideoHandlerToPool(Playable playable)
        {
            if (Application.isPlaying)
            {
                VideoPlayerPool.Instance.ReturnVideoPlayer(videoPlayerHandler);
            }
            else
            {
                if (playable.GetTime() > 0 && playable.GetTime() <= playable.GetDuration())
                {
                    EditorVideoPool.Instance.ReturnToPoolSoft(videoPlayerHandler);
                }
                else
                {
                    EditorVideoPool.Instance.ReturnToPool(videoPlayerHandler);
                }
            }

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