using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractBoss : MonoBehaviour
{
    public enum BossType //ボスの種類
    {
        KAWARA_YOKAI,
        SHISHIGAWARA,
    };
    public abstract BossType bossType { get; }
    
    /*共通のメンバ変数*/
    
    /*共通のプロパティ*/
    public abstract float HpSlider { get; set; }
    public abstract int HpMax { get; } //最大HP
    public abstract int Hp { get; set; } //HP
    public abstract bool IsBossHpHalf { get; set; } //HPが半分かどうか
    public abstract bool IsLocationSet { get; set; } //位置が設定されているかどうか
    
    /*ここに共通のメンバ関数*/
    public abstract void SetMyself();
    public abstract void Initialize();
    
    /*KawaraYokai固有*/
    //プロパティ
    public virtual bool IsAllDescendantDead { get; set; }
    public virtual int AllDescendantNum { get; set; }

    //メンバ関数
    public virtual void AttackKawaraYokai(int attackPower) { }
    public virtual void GenerateKawaraYokaiDescendant(RoofTileController roofTileController, GameObject roofTile) { }

    /*ShishiGawara固有*/
    //プロパティ
    public virtual Slider AwakingPointSlider { get; set; }
    public virtual int MaxAwakingPoint { get; }
    public virtual float AwakingPoint { get; set; }
    public virtual int AllShishiGawaraWhistleNum { get; set; }
    public virtual int AllShishiGawaraWaterRoofTileNum { get; set; }
    public virtual bool IsAwaking { get; set; }
    public virtual bool IsGenerateShishiGawaraWaterRoofTile { get; set; }
    
    //メンバ関数
    public virtual void AddAwakingPoint(float awakingPoint) { }
    public virtual void AttackShishiGawara(int attackPower) { }
    public virtual void GenerateShishiGawaraWaterRoofTile(RoofTileController roofTileController, GameObject roofTile) { }
    public virtual void GenerateShishiGawaraWhistle(RoofTileController roofTileController, GameObject roofTile) { }
    public virtual void GenerateShishiGawaraEventRoofTile(RoofTileController roofTileController, GameObject roofTile) { }
}
