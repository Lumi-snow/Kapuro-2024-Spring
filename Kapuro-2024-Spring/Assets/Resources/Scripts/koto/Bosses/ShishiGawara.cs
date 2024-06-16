using UnityEngine.UI;
using UnityEngine;

public class ShishiGawara : AbstractBoss
{
    public override BossType bossType => BossType.SHISHIGAWARA; // bossType プロパティをオーバーライド
    
    /*共通のメンバ変数*/
    [SerializeField] private Slider ShishiGawaraHpSlider;
    [SerializeField] private const int hpMax = 1000; // 最大HP
    [SerializeField] private int shishiGawaraHp = 1000; //HP
    [SerializeField] private bool isShishiGawaraHpHalf = false; //HPが半分かどうか
    [SerializeField] private bool isLocationSet = false; //位置が設定されているかどうか
    
    /*共通のプロパティ*/
    public override float HpSlider { get => ShishiGawaraHpSlider.value; set => ShishiGawaraHpSlider.value = value; }
    
    public override int HpMax { get => hpMax;}
    
    public override int Hp
    {
        get => shishiGawaraHp;
        set => shishiGawaraHp = value;
    }
    
    public override bool IsBossHpHalf
    {
        get => isShishiGawaraHpHalf;
        set => isShishiGawaraHpHalf = value;
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
        HpSlider = HpMax;
        AwakingPointSlider.value = 0;
    }
    
    /*固有のメンバ変数*/
    [SerializeField] private Slider awakingPointSlider; //覚醒ポイントのスライダー
    [SerializeField] private int shishiGawaraMaxAwakingPoint = 300; //最大覚醒ポイント
    [SerializeField] private float shishiGawaraAwakingPoint = 0; //覚醒ポイント
    [SerializeField] private int allShishiGawaraWaterRoofTileNum = 30; //水の音が鳴る瓦の総数
    [SerializeField] private int allshishiGawaraWhistleNum = 100; //水の音が鳴る瓦の数
    [SerializeField] private bool isShishiGawaraAwaking = false; //覚醒しているかどうか
    [SerializeField] private bool isGenerateShishiGawaraWaterRoofTile = false; //特殊瓦を生成するかどうか
    
    /*固有のプロパティ*/
    public override Slider AwakingPointSlider { get => awakingPointSlider; set => awakingPointSlider = value; }
    public override int MaxAwakingPoint { get => shishiGawaraMaxAwakingPoint; }
    public override float AwakingPoint { get => shishiGawaraAwakingPoint; set => shishiGawaraAwakingPoint = value; }
    public override int AllShishiGawaraWhistleNum { get => allshishiGawaraWhistleNum; set => allshishiGawaraWhistleNum = value; }
    public override int AllShishiGawaraWaterRoofTileNum { get => allShishiGawaraWaterRoofTileNum; set => allShishiGawaraWaterRoofTileNum = value; }
    public override bool IsAwaking { get => isShishiGawaraAwaking; set => isShishiGawaraAwaking = value; }
    public override bool IsGenerateShishiGawaraWaterRoofTile { get => isGenerateShishiGawaraWaterRoofTile; set => isGenerateShishiGawaraWaterRoofTile = value; }
    
    /*固有メンバ関数*/
    public override void AddAwakingPoint(float awakingPoint)
    {
        AwakingPoint += awakingPoint;
        AwakingPointSlider.value += awakingPoint;
    }

    public override void AttackShishiGawara(int attackPower)
    {
        Hp -= attackPower;
        HpSlider -= attackPower;
    }
}
