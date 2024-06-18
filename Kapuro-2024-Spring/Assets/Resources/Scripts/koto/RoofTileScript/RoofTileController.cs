using System.Collections.Generic;
using UnityEngine;

//RoofTileに関連する処理を行うクラス
public class RoofTileController : MonoBehaviour
{
    public List<GameObject> roofTiles; //生成した瓦のリスト
    public int allRoofTileNum; //生成する瓦の総数
    
    [SerializeField] private RoofTileGenerator roofTileGenerator;
    [SerializeField] private RoofTileDisplayer roofTileDisplayer;
    [SerializeField] private RoofTileEvaluater roofTileEvaluater;
    [SerializeField] private RoofTileDestroyer roofTileDestroyer;
    [SerializeField] private RoofTileEventHandler roofTileEventHandler;
    [SerializeField] private ScoreController scoreController;
    
    //初期化処理
    public void Initialize()
    {
        roofTileGenerator.Initialize();
        roofTileDisplayer.HideRoofTile();
        roofTileDisplayer.SetRoofTile();
    }

    //瓦を生成する
    public void Evaluate()
    {
        roofTileEvaluater.EvaluateRoofTile(GetCurrentRoofTile());
    }

    //瓦を破棄する
    public async void Destroy()
    {
        bool isSetNext = await roofTileDestroyer.DestroyEvaluatedRoofTile(GetCurrentRoofTile());

        if (isSetNext == true)
        {
            SetNewRoofTile();
        }
    }
    
    public void DestroySpecialRoofTileForBoss()
    {
        roofTileDestroyer.DestroySpecialRoofTileForCurrentIndex1and2(GetCurrentRoofTile(), GetSpecialRoofTileIndex1());
        roofTileDestroyer.DestroySpecialRoofTileForBoss(GetSpecialRoofTileIndex2());
        SetNewRoofTile();
    }

    public void GenerateRoofTileInTime()
    {
        for (int i = 0; i < 2; i++)
        {
            roofTileGenerator.GenerateRoofTile();
        }
    }
    
    public void GenerateSpecialRoofTileForBoss()
    {
        roofTileGenerator.GenerateSpecialRoofTileForBoss();
    }

    //新しく瓦をセットする
    private void SetNewRoofTile()
    {
        roofTileDisplayer.SetRoofTile();
        roofTileDisplayer.ActivateRoofTile();
        roofTileDisplayer.HideRoofTile();
    }
    
    //現在の瓦を取得する
    private GameObject GetCurrentRoofTile()
    {
        if (roofTiles.Count != 0)
        {
            return roofTiles[0];
        }
        else //最後の瓦の場合
        {
            return null;
        }
    }

    private GameObject GetSpecialRoofTileIndex2()
    {
        if(roofTiles.Count > 2)
        {
            return roofTiles[2];
        }
        else
        {
            return null;
        }
    }

    private GameObject GetSpecialRoofTileIndex1()
    {
        if(roofTiles.Count > 1)
        {
            return roofTiles[1];
        }
        else
        {
            return null;
        }
    }
    
    
}
