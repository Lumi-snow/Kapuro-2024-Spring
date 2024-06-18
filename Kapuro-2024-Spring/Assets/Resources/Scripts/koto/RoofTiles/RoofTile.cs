using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

//瓦の抽象クラス
public abstract class RoofTile : MonoBehaviour
{
    public enum RoofTileType //瓦の種類
    {
        NORMAL,
        EXPENSIVE,
        LEGEND,
        BROKEN,
        EVENT,
        KAWARA_YOKAI_DESCENDANT,
        SHISHIGAWARA_WATER,
        SHISHIGAWARA_WHISTLE,
        SHISHIGAWARA_EVENT,
    }

    public enum EvaluateType //評価の種類
    {
        CORRECT,
        INCORRECT,
        NOT_EVALUATED,
    }
    
    public abstract RoofTileType roofTileType { get; } // RoofTileType を抽象プロパティとして宣言
    public abstract EvaluateType evaluateType { get; set; } // EvaluateType を抽象プロパティとして宣言
    
    /*共通のプロパティ*/
    public abstract int Score { get; set; }
    
    /*共通のメンバ関数*/
    public abstract UniTask OnDestroyProcess();

    /*KawaraYokaiの固有のプロパティ*/
    public virtual int AttackPower { get; set; }
    
    /*ShishiGawaraWaterRoofTileの固有のプロパティ*/
    public virtual int AwakingPoint { get; set; }
    public virtual int AwakingPointPower { get; set; }
    
    /*ShishiGawaraWhistleの固有プロパティ*/
    public virtual int ShishiGawaraWhistleAttackPower { get; set; }
    
    /*ShishiGawaraEventRoofTileの固有プロパティ*/
    public virtual bool IsFinishEvent { get; }
    
    /*ShishiGawaraEventRoofTileの固有メンバ関数*/
    public virtual void InitializeShishiGawaraMessageEvent() { }
    public virtual void ShishiGawaraMessageEvent() { }
}
