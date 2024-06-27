using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerController : MonoBehaviour
{
    public static float CountDownTime;
    public Text TextCountDown;
    private float step_time;
    private StartSignalScript startSignalScript;
    void Start()
    {
        CountDownTime = 15.0F;　//タイマーの時間指定はここで
        step_time = 0.0f; //endスクリーン移行のための時間
        startSignalScript = FindObjectOfType<StartSignalScript>(); // StartSignalScriptのインスタンスを探す
    }

    // Update is called once per frame
    void Update()
    {
        if (startSignalScript != null && startSignalScript.signal == true)
        {
            CountDownTime -= Time.deltaTime;
            TextCountDown.text = String.Format("のこりじかん:{0:00.00}", CountDownTime);
           
            if (CountDownTime <= 0.0F)
            {
                TextCountDown.text = ("のこりじかん:0:00");
                step_time += Time.deltaTime;
            }
        }
    }
}