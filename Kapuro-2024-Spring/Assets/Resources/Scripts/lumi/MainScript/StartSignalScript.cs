using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSignalScript : MonoBehaviour
{
     public Text StartCount;
     public static float StartTime;
     public bool signal = false;
    public bool isCount = false;

    // Start is called before the first frame update

    private void Start()
    {
        StartTime = 3.0f;//�Q�[���J�n�O�̃J�E���g�_�E���p
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            isCount = true;
        if(isCount == true)
            StartSignal();
        EndSignal();
    }
    void StartSignal()
    {        
     StartTime -= Time.deltaTime;
     StartCount.text = String.Format("{0:00}", StartTime);
        if (StartTime <= 0.0 && StartTime >= -2.0)
            {
                StartCount.text = String.Format("�X�^�[�g�I");
                Signal();
         }
        if(StartTime <= -2.0)
            {
                StartCount.text = ("");
         }
        
    }

    void EndSignal()
    {
        if(TimerController.CountDownTime <= 0.0f)
        {
            StartCount.text = ("�����I");
            signal = false;
            Debug.Log("�I�t�ɂȂ�����");
            Invoke("MoveScenes", 3);
        }
    }

    void Signal()
    { signal = true; }

    void MoveScenes()
    {
        SceneController.ChangeScene("lumiEnd");
    }
}
