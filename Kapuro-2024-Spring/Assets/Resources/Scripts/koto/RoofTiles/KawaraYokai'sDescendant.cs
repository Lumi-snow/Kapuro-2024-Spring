using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KawaraYokaisDescendant : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.KAWARA_YOKAI_DESCENDANT; // roofTileType プロパティをオーバーライド
    public override EvaluateType evaluateType { get; set; } // evaluateType プロパティをオーバーライド

    /*共通のメンバ変数*/
    [SerializeField] int KawaraYokaisDescendantscore = 500;
    
    /*共通のプロパティ*/
    public override int Score
    {
        get => KawaraYokaisDescendantscore;
        set => KawaraYokaisDescendantscore = value;
    }
    
    /*固有のメンバ変数*/
    [SerializeField] private int KawaraYokaisDescendantAtackPower = 22;
    
    /*固有のプロパティ*/
    public override int AttackPower
    {
        get => KawaraYokaisDescendantAtackPower;
        set => KawaraYokaisDescendantAtackPower = value;
    }
}
