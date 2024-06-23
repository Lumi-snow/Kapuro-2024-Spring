using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIControllerKoto : MonoBehaviour
{
    [SerializeField] private ScoreController scoreController;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bossExpressionText;

    public void Initialize()
    {
        
    }
    
    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + scoreController.AccumulatedScore;
    }
}
