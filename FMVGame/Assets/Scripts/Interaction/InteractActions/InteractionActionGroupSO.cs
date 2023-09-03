using System.Collections.Generic;
using DoubleOhPew.Interactions.Core;
using UnityEngine;

/// <summary>
/// Group together a set of interaction actions for reusability
/// </summary>
[CreateAssetMenu(menuName = "InteractionActionGroupSO")]
public class InteractionActionGroupSO : InteractActionSO
{
    [SerializeReference]
    private List<IInteractAction> interactActions;

    public override void TriggerAction(InteractionInfo info)
    {
        foreach (var interactAction in interactActions)
        {
            interactAction.TriggerAction(info);
        }
    }
}