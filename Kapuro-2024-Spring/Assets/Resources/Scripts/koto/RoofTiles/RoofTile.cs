using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//瓦の抽象クラス
public abstract class RoofTile : MonoBehaviour
{
    public enum RoofTileType
    {
        NORMAL,
        BROKEN,
        EVENT,
        KAWARA_YOKAI_DESCENDANT,
    }

    public enum EvaluateType
    {
        CORRECT,
        INCORRECT,
        NOT_EVALUATED,
    }
    
    public abstract RoofTileType roofTileType { get; } // RoofTileType を抽象プロパティとして宣言
    public abstract EvaluateType evaluateType { get; set; } // EvaluateType を抽象プロパティとして宣言
    
    public abstract int Score { get; set; }
    public abstract int AttackPower { get; set; }
}
