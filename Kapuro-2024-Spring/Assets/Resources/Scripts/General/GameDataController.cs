using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataController : SingletonWithMonoBehaviour<GameDataController>
{
    //GameDataControllerの破棄フラグ
    [SerializeField] private bool isGameDataControllerDestroyFlag = false;
    public bool IsGameDataControllerDestroyFlag
    {
        get => isGameDataControllerDestroyFlag;
        set => isGameDataControllerDestroyFlag = value;
    }

    /*ここにシーンをまたいで引き継ぎたいデータを書く*/
    //[SerializeField] private int score = 0;
    
    //ここで生成時の処理を追加
    protected override void onAwakeProcess()
    {
        base.onAwakeProcess();
        if (IsGameDataControllerDestroyFlag == false)
        {
            DontDestroyOnLoad(gameObject); //シーンをまたいでもオブジェクトが破棄されないようにする
        }
    }
    
    //ここで破棄時の処理を追加
    protected override void onDestroyProcess()
    {
        base.onDestroyProcess();
    }
}
