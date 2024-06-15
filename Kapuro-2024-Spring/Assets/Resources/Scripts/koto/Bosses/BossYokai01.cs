using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossYokai01 : AbstractBoss
{
    [SerializeField] private Slider hpSlider;
    public override float HpSlider { get => hpSlider.value; set => hpSlider.value = value; }
    
    public override BossType bossType => BossType.KAWARA_YOKAI; // bossType プロパティをオーバーライド
    
    public const int hpMax = 1000; // 最大HP
    public override int HpMax { get => hpMax;}
    
    private int kawaraYokaihp = 1000;
    public override int Hp
    {
        get => kawaraYokaihp;
        set => kawaraYokaihp = value;
    }

    private int allKawaraYokaisDescendantNum = 50;
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
    
    private bool isKawaraYokaiHpHalf = false;
    public override bool IsBossHpHalf
    {
        get => isKawaraYokaiHpHalf;
        set => isKawaraYokaiHpHalf = value;
    }
}
