using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController_HowToPlay : MonoBehaviour
{
    public static UIController_HowToPlay Instance { get; private set; } //自身のインスタンス

    //Buttons
    [Header("シーン変更 - button")]
    [SerializeField] private Button buttonBackToTitle; //[SerializeField]フィールド(変数)をインスペクタ(右のウィンドウ)から編集できるようにする

    //Panels
    List<Panel> panelList = new List<Panel>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; //自身のインスタンスを設定
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //ButtonにClickEventを追加
        buttonBackToTitle.onClick.AddListener(OnClickButtonBackToTitle); //クリックされたら自動で引数の中の関数を実行

        //panelの初期化
        initializePanels();
    }

    private void OnClickButtonBackToTitle()
    {
        SceneController.ChangeSceneToTitle();
    }

    public void showNextPanel()
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if (panelList[i].isActive() == true) //自身がアクティブか確認
            {
                panelList[i].hide(); //現在のパネルを非アクティブにする
                panelList[(i + 1) % panelList.Count].show(); //次のパネルをアクティブにする
                break;
            }
        }
    }

    public void hidePrevPanel()
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if (panelList[i].isActive() == true)
            {
                panelList[i].hide();
                panelList[(i - 1 + panelList.Count) % panelList.Count].show(); //前のパネルをアクティブにする
                break;
            }
        }
    }

    private void initializePanels()
    {
        //パネルを動的に取得してリストに追加
        for (int i = 1; i <= 4; i++)
        {
            string panelName = "PanelScene0" + i.ToString(); //"PanelScene01", "PanelScene02", "PanelScene03", "PanelScene04"を探す
            GameObject panelObject = GameObject.Find(panelName); //panelNameをもとにpanelObjectを取得

            if(panelObject != null)
            {
                Panel panel = panelObject.GetComponent<Panel>(); //panelObjectからPanelコンポーネントを取得(アタッチされているスクリプトのこと)

                if(panel != null)
                {
                    panel.initialize(); //初期化
                    panelList.Add(panel); //Listに追加
                    panel.hide(); //非表示にする

                    Debug.Log("Add " + panelName + "to panelList");
                }
                else
                {
                    Debug.LogWarning("Error occured while panel adding");
                }
            }
            else
            {
                Debug.LogWarning("Error occured while panel searching");
            }
        }

        //パネルの追加に成功した場合、1番目のパネルを表示
        if(panelList.Count > 0)
        {
            panelList[0].show();
        }
    }
}
