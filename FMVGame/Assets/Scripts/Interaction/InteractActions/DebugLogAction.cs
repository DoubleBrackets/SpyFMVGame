using System;
using DoubleOhPew.Interactions.Core;
using UnityEngine;

[Serializable]
public class DebugLogAction : IInteractAction
{
    public string message;

    public void TriggerAction(InteractionInfo info)
    {
        Debug.Log(message);
    }
}