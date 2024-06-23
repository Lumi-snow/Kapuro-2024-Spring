using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshProを扱う際に必要
 
public class TimeScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    private float time;
    public bool isTimeUp;
    public static TimeScript instance;
 
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
 
    void Start()
    {
        time = 3.0f;
    }
 
    void Update()
    {
        if (0 <= time) 
        {
            time -= Time.deltaTime;
            timeText.text = "Time: " + time.ToString("F1");
        }
        else if(0 >= time)
        {
            isTimeUp = true;
            timeText.text = "TimeUp!";
        }
    }
}

/*
リファレンスhttps://futabazemi.net/unity/tmp-timeup
*/