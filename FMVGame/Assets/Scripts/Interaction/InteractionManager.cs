using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class InteractionManager : MonoBehaviour
{
    private struct InteractionHandler : IComparable<InteractionHandler>
    {
        public InteractionHandlerDelegate handler;
        public bool passThrough;
        public int priority;
        public int CompareTo(InteractionHandler other) => other.priority.CompareTo(priority);
    }

    public delegate bool InteractionHandlerDelegate(InteractionInfo info);

    public static InteractionManager instance;

    private readonly List<InteractionHandler> registeredHandlers = new();

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public void SendInteraction(InteractionInfo interactionInfo)
    {
        foreach (var handler in registeredHandlers)
        {
            var passed = handler.handler.Invoke(interactionInfo);
            if (passed && !handler.passThrough)
            {
                return;
            }
        }
    }

    public void SubscribeInteractHandler(InteractionHandlerDelegate handlerAction, bool passThrough, int priority)
    {
        var newHandler = new InteractionHandler
        {
            handler = handlerAction,
            passThrough = passThrough,
            priority = priority
        };


        var index = registeredHandlers.BinarySearch(newHandler);
        if (index < 0)
        {
            index = ~index;
        }

        registeredHandlers.Insert(index, newHandler);
    }

    public void UnsubscribeInteractHandler(InteractionHandlerDelegate handlerAction)
    {
        for (var i = 0; i < registeredHandlers.Count; i++)
        {
            if (registeredHandlers[i].handler == handlerAction)
            {
                registeredHandlers.RemoveAt(i);
                return;
            }
        }
    }
}

public struct InteractionInfo
{
    public enum InteractionType
    {
        Click,
        TimelineMarker
    }

    public InteractionType interactionType;
    public Vector2 mouseWorldPos;
    public Playable sourcePlayable;

    public override string ToString() => $"type: {interactionType}, mouseWorldPos: {mouseWorldPos}";
}