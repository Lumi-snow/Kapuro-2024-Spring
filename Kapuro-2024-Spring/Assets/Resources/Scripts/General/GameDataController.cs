using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataController<TYpe> : SingletonWithMonoBehaviour<TYpe> where TYpe : MonoBehaviour
{
    /*ここにシーンをまたいで引き継ぎたいデータを書く*/
    
    //ここで初期化
    protected override void onAwakeProcess()
    {
        base.onAwakeProcess();
    }
}
