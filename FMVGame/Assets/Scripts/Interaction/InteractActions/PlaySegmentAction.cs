using System;
using DoubleOhPew.Interactions.Core;
using UnityEngine;

[Serializable]
public class PlaySegmentAction : IInteractAction
{
    public FMVSegmentSetupSO segment;

    public void TriggerAction(InteractionInfo info)
    {
        if (Application.isPlaying)
        {
            FMVGameDirector.Instance.PlaySegment(segment);
        }
    }
}