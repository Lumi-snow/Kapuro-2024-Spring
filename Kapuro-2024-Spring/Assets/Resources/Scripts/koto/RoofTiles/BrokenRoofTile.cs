using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

//��ꂽ��
public class BrokenRoofTile : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.BROKEN; // roofTileType �v���p�e�B���I�[�o�[���C�h
    public override EvaluateType evaluateType { get; set; } // evaluateType �v���p�e�B���I�[�o�[���C�h
    
    /*���ʂ̃����o�ϐ�*/
    [SerializeField] private int scoreBrokenRoofTile = 50; //�X�R�A
    
    /*���ʂ̃v���p�e�B*/
    public override int Score //�X�R�A�̃v���p�e�B
    {
        get => scoreBrokenRoofTile;
        set => scoreBrokenRoofTile = value;
    }
    
    /*���ʂ̃����o�֐�*/
    public override async UniTask OnDestroyProcess()
    {
        await UniTask.Yield();
    }
    
    /*�ŗL�̃����o�ϐ�*/
    
    /*�ŗL�̃v���p�e�B*/
}
