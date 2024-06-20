using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class How2Displayer : MonoBehaviour, IPanel
{
    [Header("パネル名と各ボタン")]
    [SerializeField] private TextMeshProUGUI panelTitle; //パネルの名前
    [SerializeField] private Button nextButton; //次のパネルへのボタン
    [SerializeField] private Button prevButton; //前のパネルへのボタン

    public void initialize()
    {
        //ボタンクリック時の処理を登録
        nextButton.onClick.AddListener(onClickNextButton);
        prevButton.onClick.AddListener(onClickPrevButton);
    }

    //パネルを表示
    public void show()
    {
        gameObject.SetActive(true);
    }

    //パネルを非表示
    public void hide()
    {
        gameObject.SetActive(false);
    }

    //パネルがアクティブか非アクティブか
    public bool isActive()
    {
        return gameObject.activeSelf;
    }

    //次のパネルへのボタンが押された時の処理
    private void onClickNextButton()
    {
        UIController_HowToPlay.Instance.showNextPanel();
    }

    //前のパネルへのボタンが押された時の処理
    private void onClickPrevButton()
    {
        UIController_HowToPlay.Instance.hidePrevPanel();
    }
}
