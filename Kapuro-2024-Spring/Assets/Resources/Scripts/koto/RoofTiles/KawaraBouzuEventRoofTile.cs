using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KawaraBouzuEventRoofTile : RoofTile
{
    public override RoofTileType roofTileType => RoofTileType.KAWARA_BOUZU_EVENT; // roofTileType プロパティをオーバーライド
    public override EvaluateType evaluateType { get; set; } // evaluateType プロパティをオーバーライド

    /*共通のメンバ変数*/
    [SerializeField] private int scoreCorrectRoofTile = 1000; //スコア
    
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
                await UniTask.WaitUntil(() => IsFinishEventKawaraBouzu == true, cancellationToken: this.GetCancellationTokenOnDestroy());
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
    [SerializeField] private bool isNextMessage = false;
    
    /*固有のプロパティ*/
    public override bool IsFinishEventKawaraBouzu { get => isFinishEvent; } //イベントが終了したかどうかのプロパティ
    public override bool IsNextMessageKawaraBouzu { get => isNextMessage; set => isNextMessage = value; } //次のメッセージに進むかどうかのプロパティ
    
    /*固有のメンバ関数*/
    public override void InitializeKawaraBouzuEvent()
    {
        Transform panelTransform = gameObject.transform.Find("EventPanel");
        panel = GameObject.Find("EventPanel");
        Transform trueButtonTransform = panelTransform.Find("TrueButton");
        trueButton = trueButtonTransform.GetComponent<Button>();
        Transform falseButtonTransform = panelTransform.Find("FalseButton");
        falseButton = falseButtonTransform.GetComponent<Button>();
        Transform messageTextTransform = panelTransform.Find("EventMessage");
        messageText = messageTextTransform.GetComponent<TextMeshProUGUI>();
        messageText.text = "坊主が二人経を唱えている";
        panel.gameObject.SetActive(false);
        boss = GameObject.Find("KawaraBouzu(Clone)");
    }
    
    public override async UniTask KawaraBouzuMessageEvent()
    {
        panel.gameObject.SetActive(true);
        messageText.text = "お経に混じって\nカラスの鳴き声が聞こえる\n(スペースキー押下で次のメッセージに進む)";
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.DownArrow), cancellationToken: this.GetCancellationTokenOnDestroy());
        messageText.text = "二人坊主という奴だろう\n油と豆をやると笑うという\n(スペースキー押下で次のメッセージに進む)";
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.DownArrow), cancellationToken: this.GetCancellationTokenOnDestroy());
        messageText.text = "カラスの鳴き声がする坊主に\n油と豆をやる？\n(ボタンを選択してください)";
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.DownArrow), cancellationToken: this.GetCancellationTokenOnDestroy());
        trueButton.onClick.AddListener(() => OnClickTrueButton().Forget());
        falseButton.onClick.AddListener(() => OnClickFalseButton().Forget());
    }

    private async UniTask OnClickTrueButton()
    {
        messageText.text = "油と豆をやった\n";
        await UniTask.Delay(1000, cancellationToken: this.GetCancellationTokenOnDestroy());
        messageText.text =　"坊主が薄気味悪く、\nにたーっと笑う";
        await UniTask.Delay(1000, cancellationToken: this.GetCancellationTokenOnDestroy());
        messageText.text =　"瓦坊主のHpが減少\n";
        boss.GetComponent<AbstractBoss>().AttackKawaraBouzu(200);
        await UniTask.Delay(3000, cancellationToken: this.GetCancellationTokenOnDestroy());
        isFinishEvent = true;
        isCorrectAnswer = true;
    }
    
    private async UniTask OnClickFalseButton()
    {
        messageText.text = "試しにお経をやってみた\n";
        await UniTask.Delay(1000, cancellationToken: this.GetCancellationTokenOnDestroy());
        messageText.text = "もう一人の坊主が怪訝な顔でこちらを見る\n";
        await UniTask.Delay(1000, cancellationToken: this.GetCancellationTokenOnDestroy());
        messageText.text = "坊主のHpが減少\n";
        boss.GetComponent<AbstractBoss>().AttackBouzu(200);
        await UniTask.Delay(3000, cancellationToken: this.GetCancellationTokenOnDestroy());
        isFinishEvent = true;
        isCorrectAnswer = false;
    }
}
