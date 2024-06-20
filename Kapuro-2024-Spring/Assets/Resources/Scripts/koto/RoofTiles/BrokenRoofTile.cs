using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ꂽ��
public class BrokenRoofTile : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.BROKEN; // roofTileType �v���p�e�B���I�[�o�[���C�h
    public override EvaluateType evaluateType { get; set; } // evaluateType �v���p�e�B���I�[�o�[���C�h

    private int scoreBrokenRoofTile = 50; //�X�R�A
    public override int Score //�X�R�A�̃v���p�e�B
    {
        get => scoreBrokenRoofTile;
        set => scoreBrokenRoofTile = value;
    }
    
    private int brokenAtackPower = 0; //�U����
    public override int AttackPower //�U���͂̃v���p�e�B
    {
        get => brokenAtackPower;
        set => brokenAtackPower = value;
    }
}
