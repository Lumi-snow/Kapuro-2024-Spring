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
    
    /*KawaraYokaiの固有のプロパティ*/
    public virtual int AttackPower { get; set; }
    
    /*ShishiGawaraWaterRoofTileの固有のプロパティ*/
    public virtual int AwakingPoint { get; set; }
    public virtual int AwakingPointPower { get; set; }
    
    /*ShishiGawaraWhistleの固有プロパティ*/
    public virtual int ShishiGawaraWhistleAttackPower { get; set; }
}
