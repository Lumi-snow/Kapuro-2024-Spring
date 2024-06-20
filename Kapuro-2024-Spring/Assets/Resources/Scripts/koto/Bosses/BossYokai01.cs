using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossYokai01 : AbstractBoss
{
    [SerializeField] private Slider hpSlider;
    public override float HpSlider { get => hpSlider.value; set => hpSlider.value = value; }
    
    public override BossType bossType => BossType.KAWARA_YOKAI; // bossType プロパティをオーバーライド
    
    private int kawaraYokaihp = 300;

    public override int Hp
    {
        get => kawaraYokaihp;
        set => kawaraYokaihp = value;
    }

    private int allKawaraYokaisDescendantNum = 30;
    public override int AllDescendantNum 
    { 
        get => this.allKawaraYokaisDescendantNum;
        set => this.allKawaraYokaisDescendantNum = value;
    }
    
    private bool isAllKawaraYokaisDescendantDead = false;

    public override bool IsAllDescendantDead
    {
        get => isAllKawaraYokaisDescendantDead; 
        set => isAllKawaraYokaisDescendantDead = value;
    }
}
