using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D obj)
    {
        //Debug.Log("�ՓˁI�I");
        if (obj.gameObject.tag == "Player")
        {
            Debug.Log("�ՓˁI�I");
            Destroy(this.gameObject);
        }
    }
}
