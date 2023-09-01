using System.Collections.Generic;

namespace DoubleOhPew.Interaction.Core
{
    public class Interactable<TTrigger, TTriggerPose>
        where TTrigger : IInteractTrigger<TTriggerPose>, new() where TTriggerPose : IHandlesDrawable
    {
        public TTrigger interactTrigger;

        public List<IInteractAction> interactActions;

        public void UpdatePose(TTriggerPose pose)
        {
            interactTrigger.UpdatePose(pose);
        }

        public bool EvaluateInteraction(InteractionInfo interactionInfo)
        {
            return interactTrigger.EvaluateInteraction(interactionInfo);
        }

        public void Initialize()
        {
            interactTrigger = new TTrigger();
            interactTrigger.Initialize();
        }

        public void Dispose()
        {
            interactTrigger.Dispose();
        }

        public void Enable()
        {
            interactTrigger.Enable();
        }

        public void Disable()
        {
            interactTrigger.Disable();
        }
    }
}