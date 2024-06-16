using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandGenerator : MonoBehaviour
{
    public GameObject sandPrefab;
    public GameObject Player;
    static bool canCreateSand = false;

    void Start()
    {
        this.Player = GameObject.Find("Player");
    }

    void Update()
    {
        int rnd = Random.Range(1, 4);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject sand = Instantiate(sandPrefab);
            sand.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y - 20, 500);
            sand.GetComponent<SandStatus>().sizeLevel = rnd;
        }
    }

    public void createLevelUpSand(GameObject sandA, GameObject sandB)
    {
        if (!canCreateSand) canCreateSand = true;
        else
        {
            Debug.Log("LevelUP!!!");
            float disX = (sandB.transform.position.x - sandA.transform.position.x) / 2;
            float disY = (sandB.transform.position.y - sandA.transform.position.y) / 2;

            GameObject sand = Instantiate(sandPrefab);
            sand.transform.position = new Vector3(sandA.transform.position.x + disX, sandA.transform.position.y + disY, 500);
            sand.GetComponent<SandStatus>().sizeLevel = sandA.GetComponent<SandStatus>().sizeLevel + 1;

            canCreateSand = false;
        }
    }
}
