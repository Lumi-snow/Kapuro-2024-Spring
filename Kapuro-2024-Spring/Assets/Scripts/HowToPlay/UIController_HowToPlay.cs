using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController_HowToPlay : MonoBehaviour
{
    //Buttons
    [Header("シーン変更 - button")]
    [SerializeField] private Button buttonBackToTitle; //[SerializeField]フィールド(変数)をインスペクタ(右のウィンドウ)から編集できるようにする

    //Panels
    List<GameObject> panelList = new List<GameObject>();

    [Header("遊び方 - panel")]
    [Header("Panel01")]
    [SerializeField] private GameObject panelScene01;
    [SerializeField] private Button panelScene01NextButton;
    [SerializeField] private Button panelScene01PrevButton;
    [SerializeField] private TextMeshProUGUI panelScene01Title;

    [Header("Panel02")]
    [SerializeField] private GameObject panelScene02;
    [SerializeField] private Button panelScene02NextButton;
    [SerializeField] private Button panelScene02PrevButton;
    [SerializeField] private TextMeshProUGUI panelScene02Title;

    [Header("Panel03")]
    [SerializeField] private GameObject panelScene03;
    [SerializeField] private Button panelScene03NextButton;
    [SerializeField] private Button panelScene03PrevButton;
    [SerializeField] private TextMeshProUGUI panelScene03Title;

    [Header("Panel04")]
    [SerializeField] private GameObject panelScene04;
    [SerializeField] private Button panelScene04NextButton;
    [SerializeField] private Button panelScene04PrevButton;
    [SerializeField] private TextMeshProUGUI panelScene04Title;

    private void Start()
    {
        //ButtonにClickEventを追加
        buttonBackToTitle.onClick.AddListener(OnClickButtonBackToTitle); //クリックされたら自動で引数の中の関数を実行

        panelScene01NextButton.onClick.AddListener(OnClickButtonNextPanel);
        panelScene02NextButton.onClick.AddListener(OnClickButtonNextPanel);
        panelScene03NextButton.onClick.AddListener(OnClickButtonNextPanel);
        panelScene04NextButton.onClick.AddListener(OnClickButtonNextPanel);

        panelScene01PrevButton.onClick.AddListener(OnClickButtonPrevPanel);
        panelScene02PrevButton.onClick.AddListener(OnClickButtonPrevPanel);
        panelScene03PrevButton.onClick.AddListener(OnClickButtonPrevPanel);
        panelScene04PrevButton.onClick.AddListener(OnClickButtonPrevPanel);

        //panelの初期化
        initialPanel();
    }

    private void OnClickButtonBackToTitle()
    {
        SceneController.ChangeSceneToTitle();
    }

    private void OnClickButtonNextPanel()
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if (panelList[i].activeSelf == true) //自身がアクティブか確認
            {
                panelList[i].SetActive(false); //現在のパネルを非アクティブにする
                panelList[(i + 1) % panelList.Count].SetActive(true); //次のパネルをアクティブにする
                break;
            }
        }
    }

    private void OnClickButtonPrevPanel()
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if (panelList[i].activeSelf == true)
            {
                panelList[i].SetActive(false);
                panelList[(i - 1 + panelList.Count) % panelList.Count].SetActive(true); //前のパネルをアクティブにする
                break;
            }
        }
    }

    private void initialPanel()
    {
        //パネルを動的に取得してリストに追加
        for (int i = 1; i <= 4; i++)
        {
            string panelName = "PanelScene0" + i.ToString(); //"PanelScene01", "PanelScene02", "PanelScene03", "PanelScene04"を探す
            GameObject panel = GameObject.Find(panelName);

            //例外処理
            if (panel != null)
            {
                panelList.Add(panel);
                Debug.Log("Added " + panelName + " to panelList");
            }
            else
            {
                Debug.LogWarning("Panel " + panelName + " not found!");
            }
        }

        panelScene01.SetActive(true);
        panelScene02.SetActive(false);
        panelScene03.SetActive(false);
        panelScene04.SetActive(false);
    }
}
