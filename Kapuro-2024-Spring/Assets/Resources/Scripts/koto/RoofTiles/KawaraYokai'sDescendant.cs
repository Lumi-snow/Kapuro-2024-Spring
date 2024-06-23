using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KawaraYokaisDescendant : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.KAWARA_YOKAI_DESCENDANT; // roofTileType プロパティをオーバーライド
    public override EvaluateType evaluateType { get; set; } // evaluateType プロパティをオーバーライド

    private int KawaraYokaisDescendantscore = 1000;
    public override int Score
    {
        get => KawaraYokaisDescendantscore;
        set => KawaraYokaisDescendantscore = value;
    }
    
    private int KawaraYokaisDescendantAtackPower = 15;
    public override int AttackPower
    {
        get => KawaraYokaisDescendantAtackPower;
        set => KawaraYokaisDescendantAtackPower = value;
    }
}
