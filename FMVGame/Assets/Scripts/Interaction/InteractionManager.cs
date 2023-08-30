using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [ShowInInspector]
    private readonly Dictionary<string, InteractableInstance> currentInteractions = new();

    public event Action DrawGizmos;


    public InteractableInstance CreateInteractable(string interactionId, IEnumerable<InteractableTrigger> interactionTrigger)
    {
        if (currentInteractions.ContainsKey(interactionId))
        {
            return currentInteractions[interactionId];
        }

        var interactionInstance = new InteractableInstance { interactionTriggers = interactionTrigger.ToList() };
        currentInteractions.Add(interactionId, interactionInstance);
        interactionInstance.Initialize();

        return interactionInstance;
    }

    public void DisposeInteractable(string interactionId)
    {
        if (currentInteractions.ContainsKey(interactionId))
        {
            currentInteractions[interactionId].Dispose();
            currentInteractions.Remove(interactionId);
        }
    }

    public void EnableInteractable(string interactionId)
    {
        if (currentInteractions.TryGetValue(interactionId, out var interactableInstance))
        {
            interactableInstance.Enable();
        }
    }

    public void DisableInteractable(string interactionId)
    {
        if (currentInteractions.TryGetValue(interactionId, out var interactableInstance))
        {
            interactableInstance.Disable();
        }
    }

    public void SetTriggerPose(string interactionId, InteractableTrigger.TriggerPose triggerPose)
    {
        if (currentInteractions.TryGetValue(interactionId, out var interactableInstance))
        {
            interactableInstance.interactionTriggers[0].UpdateTrigger(triggerPose);
        }
    }

    public void SendInteraction(InteractionInfo interactionInfo)
    {
        foreach (var pair in currentInteractions)
        {
            var interactionInstance = pair.Value;
            interactionInstance.EvaluateInteraction(interactionInfo);
        }
    }

    public bool InteractableExists(string interactionId)
    {
        return currentInteractions.ContainsKey(interactionId);
    }

    public void OnDrawGizmos()
    {
        DrawGizmos?.Invoke();
    }
}

public class InteractableInstance
{
    public List<InteractableTrigger> interactionTriggers;

    public void Initialize()
    {
        foreach (var trigger in interactionTriggers)
        {
            trigger.Initialize();
        }
    }

    public void Enable()
    {
        foreach (var trigger in interactionTriggers)
        {
            trigger.Enable();
        }
    }

    public void Disable()
    {
        foreach (var trigger in interactionTriggers)
        {
            trigger.Disable();
        }
    }

    public void Dispose()
    {
        foreach (var trigger in interactionTriggers)
        {
            trigger.Dispose();
        }
    }

    public void EvaluateInteraction(InteractionInfo interactionInfo)
    {
        foreach (var trigger in interactionTriggers)
        {
            if (trigger.EvaluateInteraction(interactionInfo))
            {
                Debug.Log($"{"OMG IT TRIGGERED"}");
                return;
            }
        }
    }
}

public struct InteractionInfo
{
    public enum InteractionType
    {
        Click
    }

    public InteractionType interactionType;
    public Vector2 mouseWorldPos;
}