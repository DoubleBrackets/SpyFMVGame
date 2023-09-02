using System;
using DoubleOhPew.Interactions.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace DoubleOhPew.Interactions.Timeline
{
    [Serializable]
    public abstract class InteractBehaviour<TTrigger, TTriggerPose> : PlayableBehaviour
        where TTrigger : IInteractTrigger<TTriggerPose>, new() where TTriggerPose : struct, IHandlesDrawable
    {
        // Pose needs to be a struct to have its fields be keyframeable by Timeline
        public TTriggerPose pose;
        public InteractActionSO[] interactActions;

        [Header("Debug Color")]
        public Color handleColor = Color.red;

        private Interactable<TTrigger, TTriggerPose> interactable;
        private TTrigger interactTrigger;

        public InteractionManager interactionManager { get; set; }


        public override void PrepareFrame(Playable playable, FrameData info)
        {
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (Application.isPlaying)
            {
                interactable.UpdatePose(pose);
            }
        }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            SceneView.duringSceneGui += DrawInteractableTriggerVisuals;

            if (Application.isPlaying && interactionManager)
            {
                interactable.Enable();
                interactionManager.handleInteraction += HandleInteraction;
            }
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            SceneView.duringSceneGui -= DrawInteractableTriggerVisuals;

            if (Application.isPlaying && interactionManager)
            {
                interactable.Disable();
                interactionManager.handleInteraction -= HandleInteraction;
            }
        }

        public override void OnPlayableCreate(Playable playable)
        {
            if (Application.isPlaying)
            {
                interactionManager ??= InteractionManager.instance;
                interactable = new Interactable<TTrigger, TTriggerPose>();
                interactable.Initialize(interactActions);
            }
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            if (Application.isPlaying)
            {
                interactable.Dispose();
            }
        }

        public override void OnGraphStart(Playable playable)
        {
        }

        public override void OnGraphStop(Playable playable)
        {
        }


        private void HandleInteraction(InteractionInfo info)
        {
            interactable.HandleInteraction(info);
        }


        private void DrawInteractableTriggerVisuals(SceneView sceneView)
        {
            pose.DrawHandles(handleColor);
        }
    }
}