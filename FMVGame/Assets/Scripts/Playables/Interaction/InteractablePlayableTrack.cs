using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace FMVCore.Interactable
{
    [Serializable]
    [TrackClipType(typeof(InteractablePlayableAsset))]
    [TrackBindingType(typeof(InteractionManager))]
    [TrackColor(1, 0.776f, 0.255f)]
    public class InteractablePlayableTrack : PlayableTrack
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var playableDirector = go.GetComponent<PlayableDirector>();

            var playable = ScriptPlayable<InteractableMixerPlayableBehaviour>.Create(graph, inputCount);

            var interactableMixerPlayableBehaviour = playable.GetBehaviour();

            if (interactableMixerPlayableBehaviour != null)
            {
                interactableMixerPlayableBehaviour.Director = playableDirector;
                interactableMixerPlayableBehaviour.Clips = GetClips();
            }

            return playable;
        }
    }
}