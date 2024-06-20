using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractBoss : MonoBehaviour
{
    public enum BossType //ボスの種類
    {
        KAWARA_YOKAI,
        SHISHIGAWARA,
        KAWARA_BOUZU
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
    
    /*KawaraBouzu固有*/
    //プロパティ
    public virtual Slider BouzuHpSlider { get; set; }
    public virtual int BouzuHp { get; set; }
    public virtual int AllKyouNum { get; set; }
    public virtual int AllAburaNum { get; set; }
    public virtual int AllMameNum { get; set; }
    public virtual bool IsGenerateKawaraBouzuRoofTile { get; set; }
    public virtual bool IsCorrectAnswerKawaraBouzu { get; set; }
    public virtual bool IsFinishEventKawaraBouzu { get; set; }
    
    //メンバ関数
    public virtual void AttackBouzu(int attackPower) { }
    public virtual void AttackKawaraBouzu(int attackPower) { }
    public virtual void GenerateKawaraBouzuAburaRoofTile(RoofTileController roofTileController, GameObject roofTile) { }
    public virtual void GenerateKawaraBouzuKyouRoofTile(RoofTileController roofTileController, GameObject roofTile) { }
    public virtual void GenerateKawaraBouzuMameRoofTile(RoofTileController roofTileController, GameObject roofTile) { }
    public virtual void GenerateKawaraBouzuEventRoofTile(RoofTileController roofTileController, GameObject roofTile) { }
}
