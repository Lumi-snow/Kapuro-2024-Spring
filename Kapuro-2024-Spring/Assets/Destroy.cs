using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D obj)
    {
        //Debug.Log("衝突！！");
        if (obj.gameObject.tag == "Player")
        {
            Debug.Log("衝突！！");
            Destroy(this.gameObject);
        }
    }
}
