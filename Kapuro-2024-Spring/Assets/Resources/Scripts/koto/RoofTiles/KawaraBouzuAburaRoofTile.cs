using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class KawaraBouzuAburaRoofTile : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.KAWARA_BOUZU_ABURA; // roofTileType プロパティをオーバーライド
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
    [SerializeField] private int kawaraBouzuAburaRoofTileAttackPower = 30; //攻撃力
    
    /*固有のプロパティ*/
    public override int KawaraBouzuAburaRoofTileAttackPower { get => kawaraBouzuAburaRoofTileAttackPower; set => kawaraBouzuAburaRoofTileAttackPower = value; }
    
    /*固有のメンバ関数*/
}
