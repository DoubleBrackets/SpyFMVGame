using System;
using DoubleOhPew.Interactions.Core;
using UnityEngine;

[Serializable]
public class TriggerScriptableObjectAction : IInteractAction
{
    [SerializeField]
    private InteractActionSO actionSO;

    public void TriggerAction(InteractionInfo info)
    {
        actionSO.TriggerAction(info);
    }
}