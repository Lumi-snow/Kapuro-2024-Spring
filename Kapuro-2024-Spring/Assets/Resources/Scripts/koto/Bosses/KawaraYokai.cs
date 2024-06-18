using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KawaraYokai : AbstractBoss
{
    public override BossType bossType => BossType.KAWARA_YOKAI; // bossType プロパティをオーバーライド
    
    /*共通のメンバ変数*/
    [SerializeField] private PrefabController prefabController; //PrefabController
    [SerializeField] private PrefabList prefabList; //PrefabList
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
    //座標を設定
    public override void SetMyself()
    {
        transform.localPosition = new Vector3(0, 300, 0);
        transform.localScale = new Vector3(50, 50, 1);
        HpSlider = Hp;
    }
    
    //初期化
    public override void Initialize()
    {
        prefabController = this.AddComponent<PrefabController>();
        prefabController.prefabList = prefabList;
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
    
    public override void GenerateKawaraYokaiDescendant(RoofTileController roofTileController, GameObject roofTile)
    {
        int randomValue = UnityEngine.Random.Range(3, (roofTileController.roofTiles.Count - 1) / 2); //どのタイミングで出現させるかをランダムに決定
        prefabController.InstantiatePrefab("KawaraYokai'sDescendant", Vector3.zero, Quaternion.identity, roofTile); //PrefabからKawaraYokaiDescendantを複製
        GameObject kawaraYokaisDescendant = prefabController.clonePrefab;
        kawaraYokaisDescendant.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //KawaraYokaiDescendantの評価をNOT_EVALUATEDに設定
        roofTileController.roofTiles.Insert(randomValue, kawaraYokaisDescendant); //複製したKawaraYokaiDescendantをリストに追加
    }
}
