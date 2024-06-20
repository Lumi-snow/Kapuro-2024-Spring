using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ShishiGawaraWaterRoofTile : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.SHISHIGAWARA_WATER; // roofTileType プロパティをオーバーライド
    public override EvaluateType evaluateType { get; set; } // evaluateType プロパティをオーバーライド

    /*共通のメンバ変数*/
    [SerializeField] private int scoreCorrectRoofTile = 1000; //スコア
    
    /*共通のプロパティ*/
    public override int Score //スコアのプロパティ
    {
        get => scoreCorrectRoofTile;
        set => scoreCorrectRoofTile = value;
    }

    /*共通のメンバ関数*/
    public override async UniTask OnDestroyProcess()
    {
        await UniTask.Yield();
    }
    
    /*固有のメンバ変数*/
    [SerializeField] private int awakingPoint = 30; //覚醒ポイント
    [SerializeField] private int awakingPointPower = 30; //覚醒ポイントのパワー
    
    /*固有のプロパティ*/
    public override int AwakingPoint { get => awakingPoint; set => awakingPoint = value; }
    public override int AwakingPointPower { get => awakingPointPower; set => awakingPointPower = value; }
    
    /*固有のメンバ関数*/
}
