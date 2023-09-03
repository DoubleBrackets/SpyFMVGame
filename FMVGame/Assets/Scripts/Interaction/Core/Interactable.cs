using System.Collections.Generic;
using System.Linq;

namespace DoubleOhPew.Interactions.Core
{
    public class Interactable<TTrigger, TTriggerPose>
        where TTrigger : IInteractTrigger<TTriggerPose>, new() where TTriggerPose : IHandlesDrawable
    {
        public TTrigger interactTrigger;

        public IInteractAction[] interactActions;

        public void UpdatePose(TTriggerPose pose)
        {
            interactTrigger.UpdatePose(pose);
        }

        public bool HandleInteraction(InteractionInfo interactionInfo)
        {
            if (interactTrigger.EvaluateInteraction(interactionInfo))
            {
                foreach (var interactAction in interactActions)
                {
                    interactAction.TriggerAction(interactionInfo);
                }

                return true;
            }

            return false;
        }

        public void Initialize(IEnumerable<IInteractAction> actions)
        {
            interactTrigger = new TTrigger();
            interactTrigger.Initialize();
            interactActions = actions.ToArray();
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