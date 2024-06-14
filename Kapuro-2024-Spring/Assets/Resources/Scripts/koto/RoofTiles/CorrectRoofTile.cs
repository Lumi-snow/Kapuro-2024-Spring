using System.Collections;
using System.Collections.Generic;

//���ʂ̊�
public class CorrectRoofTile : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.NORMAL; // roofTileType �v���p�e�B���I�[�o�[���C�h
    public override EvaluateType evaluateType { get; set; } // evaluateType �v���p�e�B���I�[�o�[���C�h

    private int scoreCorrectRoofTile = 100; //�X�R�A
    public override int Score //�X�R�A�̃v���p�e�B
    {
        get => scoreCorrectRoofTile;
        set => scoreCorrectRoofTile = value;
    }
    
    private int correctRoofTileAtackPower = 0; //�U����
    public override int AttackPower //�U���͂̃v���p�e�B
    {
        get => correctRoofTileAtackPower;
        set => correctRoofTileAtackPower = value;
    }
}
