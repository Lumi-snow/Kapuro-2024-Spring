using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private StartSignalScript startSignalScript;

    void Start()
    {
        Application.targetFrameRate = 60;
        startSignalScript = FindObjectOfType<StartSignalScript>(); // StartSignalScriptのインスタンスを探す
    }

    void Update()
    {
        if (startSignalScript != null && startSignalScript.signal == true)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                transform.Translate(-0.1f, 0, 0);

            if (Input.GetKey(KeyCode.RightArrow))
                transform.Translate(0.1f, 0, 0);

            if (Input.GetKey(KeyCode.UpArrow))
                transform.Translate(0, 0.1f, 0);


            if (Input.GetKey(KeyCode.DownArrow))
                transform.Translate(0, -0.1f, 0);

        }
    }
}
