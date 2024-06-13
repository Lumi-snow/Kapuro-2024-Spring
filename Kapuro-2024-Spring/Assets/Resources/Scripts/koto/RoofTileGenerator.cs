using System.Collections.Generic;
using UnityEngine;

//瓦を生成するクラス
public class RoofTileGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> roofTileType; //瓦のPrefab
    
    [SerializeField] private RoofTileController roofTileController; //RoofTileController
    [SerializeField] private PrefabController prefabController; //PrefabController
    [SerializeField] private AddGameObjectController addGameObjectController; //AddGameObjectController

    //初期化
    public void Initialize()
    {
        //PrefabControllerに瓦のPrefabを登録
        foreach(GameObject _roofTile in roofTileType)
        {
            prefabController.AddNewPrefab(_roofTile);
        }
        
        //空のゲームオブジェクトをCanvasの子として生成
        addGameObjectController.SetPairGameObject("RoofTiles", "Canvas");
        addGameObjectController.AddGameObject();
        
        for(int i = 0; i < roofTileController.allRoofTileNum; i++)
        {
            GenerateRoofTile(); //瓦を生成
        }
    }

    //瓦を生成
    public void GenerateRoofTile()
    {
        int randomValue = UnityEngine.Random.Range(0, roofTileType.Count); //瓦の種類をランダムに決定

        switch (randomValue)
        {
            case 0: //CorrectRoofTileを生成
                prefabController.InstantiatePrefab("CorrectRoofTile", Vector3.zero, Quaternion.identity, addGameObjectController.NewGameObject); //PrefabからCorrectRoofTileを複製
                GameObject correctRoofTile = prefabController.clonePrefab;
                correctRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //CorrectRoofTileの評価をNOT_EVALUATEDに設定
                roofTileController.roofTiles.Add(correctRoofTile); //複製したCorrectRoofTileをリストに追加
                break;
            case 1: //BrokenRoofTileを生成
                prefabController.InstantiatePrefab("BrokenRoofTile", Vector3.zero, Quaternion.identity, addGameObjectController.NewGameObject); //PrefabからBrokenRoofTileを複製
                GameObject brokenRoofTile = prefabController.clonePrefab;
                brokenRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //BrokenRoofTileの評価をNOT_EVALUATEDに設定
                roofTileController.roofTiles.Add(brokenRoofTile); //複製したBrokenRoofTileをリストに追加
                break;
            default:
                Debug.Log("Error occured in Generate(), RoofTileGenerator");
                break;
        }
    }

    //DEBUG Listの中身を表示
    private void PrintList()
    {
        foreach(GameObject _roofTile in roofTileController.roofTiles)
        {
            if (_roofTile != null)
            {
                Debug.Log("瓦誕生！");
                Debug.Log(_roofTile.GetType().Name);
            }
            else
            {
                Debug.Log("Error occured in PrintList(), RoofGenerator");
            }   
        }
    }
}
