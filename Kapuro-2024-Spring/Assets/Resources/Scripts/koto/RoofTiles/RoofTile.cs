using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

//���̒��ۃN���X
public abstract class RoofTile : MonoBehaviour
{
    public enum RoofTileType //���̎��
    {
        NORMAL,
        EXPENSIVE,
        LEGEND,
        BROKEN,
        EVENT,
        KAWARA_YOKAI_DESCENDANT,
        SHISHIGAWARA_WATER,
        SHISHIGAWARA_WHISTLE,
        SHISHIGAWARA_EVENT,
        KAWARA_BOUZU_ABURA,
        KAWARA_BOUZU_MAME,
        KAWARA_BOUZU_KYOU,
        KAWARA_BOUZU_EVENT,
    }

    public enum EvaluateType //�]���̎��
    {
        CORRECT,
        INCORRECT,
        NOT_EVALUATED,
    }
    
    public abstract RoofTileType roofTileType { get; } // RoofTileType �𒊏ۃv���p�e�B�Ƃ��Đ錾
    public abstract EvaluateType evaluateType { get; set; } // EvaluateType �𒊏ۃv���p�e�B�Ƃ��Đ錾
    
    /*���ʂ̃v���p�e�B*/
    public abstract int Score { get; set; }
    
    /*���ʂ̃����o�֐�*/
    public abstract UniTask OnDestroyProcess();

    /*KawaraYokai�̌ŗL�̃v���p�e�B*/
    public virtual int AttackPower { get; set; }
    
    /*ShishiGawaraWaterRoofTile�̌ŗL�̃v���p�e�B*/
    public virtual int AwakingPoint { get; set; }
    public virtual int AwakingPointPower { get; set; }
    
    /*ShishiGawaraWhistle�̌ŗL�v���p�e�B*/
    public virtual int ShishiGawaraWhistleAttackPower { get; set; }
    
    /*ShishiGawaraEventRoofTile�̌ŗL�v���p�e�B*/
    public virtual bool IsFinishEventShishiGawara { get; }
    
    /*ShishiGawaraEventRoofTile�̌ŗL�����o�֐�*/
    public virtual void InitializeShishiGawaraMessageEvent() { }
    public virtual void ShishiGawaraMessageEvent() { }
    
    /*KawaraBouzuAburaRoofTile�̌ŗL�v���p�e�B*/
    public virtual int KawaraBouzuAburaRoofTileAttackPower { get; set; }
    
    /*KawaraBouzuMameRoofTile�̌ŗL�v���p�e�B*/
    public virtual int KawaraBouzuMameRoofTileAttackPower { get; set; }
    
    /*KawaraBouzuKyouRoofTile�̌ŗL�v���p�e�B*/
    public virtual int KawaraBouzuKyouRoofTileAttackPower { get; set; }
    
    /*KawaraBouzuEventRoofTile�̌ŗL�v���p�e�B*/
    public virtual bool IsFinishEventKawaraBouzu { get; }
    public virtual bool IsNextMessageKawaraBouzu { get; set; }
    
    /*KawaraBouzuEventRoofTile�̌ŗL�v���p�e�B*/
    public virtual void InitializeKawaraBouzuEvent() { }
    public virtual async UniTask KawaraBouzuMessageEvent() { }
}
