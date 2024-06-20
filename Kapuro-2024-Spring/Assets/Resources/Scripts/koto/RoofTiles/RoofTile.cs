using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���̒��ۃN���X
public abstract class RoofTile : MonoBehaviour
{
    public enum RoofTileType
    {
        NORMAL,
        BROKEN,
        EVENT,
        KAWARA_YOKAI_DESCENDANT,
    }

    public enum EvaluateType
    {
        CORRECT,
        INCORRECT,
        NOT_EVALUATED,
    }
    
    public abstract RoofTileType roofTileType { get; } // RoofTileType �𒊏ۃv���p�e�B�Ƃ��Đ錾
    public abstract EvaluateType evaluateType { get; set; } // EvaluateType �𒊏ۃv���p�e�B�Ƃ��Đ錾
    
    public abstract int Score { get; set; }
    public abstract int AttackPower { get; set; }
}
