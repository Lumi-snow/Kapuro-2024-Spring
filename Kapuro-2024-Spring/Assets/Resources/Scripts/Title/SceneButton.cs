using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButton : MonoBehaviour, ISceneButton
{
    [SerializeField] private string sceneName; //シーンの名前

    //アクセサ
    public string SceneName
    {
        set //setSceneName()
        {
            sceneName = value; //value(名前の変更不可)で値を設定する
        }
        get //getSceneName()
        {
            return sceneName;
        }
    }

    public void onClick()
    {
        SceneController.ChangeScene(sceneName);
    }
}
