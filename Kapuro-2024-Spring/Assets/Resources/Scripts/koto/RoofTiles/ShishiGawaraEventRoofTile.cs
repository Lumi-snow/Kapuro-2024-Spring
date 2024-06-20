using System;
using System.Collections;
using System.Collections.Generic;
using AudioController;
using Cysharp.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShishiGawaraEventRoofTile : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.SHISHIGAWARA_EVENT; // roofTileType プロパティをオーバーライド
    public override EvaluateType evaluateType { get; set; } // evaluateType プロパティをオーバーライド

    /*共通のメンバ変数*/
    [SerializeField] private int scoreCorrectRoofTile = 100; //スコア
    
    /*共通のプロパティ*/
    public override int Score //スコアのプロパティ
    {
        get => scoreCorrectRoofTile;
        set => scoreCorrectRoofTile = value;
    }
    
    /*共通のメンバ関数*/
    public override async UniTask OnDestroyProcess()
    {
        if (boss != null)
        {
            boss.transform.localPosition = new Vector3(0, 1000, 0);
            gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(0, 0, 0);
            try
            {
                await UniTask.WaitUntil(() => IsFinishEventShishiGawara == true, cancellationToken: this.GetCancellationTokenOnDestroy());
            }
            catch (OperationCanceledException exception)
            {
                // キャンセルされた場合の処理
                Debug.Log("OnDestroyProcess was canceled.");
                return;
            }
            boss.transform.localPosition = new Vector3(0, 300, 0);
        }
    }
    
    /*固有のメンバ変数*/
    [SerializeField] private GameObject boss; //ボス
    [SerializeField] private GameObject panel; //パネル
    [SerializeField] private TextMeshProUGUI messageText; //メッセージテキスト
    [SerializeField] private Button trueButton; //決定ボタン
    [SerializeField] private Button falseButton; //キャンセルボタン
    [SerializeField] private bool isFinishEvent = false; //イベントが終了したかどうか
    [SerializeField] private bool isCorrectAnswer = false;
    
    /*固有のプロパティ*/
    public override bool IsFinishEventShishiGawara //イベントが終了したかどうかのプロパティ
    {
        get => isFinishEvent;
    }
    
    /*固有のメンバ関数*/
    public override void InitializeShishiGawaraMessageEvent()
    {
        Transform panelTransform = gameObject.transform.Find("EventPanel");
        panel = GameObject.Find("EventPanel");
        Transform trueButtonTransform = panelTransform.Find("TrueButton");
        trueButton = trueButtonTransform.GetComponent<Button>();
        Transform falseButtonTransform = panelTransform.Find("FalseButton");
        falseButton = falseButtonTransform.GetComponent<Button>();
        Transform messageTextTransform = panelTransform.Find("EventMessage");
        messageText = messageTextTransform.GetComponent<TextMeshProUGUI>();
        messageText.text = "獅子瓦は水の音を聞くと、\n箱から飛び出してしまう。\n笛を吹く？";
        panel.gameObject.SetActive(false);
        boss = GameObject.Find("ShishiGawara(Clone)");
    }
    
    public override void ShishiGawaraMessageEvent()
    {
        panel.gameObject.SetActive(true);
        trueButton.onClick.AddListener(() => OnClickTrueButton().Forget());
        falseButton.onClick.AddListener(() => OnClickFalseButton().Forget());
    }
    
    private async UniTask OnClickTrueButton()
    {
        messageText.text = "笛を吹いた。\n";
        await UniTask.Delay(1000, cancellationToken: this.GetCancellationTokenOnDestroy());
        messageText.text =　"箱の揺れが収まる。\n";
        await UniTask.Delay(1000, cancellationToken: this.GetCancellationTokenOnDestroy());
        messageText.text =　"獅子瓦のHpと覚醒度が減少。\n";
        boss.GetComponent<AbstractBoss>().AttackShishiGawara(200);
        boss.GetComponent<AbstractBoss>().AddAwakingPoint(100);
        await UniTask.Delay(3000, cancellationToken: this.GetCancellationTokenOnDestroy());
        isFinishEvent = true;
        isCorrectAnswer = true;
    }

    private async UniTask OnClickFalseButton()
    {
        messageText.text = "笛を吹かなかった。\n";
        await UniTask.Delay(1000, cancellationToken: this.GetCancellationTokenOnDestroy());
        messageText.text = "箱の中で獅子が蠢く。\n";
        await UniTask.Delay(1000, cancellationToken: this.GetCancellationTokenOnDestroy());
        messageText.text = "獅子瓦の覚醒度が増加\n";
        boss.GetComponent<AbstractBoss>().AddAwakingPoint(-100);
        await UniTask.Delay(3000, cancellationToken: this.GetCancellationTokenOnDestroy());
        isFinishEvent = true;
        isCorrectAnswer = false;
    }
}
