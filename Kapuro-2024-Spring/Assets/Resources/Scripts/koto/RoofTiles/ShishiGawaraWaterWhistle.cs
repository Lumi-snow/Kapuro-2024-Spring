using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShishiGawaraWaterWhistle : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.SHISHIGAWARA_WHISTLE; // roofTileType プロパティをオーバーライド
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
    
    /*固有のメンバ変数*/
    [SerializeField] private int shishiGawaraWhistleAttackPower = 15; //笛の攻撃力
    
    /*固有のプロパティ*/
    public override int ShishiGawaraWhistleAttackPower { get => shishiGawaraWhistleAttackPower; set => shishiGawaraWhistleAttackPower = value; }
    
    /*固有のメンバ関数*/
}
