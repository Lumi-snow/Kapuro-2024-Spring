using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandGenerator : MonoBehaviour
{
    public GameObject sandPrefab;
    public GameObject Player;

    void start()
    {
        this.Player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("push space");
            GameObject sand = Instantiate(sandPrefab);
            sand.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y - 20, 500);
        }
    }
}
