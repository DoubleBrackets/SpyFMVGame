using UnityEngine;

namespace DoubleOhPew.Interactions.Core
{
    public abstract class InteractActionSO : ScriptableObject, IInteractAction
    {
        public abstract void TriggerAction(InteractionInfo info);
    }
}