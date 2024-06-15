using System.Collections;
using System.Collections.Generic;
using AudioController;
using TMPro;
using UnityEngine;

public class UIControllerKoto : MonoBehaviour
{
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private PrefabController prefabController;
    [SerializeField] private AddGameObjectController addGameObjectController;

    [SerializeField] private TextMeshProUGUI scoreText;
    
    [SerializeField] private GameObject Popup;
    [SerializeField] private GameObject popUpPrefab;

    public void Initialize()
    {
        //空のゲームオブジェクトをCanvasの子として生成
        addGameObjectController.SetPairGameObject("PopUp", "Canvas");
        addGameObjectController.AddGameObject();
        Popup = addGameObjectController.NewGameObject;
        prefabController.AddNewPrefab(popUpPrefab);
    }
    
    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + scoreController.AccumulatedScore;
    }

    public void OnExpressBossExpressionText()
    {
        prefabController.InstantiatePrefab("BossExpression", Vector3.zero, Quaternion.identity, Popup);
        GameObject bossExpressionPopup = prefabController.clonePrefab;
        bossExpressionPopup.GetComponent<Transform>().transform.localPosition = new Vector3(1200, 200, 0);
        SEController.Instance.Play(SEPath.OnExpressMessage);
        bossExpressionPopup.GetComponent<Animator>().SetTrigger("OnBossExpression");
    }
}
