using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerKoto : AbstractGameController
{
    [SerializeField] private RoofTileController roofTileController;
    [SerializeField] private UIControllerKoto uiControllerKoto;
    private void Awake()
    {
        
    }

    private void Start()
    {
        roofTileController.Initialize(); //瓦関係の処理の初期化
    }

    private void Update()
    {
        roofTileController.Evaluate(); //瓦の評価
        roofTileController.Destroy(); //瓦の破棄
        roofTileController.GameEndProcess(); //ゲーム終了時の処理
        uiControllerKoto.UpdateScoreText(); //スコアの更新
    }
}