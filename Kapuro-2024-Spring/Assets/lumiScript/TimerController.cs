using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerController : MonoBehaviour
{
    public static float CountDownTime;
    public Text TextCountDown;
    void Start()
    {
     CountDownTime = 30.0F;   
    }

    // Update is called once per frame
    void Update()
    {
        TextCountDown.text = String.Format("Time:{0:00.00}",CountDownTime);
        CountDownTime -= Time.deltaTime;

        if (CountDownTime <= 0.0F)
        {
            CountDownTime = 0.0F;
        }
    }
}
