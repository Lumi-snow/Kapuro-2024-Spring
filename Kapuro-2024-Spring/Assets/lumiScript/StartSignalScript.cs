using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSignalScript : MonoBehaviour
{
     public bool signal = false;

    // Start is called before the first frame update
    private void Update()
    {
        StartSignal();
        EndSignal();
    }
    void StartSignal()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Invoke("Signal",3);
        }
    }

    void EndSignal()
    {
        if(TimerController.CountDownTime <= 0.0f)
        {
            signal = false;
            Debug.Log("ƒIƒt‚É‚È‚Á‚½‚æ");
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
