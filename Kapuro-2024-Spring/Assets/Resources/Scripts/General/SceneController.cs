using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //シーン管理に必須

public static class SceneController
{
    public static void ChangeSceneToTitle()
    {
        SceneManager.LoadScene("Title");
        Debug.Log("Titleに移動しました");
    }

    public static void ChangeSceneToScene01()
    {
        SceneManager.LoadScene("Scene01");
        Debug.Log("Scene01に移動しました");
    }

    public static void ChangeSceneToScene02()
    {
        SceneManager.LoadScene("Scene02");
        Debug.Log("Scene02に移動しました");
    }

    public static void ChangeSceneToScene03()
    {
        SceneManager.LoadScene("Scene03");
        Debug.Log("Scene03に移動しました");
    }

    public static void ChangeSceneToScene04()
    {
        SceneManager.LoadScene("Scene04");
        Debug.Log("Scene04に移動しました");
    }

    public static void ChangeSceneToHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
        Debug.Log("HowToPlayに移動しました");
    }

    public static void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log(sceneName + "に移動しました");
    }
}

/*シーンの追加方法*/

///1,Unityエディタ左上のFileからNew Sceneを選択
///2,Unityエディタ左上のFileからSave as...でScenesフォルダにSceneを保存
///3,Unityエディタ左上のBuild settingsからAdd open scenesで現在のシーンを追加する
///4,ここに関数を書く(static修飾子を忘れずに！)
///5,クラス名.関数名でシーンを呼び出す
