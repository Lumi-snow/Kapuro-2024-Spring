using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Text ScoreTextA;
    public Text ScoreTextB;
    public Text ScoreTextC;
    public Text ScoreTextD;
    int scoreA;
    int scoreB;
    int scoreC;
    int scoreD;

    // Start is called before the first frame update
    void Start()
    {
        scoreA = Count.getCountA();
        scoreB = Count.getCountB();          
        scoreC = Count.getCountC();
        scoreD = Count.getCountD();

        ScoreTextA.text = string.Format("き:{0}こ", scoreA);
        ScoreTextB.text = string.Format("つち:{0}こ", scoreB);
        ScoreTextC.text = string.Format("かわら:{0}まい", scoreC);
        ScoreTextD.text = string.Format("なかま:{0}にん", scoreD);

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
