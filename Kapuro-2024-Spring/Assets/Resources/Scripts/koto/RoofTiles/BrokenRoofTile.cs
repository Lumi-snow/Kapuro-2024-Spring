using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//壊れた瓦
public class BrokenRoofTile : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.BROKEN; // roofTileType プロパティをオーバーライド
    public override EvaluateType evaluateType { get; set; } // evaluateType プロパティをオーバーライド
    
    /*共通のメンバ変数*/
    [SerializeField] private int scoreBrokenRoofTile = 50; //スコア
    
    /*共通のプロパティ*/
    public override int Score //スコアのプロパティ
    {
        get => scoreBrokenRoofTile;
        set => scoreBrokenRoofTile = value;
    }
    
    /*固有のメンバ変数*/
    
    /*固有のプロパティ*/
}
