using System;
using DoubleOhPew.Interactions.Core;

[Serializable]
public class AddScoreAction : IInteractAction
{
    public int score;


    public void TriggerAction(InteractionInfo info)
    {
        if (ScoreManager.Instance)
        {
            ScoreManager.Instance.AdjustScore(score);
        }
    }
}