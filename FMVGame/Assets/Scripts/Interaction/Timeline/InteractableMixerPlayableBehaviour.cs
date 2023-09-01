using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace DoubleOhPew.Interactions.Timeline
{
    public class InteractableMixerPlayableBehaviour : PlayableBehaviour
    {
        public IEnumerable<TimelineClip> Clips { get; set; }
        public PlayableDirector Director { get; set; }

        public override void PrepareFrame(Playable playable, FrameData info)
        {
            /*if (Clips == null)
            {
                return;
            }

            var inputPort = 0;
            foreach (var clip in Clips)
            {
                var scriptPlayable =
                    (ScriptPlayable<InteractablePlayableBehaviour>)playable.GetInput(inputPort);

                var interactablePlayableBehaviour = scriptPlayable.GetBehaviour();

                if (interactablePlayableBehaviour != null)
                {
                }

                ++inputPort;
            }*/
        }
    }
}