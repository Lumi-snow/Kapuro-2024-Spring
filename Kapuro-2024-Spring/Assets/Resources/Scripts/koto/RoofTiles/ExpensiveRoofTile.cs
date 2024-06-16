using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpensiveRoofTile : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.EXPENSIVE; // roofTileType プロパティをオーバーライド
    public override EvaluateType evaluateType { get; set; } // evaluateType プロパティをオーバーライド

    private int scoreCorrectRoofTile = 500; //スコア
    public override int Score //スコアのプロパティ
    {
        get => scoreCorrectRoofTile;
        set => scoreCorrectRoofTile = value;
    }
    
    private int correctRoofTileAtackPower = 0; //攻撃力
    public override int AttackPower //攻撃力のプロパティ
    {
        get => correctRoofTileAtackPower;
        set => correctRoofTileAtackPower = value;
    }
}
