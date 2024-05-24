using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofTileGenerator : MonoBehaviour, IRoofTileGenerator
{
    [SerializeField] private List<RoofTile> roofTiles = new List<RoofTile>(); //瓦たち
    [SerializeField] private float waitingTime; //ボタン押下待機時間
    [SerializeField] private int allRoofTileType; //瓦の種類
    [SerializeField] private int allRoofTileNum; //瓦の総数

    private void Start()
    {
        Initialize();
        PrintList();
    }

    private void Update()
    {

    }

    //初期化
    public void Initialize()
    {
        //DEBUG この値はデバッグ用
        waitingTime = 2.0f;
        allRoofTileType = 2;
        allRoofTileNum = 5;

        for(int i = 0; i < allRoofTileNum; i++)
        {
            Generate();
        }
    }

    //瓦を生成
    public void Generate()
    {
        int randomValue = UnityEngine.Random.Range(0, allRoofTileType);

        switch (randomValue)
        {
            case 0:
                roofTiles.Add(new CorrectRoofTile());
                break;
            case 1:
                roofTiles.Add(new BrokenRoofTile());
                break;
            default:
                Debug.Log("Error occured in Generate(), RoofTileGenerator");
                break;
        }
    }

    //DEBUG Listの中身を表示
    private void PrintList()
    {
        foreach(RoofTile roofTile in roofTiles)
        {
            if (roofTile != null)
            {
                Debug.Log("瓦誕生！");
                Debug.Log(roofTile.GetType().Name);
            }
            else
            {
                Debug.Log("Error occured in PrintList(), RoofGenerator");
            }   
        }
    }
}
