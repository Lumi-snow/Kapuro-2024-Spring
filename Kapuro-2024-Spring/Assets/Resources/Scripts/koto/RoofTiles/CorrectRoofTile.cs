using System.Collections;
using System.Collections.Generic;

//普通の瓦
public class CorrectRoofTile : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.NORMAL; // roofTileType プロパティをオーバーライド
    public override EvaluateType evaluateType { get; set; } // evaluateType プロパティをオーバーライド

    private int scoreCorrectRoofTile = 100; //スコア
    public override int Score //スコアのプロパティ
    {
        get => scoreCorrectRoofTile;
        set => scoreCorrectRoofTile = value;
    }
}
