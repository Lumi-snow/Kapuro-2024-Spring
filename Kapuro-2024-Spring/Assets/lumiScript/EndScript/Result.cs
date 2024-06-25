using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Text ScoreText;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        score = Count.getCount();
        ScoreText.text = string.Format("‚à‚­‚´‚¢:{0}‚±",score);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
