using System;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public event Action<InteractionInfo> handleInteraction;

    public void SendInteraction(InteractionInfo interactionInfo)
    {
        handleInteraction?.Invoke(interactionInfo);
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

    public override string ToString() => $"type: {interactionType}, mouseWorldPos: {mouseWorldPos}";
}