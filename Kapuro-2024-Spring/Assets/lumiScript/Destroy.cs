using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void Start()
    {
        startSignalScript = FindObjectOfType<StartSignalScript>(); // StartSignalScript�̃C���X�^���X��T��
    }

    private StartSignalScript startSignalScript;
    void OnCollisionEnter2D(Collision2D obj)
    {
        if (startSignalScript != null && startSignalScript.signal == true)
        {
            //Debug.Log("�ՓˁI�I");
            if (obj.gameObject.tag == "Player")
            {
                Debug.Log("�ՓˁI�I");
                Destroy(this.gameObject);
            }
        }
    }
}
