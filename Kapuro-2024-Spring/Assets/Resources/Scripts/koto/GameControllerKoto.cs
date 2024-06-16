using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerKoto : AbstractGameController
{
    [SerializeField] private RoofTileController roofTileController;
    [SerializeField] private BossController bossController;
    [SerializeField] private UIControllerKoto uiControllerKoto;
    private void Awake()
    {
        
    }

    private void Start()
    {
        roofTileController.Initialize(); //瓦関係の処理の初期化
        bossController.Initialize(); //ボス関係の処理の初期化
    }

    private void Update()
    {
        roofTileController.Evaluate(); //瓦の評価
        roofTileController.Destroy(); //瓦の破棄
        if (bossController.IsBossDead == true)
            roofTileController.DestroySpecialRoofTileForBoss();
        if(bossController.boss != null && roofTileController.roofTiles.Count == 2) 
            roofTileController.GenerateRoofTileInTime(); //ボス生存時の瓦追加生成
        
        bossController.SetNewBoss(); //ボスのセット
        if(bossController.boss != null)
            roofTileController.GenerateSpecialRoofTileForBoss(); //ボス用の瓦を生成
        bossController.DestroyBoss(); //ボス体力0で破棄
        
        uiControllerKoto.UpdateScoreText(); //スコアの更新
        GameEndProcess(); //ゲーム終了時の処理
    }
    
    public void GameEndProcess()
    {
        //TODO: ゲーム終了時の処理を追加
        if (roofTileController.roofTiles.Count == 0 && bossController.boss == null)
        {
            SceneController.ChangeSceneToTitle();
        }
    }
}