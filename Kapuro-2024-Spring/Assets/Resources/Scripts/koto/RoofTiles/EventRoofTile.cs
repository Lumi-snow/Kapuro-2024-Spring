using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstantNumberKoto;
using Cysharp.Threading.Tasks;

public class EventRoofTile : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.EVENT; // roofTileType プロパティをオーバーライド
    public override EvaluateType evaluateType { get; set; } // evaluateType プロパティをオーバーライド
    
    private ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE eventType; //イベントの種類
    public ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE EventType //イベントの種類のプロパティ
    {
        get => eventType;
    }

    /*共通のメンバ変数*/
    [SerializeField] private int scoreBrokenRoofTile = 50; //スコア
    
    /*共通のプロパティ*/
    public override int Score //スコアのプロパティ
    {
        get => scoreBrokenRoofTile;
        set => scoreBrokenRoofTile = value;
    }
    
    /*共通のメンバ関数*/
    public override async UniTask OnDestroyProcess()
    {
        await UniTask.Yield();
    }
    
    /*固有のメンバ変数*/
    
    /*固有のプロパティ*/
}
