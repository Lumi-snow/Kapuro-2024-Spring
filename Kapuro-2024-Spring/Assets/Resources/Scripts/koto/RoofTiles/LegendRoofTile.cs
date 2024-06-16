using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.LEGEND; // roofTileType プロパティをオーバーライド
    public override EvaluateType evaluateType { get; set; } // evaluateType プロパティをオーバーライド

    /*共通の変数*/
    [SerializeField] int scoreCorrectRoofTile = 1000; //スコア
    
    /*共通のプロパティ*/
    public override int Score //スコアのプロパティ
    {
        get => scoreCorrectRoofTile;
        set => scoreCorrectRoofTile = value;
    }
    
    /*固有の変数*/
    
    /*固有のプロパティ*/
}
