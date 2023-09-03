using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [ShowInInspector, ReadOnly]
    private int score;

    [SerializeField]
    private TMP_Text scoreText;


    private void Awake()
    {
        Instance = this;
        AdjustScore(0);
    }

    public void AdjustScore(int adjustment)
    {
        score += adjustment;
        scoreText.text = $"Score: {score}";
    }
}