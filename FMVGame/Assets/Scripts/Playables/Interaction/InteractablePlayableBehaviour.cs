using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace FMVCore.Interactable
{
    [Serializable]
    public class InteractablePlayableBehaviour : PlayableBehaviour
    {
        [Header("Interactable")]
        public string interactionId;

        [Header("Trigger")]
        public InteractableTrigger.TriggerPose pose;

        [Header("Debug")]
        public Color handleColor = Color.red;

        private InteractionManager interactionManager;

        public override void PrepareFrame(Playable playable, FrameData info)
        {
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            interactionManager ??= playerData as InteractionManager;
            if (interactionManager)
            {
                if (!interactionManager.InteractableExists(interactionId))
                {
                    interactionManager.CreateInteractable(interactionId, new[] { new InteractableTrigger() });
                }

                interactionManager.SetTriggerPose(interactionId, pose);
            }
        }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (interactionManager)
            {
                interactionManager.EnableInteractable(interactionId);
            }

            SceneView.duringSceneGui += DrawInteractableTriggerVisuals;
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (interactionManager)
            {
                interactionManager.DisableInteractable(interactionId);
            }

            SceneView.duringSceneGui -= DrawInteractableTriggerVisuals;
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            if (interactionManager && interactionManager.InteractableExists(interactionId))
            {
                interactionManager.DisposeInteractable(interactionId);
            }
        }

        private void DrawInteractableTriggerVisuals(SceneView sceneView)
        {
            pose.DrawPoseHandles(handleColor);
        }
    }
}