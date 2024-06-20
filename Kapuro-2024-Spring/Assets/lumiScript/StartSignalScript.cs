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
    }
    void StartSignal()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Invoke("Signal",3);
        }
    }

    void Signal()
    { signal = true; }
}
