using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpensiveRoofTile : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.EXPENSIVE; // roofTileType プロパティをオーバーライド
    public override EvaluateType evaluateType { get; set; } // evaluateType プロパティをオーバーライド

    /*共通のメンバ変数*/
    [SerializeField] private int scoreCorrectRoofTile = 500; //スコア
    
    /*共通のプロパティ*/
    public override int Score //スコアのプロパティ
    {
        get => scoreCorrectRoofTile;
        set => scoreCorrectRoofTile = value;
    }
    
    /*固有のメンバ変数*/
    
    /*固有のプロパティ*/
}
