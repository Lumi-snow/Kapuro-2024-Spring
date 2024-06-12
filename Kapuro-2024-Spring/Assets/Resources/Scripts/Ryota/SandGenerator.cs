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
        int rnd = Random.Range(0, 3);
        int size = 20 + rnd * 15;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject sand = Instantiate(sandPrefab);
            sand.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y - 20, 500);
            sand.transform.localScale = new Vector3(size, size, size);
            sand.GetComponent<SandStatus>().sizeLevel = rnd;
        }
    }
}
