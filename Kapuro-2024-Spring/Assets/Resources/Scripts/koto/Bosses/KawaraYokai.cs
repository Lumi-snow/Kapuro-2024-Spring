using UnityEngine;
using UnityEngine.UI;

public class KawaraYokai : AbstractBoss
{
    public override BossType bossType => BossType.KAWARA_YOKAI; // bossType プロパティをオーバーライド
    
    /*共通のメンバ変数*/
    [SerializeField] private Slider kawaraYokaiHpSlider; //HPスライダー
    [SerializeField] private const int hpMax = 1000; // 最大HP
    [SerializeField] int kawaraYokaihp = 1000; //HP
    [SerializeField] bool isKawaraYokaiHpHalf = false; //HPが半分かどうか
    [SerializeField] bool isLocationSet = false; //位置が設定されているかどうか
    
    /*共通のプロパティ*/
    public override float HpSlider { get => kawaraYokaiHpSlider.value; set => kawaraYokaiHpSlider.value = value; }
    
    public override int HpMax { get => hpMax;}
    
    public override int Hp
    {
        get => kawaraYokaihp;
        set => kawaraYokaihp = value;
    }
    
    public override bool IsBossHpHalf
    {
        get => isKawaraYokaiHpHalf;
        set => isKawaraYokaiHpHalf = value;
    }
    
    public override bool IsLocationSet
    {
        get => isLocationSet;
        set => isLocationSet = value;
    }
    
    /*共通のメンバ関数*/
    public override void SetMyself()
    {
        transform.localPosition = new Vector3(0, 300, 0);
        transform.localScale = new Vector3(50, 50, 0);
        HpSlider = Hp;
    }
    
    /*固有のメンバ変数*/
    [SerializeField] int allKawaraYokaisDescendantNum = 50;
    [SerializeField] bool isAllKawaraYokaisDescendantDead = false;
    
    /*固有のプロパティ*/
    public override int AllDescendantNum 
    { 
        get => this.allKawaraYokaisDescendantNum;
        set => this.allKawaraYokaisDescendantNum = value;
    }
    
    public override bool IsAllDescendantDead
    {
        get => isAllKawaraYokaisDescendantDead; 
        set => isAllKawaraYokaisDescendantDead = value;
    }
    
    /*固有メンバ関数*/
    public override void AttackKawaraYokai(int attackPower)
    {
        Hp -= attackPower;
        HpSlider -= attackPower;
    }
}
