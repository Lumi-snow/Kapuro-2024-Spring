using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

//���ʂ̊�
public class CorrectRoofTile : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.NORMAL; // roofTileType �v���p�e�B���I�[�o�[���C�h
    public override EvaluateType evaluateType { get; set; } // evaluateType �v���p�e�B���I�[�o�[���C�h

    /*���ʂ̃����o�ϐ�*/
    [SerializeField] private int scoreCorrectRoofTile = 100; //�X�R�A
    
    /*���ʂ̃v���p�e�B*/
    public override int Score //�X�R�A�̃v���p�e�B
    {
        get => scoreCorrectRoofTile;
        set => scoreCorrectRoofTile = value;
    }
    
    /*���ʂ̃����o�֐�*/
    public override async UniTask OnDestroyProcess()
    {
        await UniTask.Yield();
    }
}
