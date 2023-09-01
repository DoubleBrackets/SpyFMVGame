using System;
using DoubleOhPew.Interaction.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace DoubleOhPew.Interactions.Timeline
{
    [Serializable]
    public abstract class InteractablePlayableBehaviour<TTrigger, TTriggerPose> : PlayableBehaviour
        where TTrigger : IInteractTrigger<TTriggerPose>, new() where TTriggerPose : struct, IHandlesDrawable
    {
        [Header("Interactable")]
        public string interactionId;

        public TTriggerPose pose;
        public InteractActionSO[] interactActions;

        [Header("Debug Color")]
        public Color handleColor = Color.red;

        private Interactable<TTrigger, TTriggerPose> interactable;
        private TTrigger interactTrigger;

        public override void PrepareFrame(Playable playable, FrameData info)
        {
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            interactable.UpdatePose(pose);
        }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            SceneView.duringSceneGui += DrawInteractableTriggerVisuals;
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            SceneView.duringSceneGui -= DrawInteractableTriggerVisuals;
        }

        public override void OnPlayableCreate(Playable playable)
        {
            interactable = new Interactable<TTrigger, TTriggerPose>();
            interactable.Initialize();
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            interactable.Dispose();
        }

        public override void OnGraphStart(Playable playable)
        {
        }

        public override void OnGraphStop(Playable playable)
        {
        }

        private void DrawInteractableTriggerVisuals(SceneView sceneView)
        {
            pose.DrawHandles(handleColor);
        }
    }
}