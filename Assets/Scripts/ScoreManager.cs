using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private UIManager UI;

    private int score = 0;

    public void AddToScore()
    {
        score += 10;
        UI.UpdateScore(score.ToString());
    }

    public int Score { get { return score; } }
}
