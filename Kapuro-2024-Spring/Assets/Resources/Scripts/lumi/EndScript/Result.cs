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

        ScoreTextA.text = string.Format("��:{0}��", scoreA);
        ScoreTextB.text = string.Format("��:{0}��", scoreB);
        ScoreTextC.text = string.Format("�����:{0}�܂�", scoreC);
        ScoreTextD.text = string.Format("�Ȃ���:{0}�ɂ�", scoreD);

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
