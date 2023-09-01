using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public void SendInteraction(InteractionInfo interactionInfo)
    {
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