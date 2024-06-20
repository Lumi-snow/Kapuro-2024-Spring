using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void Start()
    {
        startSignalScript = FindObjectOfType<StartSignalScript>(); // StartSignalScriptのインスタンスを探す
    }

    private StartSignalScript startSignalScript;
    void OnCollisionEnter2D(Collision2D obj)
    {
        if (startSignalScript != null && startSignalScript.signal == true)
        {
            //Debug.Log("衝突！！");
            if (obj.gameObject.tag == "Player")
            {
                Debug.Log("衝突！！");
                Destroy(this.gameObject);
            }
        }
    }
}
