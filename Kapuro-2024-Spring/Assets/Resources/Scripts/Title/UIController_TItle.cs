using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController_Title : MonoBehaviour
{
    //Buttons
    [SerializeField] private Button buttonScene01; //[SerializeField]フィールド(変数)をインスペクタ(右のウィンドウ)から編集できるようにする
    [SerializeField] private Button buttonScene02;
    [SerializeField] private Button buttonScene03;
    [SerializeField] private Button buttonScene04;
    [SerializeField] private Button buttonHowToPlay;

    //texts
    [SerializeField] private TextMeshProUGUI textTitle;

    //Buttonの配列　イベントハンドラとシーンの名前を格納
    private List<SceneButton> sceneButtons = new List<SceneButton>();

    private void Start()
    {
        AddSceneButton(buttonScene01, "Scene01");
        AddSceneButton(buttonScene02, "Scene02");
        AddSceneButton(buttonScene03, "Scene03");
        AddSceneButton(buttonScene04, "Scene04");
        AddSceneButton(buttonHowToPlay, "HowToPlay");
    }

    private void AddSceneButton(Button button, string sceneName)
    {
        SceneButton sceneButton = button.gameObject.AddComponent<SceneButton>();
        sceneButton.SceneName = sceneName; //〇〇.アクセサ名でアクセサにアクセスすることができる
        sceneButtons.Add(sceneButton);
        button.onClick.AddListener(sceneButton.onClick);
    }
}
