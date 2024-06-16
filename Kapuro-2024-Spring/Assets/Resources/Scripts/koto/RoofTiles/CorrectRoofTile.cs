using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//普通の瓦
public class CorrectRoofTile : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.NORMAL; // roofTileType プロパティをオーバーライド
    public override EvaluateType evaluateType { get; set; } // evaluateType プロパティをオーバーライド

    /*共通のメンバ変数*/
    [SerializeField] private int scoreCorrectRoofTile = 100; //スコア
    
    /*共通のプロパティ*/
    public override int Score //スコアのプロパティ
    {
        get => scoreCorrectRoofTile;
        set => scoreCorrectRoofTile = value;
    }
}
