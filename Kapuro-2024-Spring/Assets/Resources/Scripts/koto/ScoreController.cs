using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private int accumulatedScore; //スコアの合計値

    public int AccumulatedScore
    {
        get => accumulatedScore;
        set => accumulatedScore = value;
    }
    
    
    //スコアを加算する
    public void AddScore(int score)
    {
        accumulatedScore += score;
    }
}
