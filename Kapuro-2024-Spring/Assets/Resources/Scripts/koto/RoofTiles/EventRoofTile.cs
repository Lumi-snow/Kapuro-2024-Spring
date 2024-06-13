using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstantNumberKoto;

public class EventRoofTile : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.EVENT; // roofTileType プロパティをオーバーライド
    public override EvaluateType evaluateType { get; set; } // evaluateType プロパティをオーバーライド
    
    private ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE eventType; //イベントの種類
    public ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE EventType //イベントの種類のプロパティ
    {
        get => eventType;
    }

    private int scoreBrokenRoofTile = 50; //スコア
    public override int Score //スコアのプロパティ
    {
        get => scoreBrokenRoofTile;
        set => scoreBrokenRoofTile = value;
    }
}
