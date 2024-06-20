using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AudioController;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class UIControllerKoto : MonoBehaviour
{
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private PrefabController prefabController;
    [SerializeField] private AddGameObjectController addGameObjectController;

    [SerializeField] private TextMeshProUGUI scoreText; //スコアを表示するテキスト
    
    [SerializeField] private GameObject popupParent; //親となるオブジェクト
    [SerializeField] private List<GameObject> popupPrefabList; //プレハブのリスト

    [SerializeField] private List<GameObject> popupListForBoss; //ボス出現時のポップアップのリスト

    [SerializeField] private GameObject eventMessagePrefab; //イベントメッセージのプレハブ

    [SerializeField] private GameObject resultPanel; //リザルトパネル
    [SerializeField] private TextMeshProUGUI resultText; //リザルトテキスト

    //初期化
    public void Initialize()
    {
        //空のゲームオブジェクトをCanvasの子として生成
        addGameObjectController.SetPairGameObject("PopUp", "Canvas");
        addGameObjectController.AddGameObject();
        popupParent = addGameObjectController.NewGameObject;
        foreach(GameObject popupPrefab in popupPrefabList)
            prefabController.AddNewPrefab(popupPrefab);
        prefabController.AddNewPrefab(eventMessagePrefab);
        
        /*ResultPanel*/
        resultPanel.gameObject.SetActive(false);
    }
    
    //スコアの表示を更新
    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + scoreController.AccumulatedScore;
    }

    //ボス出現時のポップアップを生成
    public void OnExpressBossExpressionText(string bossName)
    {
        StringBuilder newBossName = new StringBuilder();
        newBossName.Append(bossName);
        newBossName.Replace("(Clone)", "");
        
        prefabController.InstantiatePrefab("BossExpression", Vector3.zero, Quaternion.identity, popupParent);
        
        GameObject bossExpressionPopup = prefabController.clonePrefab;
        GameObject Text = bossExpressionPopup.transform.GetChild(0).gameObject;
        Text.GetComponent<TextMeshProUGUI>().text = newBossName + " Appeared!";
        
        popupListForBoss.Add(bossExpressionPopup);
        bossExpressionPopup.GetComponent<Transform>().transform.localPosition = new Vector3(1200, 200, 0);
        SEController.Instance.Play(SEPath.OnExpressMessage);
        bossExpressionPopup.GetComponent<Animator>().SetTrigger("OnBossExpression");
    }

    //スペースを押すことを表示するポップアップを生成
    public void PressSpaceText()
    {
        prefabController.InstantiatePrefab("PressSpace", Vector3.zero, Quaternion.identity, popupParent);
        GameObject pressSpacePopup = prefabController.clonePrefab;
        popupListForBoss.Add(pressSpacePopup);
        pressSpacePopup.GetComponent<Transform>().transform.position = new Vector3(0, -500, 0);
        SEController.Instance.Play(SEPath.OnExpressMessage);
    }

    //ボス撃破時のポップアップを破棄
    public void DeflaggerForBossDestroy()
    {
        foreach(GameObject popup in popupListForBoss)
        {
            Destroy(popup);
        }
        
        popupListForBoss.Clear();
    }

    public void OnStartMessage()
    {
        
    }

    public async UniTask ResultMessage()
    {
        StringBuilder stringBuilder = new StringBuilder();
        resultPanel.gameObject.SetActive(true);
        stringBuilder.Append(resultText.text + "\n");
        await UniTask.Delay(1000);
        stringBuilder.Append("あなたの総スコアは\n");
        await UniTask.Delay(3000);
        stringBuilder.Append(scoreController.AccumulatedScore.ToString() + "　点\n");
        await UniTask.Delay(1000);
        stringBuilder.Append("でした");
        await UniTask.Delay(500);
    }
}
