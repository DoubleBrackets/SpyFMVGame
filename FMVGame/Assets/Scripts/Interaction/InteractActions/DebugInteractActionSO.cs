using UnityEngine;

namespace DoubleOhPew.Interactions.Core
{
    [CreateAssetMenu(menuName = "InteractAction/Debug", fileName = "DebugInteractAction")]
    public class DebugInteractActionSO : InteractActionSO
    {
        [SerializeField]
        private string debugMessage;

        public override void TriggerAction(InteractionInfo info)
        {
            Debug.Log($"DebugInteractAction: {debugMessage} with info {info.ToString()}");
        }
    }
}