using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofTileGenerator : MonoBehaviour, IRoofTileGenerator
{
    [SerializeField] private List<RoofTile> roofTiles = new List<RoofTile>(); //������
    [SerializeField] private float waitingTime; //�{�^�������ҋ@����
    [SerializeField] private int allRoofTileType; //���̎��
    [SerializeField] private int allRoofTileNum; //���̑���

    private void Start()
    {
        Initialize();
        PrintList();
    }

    private void Update()
    {

    }

    //������
    public void Initialize()
    {
        //DEBUG ���̒l�̓f�o�b�O�p
        waitingTime = 2.0f;
        allRoofTileType = 2;
        allRoofTileNum = 5;

        for(int i = 0; i < allRoofTileNum; i++)
        {
            Generate();
        }
    }

    //���𐶐�
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

    //DEBUG List�̒��g��\��
    private void PrintList()
    {
        foreach(RoofTile roofTile in roofTiles)
        {
            if (roofTile != null)
            {
                Debug.Log("���a���I");
                Debug.Log(roofTile.GetType().Name);
            }
            else
            {
                Debug.Log("Error occured in PrintList(), RoofGenerator");
            }   
        }
    }
}
