using UnityEngine;

namespace DoubleOhPew.Interaction.Core
{
    public abstract class InteractActionSO : ScriptableObject, IInteractAction
    {
        public abstract void TriggerAction(InteractionInfo info);
    }
}