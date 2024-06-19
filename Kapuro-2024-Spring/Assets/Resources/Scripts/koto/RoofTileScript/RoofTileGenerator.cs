using System.Collections.Generic;
using UnityEngine;

//瓦を生成するクラス
public class RoofTileGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> roofTileType; //瓦のPrefab
    
    [SerializeField] private RoofTileController roofTileController; //RoofTileController
    [SerializeField] private BossController bossController; //BossController
    [SerializeField] private PrefabController prefabController; //PrefabController
    [SerializeField] private AddGameObjectController addGameObjectController; //AddGameObjectController
    
    [SerializeField] private GameObject roofTile; //瓦のPrefab

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
        roofTile = addGameObjectController.NewGameObject;
        
        for(int i = 0; i < roofTileController.allRoofTileNum; i++)
        {
            GenerateRoofTile(); //瓦を生成
        }
        
        GenerateEventRoofTile(); //イベント瓦を生成
    }

    //瓦を生成
    public void GenerateRoofTile()
    {
        int randomValue = UnityEngine.Random.Range(0, roofTileType.Count - 9); //瓦の種類をランダムに決定
        int randomIndex = UnityEngine.Random.Range(3, (roofTileController.roofTiles.Count - 1) / 2); //どのタイミングで出現させるかをランダムに決定

        if (bossController.boss == null)
        {
            switch (randomValue)
            {
                case 0: //CorrectRoofTileを生成
                    prefabController.InstantiatePrefab("CorrectRoofTile", Vector3.zero, Quaternion.identity, roofTile); //PrefabからCorrectRoofTileを複製
                    GameObject correctRoofTile = prefabController.clonePrefab;
                    correctRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //CorrectRoofTileの評価をNOT_EVALUATEDに設定
                    roofTileController.roofTiles.Add(correctRoofTile); //複製したCorrectRoofTileをリストに追加
                    break;
                case 1: //ExpensiveRoofTileを生成
                    prefabController.InstantiatePrefab("ExpensiveRoofTile", Vector3.zero, Quaternion.identity, roofTile); //PrefabからExpensiveRoofTileを複製
                    GameObject expensiveRoofTile = prefabController.clonePrefab;
                    expensiveRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //ExpensiveRoofTileの評価をNOT_EVALUATEDに設定
                    roofTileController.roofTiles.Add(expensiveRoofTile); //複製したExpensiveRoofTileをリストに追加
                    break;
                case 2: //LegendRoofTileを生成
                    prefabController.InstantiatePrefab("LegendRoofTile", Vector3.zero, Quaternion.identity, roofTile); //PrefabからLegendRoofTileを複製
                    GameObject legendRoofTile = prefabController.clonePrefab;
                    legendRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //LegendRoofTileの評価をNOT_EVALUATEDに設定
                    roofTileController.roofTiles.Add(legendRoofTile); //複製したLegendRoofTileをリストに追加
                    break;
                case 3: //BrokenRoofTileを生成
                    prefabController.InstantiatePrefab("BrokenRoofTile", Vector3.zero, Quaternion.identity, roofTile); //PrefabからBrokenRoofTileを複製
                    GameObject brokenRoofTile = prefabController.clonePrefab;
                    brokenRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //BrokenRoofTileの評価をNOT_EVALUATEDに設定
                    roofTileController.roofTiles.Add(brokenRoofTile); //複製したBrokenRoofTileをリストに追加
                    break;
                default:
                    Debug.Log("Error occured in Generate(), RoofTileGenerator");
                    break;
            }
        }
        else
        {
            switch (randomValue)
            {
                case 0: //CorrectRoofTileを生成
                    prefabController.InstantiatePrefab("CorrectRoofTile", Vector3.zero, Quaternion.identity, roofTile); //PrefabからCorrectRoofTileを複製
                    GameObject correctRoofTile = prefabController.clonePrefab;
                    correctRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //CorrectRoofTileの評価をNOT_EVALUATEDに設定
                    roofTileController.roofTiles.Insert(randomIndex, correctRoofTile); //複製したCorrectRoofTileをリストに追加
                    break;
                case 1: //ExpensiveRoofTileを生成
                    prefabController.InstantiatePrefab("ExpensiveRoofTile", Vector3.zero, Quaternion.identity, roofTile); //PrefabからExpensiveRoofTileを複製
                    GameObject expensiveRoofTile = prefabController.clonePrefab;
                    expensiveRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //ExpensiveRoofTileの評価をNOT_EVALUATEDに設定
                    roofTileController.roofTiles.Insert(randomIndex, expensiveRoofTile); //複製したExpensiveRoofTileをリストに追加
                    break;
                case 2: //LegendRoofTileを生成
                    prefabController.InstantiatePrefab("LegendRoofTile", Vector3.zero, Quaternion.identity, roofTile); //PrefabからLegendRoofTileを複製
                    GameObject legendRoofTile = prefabController.clonePrefab;
                    legendRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //LegendRoofTileの評価をNOT_EVALUATEDに設定
                    roofTileController.roofTiles.Add(legendRoofTile); //複製したLegendRoofTileをリストに追加
                    break;
                case 3: //BrokenRoofTileを生成
                    prefabController.InstantiatePrefab("BrokenRoofTile", Vector3.zero, Quaternion.identity, roofTile); //PrefabからBrokenRoofTileを複製
                    GameObject brokenRoofTile = prefabController.clonePrefab;
                    brokenRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //BrokenRoofTileの評価をNOT_EVALUATEDに設定
                    roofTileController.roofTiles.Insert(randomIndex, brokenRoofTile); //複製したBrokenRoofTileをリストに追加
                    break;
                default:
                    Debug.Log("Error occured in Generate(), RoofTileGenerator");
                    break;
            }
        }
    }

    public void GenerateSpecialRoofTileForBoss()
    {
        switch(bossController.boss.GetComponent<AbstractBoss>().bossType)
        {
            case AbstractBoss.BossType.KAWARA_YOKAI: //瓦妖怪の場合
                if (bossController.boss.GetComponent<AbstractBoss>().IsAllDescendantDead == false)
                {
                    //眷属を生成
                    for(int i = 0 ; i < bossController.boss.GetComponent<AbstractBoss>().AllDescendantNum ; i++)
                    {
                        bossController.boss.GetComponent<AbstractBoss>().GenerateKawaraYokaiDescendant(roofTileController, roofTile);
                        GenerateRoofTile(); //追加の瓦を生成
                    }
                
                    bossController.boss.GetComponent<AbstractBoss>().IsAllDescendantDead = true; //AllDescendantNumを0に設定
                }
                
                break;
            case AbstractBoss.BossType.SHISHIGAWARA: //獅子瓦の場合
                if (bossController.boss.GetComponent<AbstractBoss>().IsGenerateShishiGawaraWaterRoofTile == false)
                {
                    //水の音がする瓦を生成
                    for(int i = 0 ; i < bossController.boss.GetComponent<AbstractBoss>().AllShishiGawaraWaterRoofTileNum ; i++)
                    {
                        bossController.boss.GetComponent<AbstractBoss>().GenerateShishiGawaraWaterRoofTile(roofTileController, roofTile);
                        GenerateRoofTile(); //追加の瓦を生成
                    }

                    //笛を生成
                    for (int i = 0; i < bossController.boss.GetComponent<AbstractBoss>().AllShishiGawaraWhistleNum; i++)
                    {
                        bossController.boss.GetComponent<AbstractBoss>().GenerateShishiGawaraWhistle(roofTileController, roofTile);
                        GenerateRoofTile(); //追加の瓦を生成
                    }
                    
                    //Todo KidsModeでは生成しない
                    //イベント用の瓦を生成
                    bossController.boss.GetComponent<AbstractBoss>().GenerateShishiGawaraEventRoofTile(roofTileController, roofTile);
                    
                    bossController.boss.GetComponent<AbstractBoss>().IsGenerateShishiGawaraWaterRoofTile = true; //IsGenerateShishiGawaraWaterRoofTileをtrueに設定
                }
                
                break;
            case AbstractBoss.BossType.KAWARA_BOUZU: //瓦坊主の場合
                if (bossController.boss.GetComponent<AbstractBoss>().IsGenerateKawaraBouzuRoofTile == false)
                {
                    for (int i = 0; i < bossController.boss.GetComponent<AbstractBoss>().AllAburaNum; i++)
                    {
                        bossController.boss.GetComponent<AbstractBoss>().GenerateKawaraBouzuAburaRoofTile(roofTileController, roofTile);
                        GenerateRoofTile();
                    }

                    for (int i = 0; i < bossController.boss.GetComponent<AbstractBoss>().AllMameNum; i++)
                    {
                        bossController.boss.GetComponent<AbstractBoss>().GenerateKawaraBouzuMameRoofTile(roofTileController, roofTile);
                        GenerateRoofTile();
                    }

                    for (int i = 0; i < bossController.boss.GetComponent<AbstractBoss>().AllKyouNum; i++)
                    {
                        bossController.boss.GetComponent<AbstractBoss>().GenerateKawaraBouzuKyouRoofTile(roofTileController, roofTile);
                        GenerateRoofTile();
                    }
                    
                    //Todo KidsModeでは生成しない
                    //イベント用の瓦を生成
                    bossController.boss.GetComponent<AbstractBoss>().GenerateKawaraBouzuEventRoofTile(roofTileController, roofTile);
                    
                    bossController.boss.GetComponent<AbstractBoss>().IsGenerateKawaraBouzuRoofTile = true; //IsGenerateKawaraBouzuRoofTileをtrueに設定
                }

                break;
            default:
                Debug.Log("Error occured in GenerateSpecialRoofTileForBoss(), RoofTileGenerator");
                break;
        }
    }

    private void GenerateEventRoofTile()
    {
        int generateIndex = roofTileController.allRoofTileNum - (roofTileController.allRoofTileNum / 3); //どのタイミングで出現させるかをランダムに決定
        
        prefabController.InstantiatePrefab("EventRoofTile", Vector3.zero, Quaternion.identity, roofTile); //PrefabからEventRoofTileを複製
        GameObject eventRoofTile = prefabController.clonePrefab;
        eventRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //EventRoofTileの評価をNOT_EVALUATEDに設定
        roofTileController.roofTiles.Insert(generateIndex, eventRoofTile); //複製したEventRoofTileをリストに追加
    }
}
