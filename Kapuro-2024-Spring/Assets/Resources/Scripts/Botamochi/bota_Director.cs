using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bota_Director : MonoBehaviour
{
    GameObject unpaintedAreaGenerator;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
       if(!TimeScript.instance.isTimeUp)
       {
        Destroy(player);
       }
    }
}
