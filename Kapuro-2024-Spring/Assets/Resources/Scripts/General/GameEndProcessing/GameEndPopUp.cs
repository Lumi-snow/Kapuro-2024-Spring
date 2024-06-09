using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//ゲーム終了時のポップアップのPrefabのデータ
[Serializable]
public class GameEndPopUp : PrefabData
{
    [SerializeField] private List<Button> buttons = new List<Button>(); //ボタンのリスト
    [SerializeField] private List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>(); //テキストのリスト

    [SerializeField] private Button buttonYes; //Yesボタン
    [SerializeField] private Button buttonNo; //Noボタン
    [SerializeField] private TextMeshProUGUI confirmMessage; //確認メッセージ

    [SerializeField] bool isDestroy = false; //削除するかどうか
    public bool IsDestroy
    {
        get => isDestroy;
        set => isDestroy = value;
    }

    void Start()
    {
        buttons.Add(buttonYes); //Yesボタンをリストに追加
        buttons.Add(buttonNo); //Noボタンをリストに追加
        
        texts.Add(confirmMessage); //確認メッセージをリストに追加
        
        buttonYes.onClick.AddListener(YesButtonProcess); //Yesボタンが押された時の処理
        buttonNo.onClick.AddListener(NoButtnoProcess); //Noボタンが押された時の処理
    }
    
    //Yesボタンが押された時の処理
    private void YesButtonProcess()
    {
        #if UNITY_EDITOR //UnityEditorの時
            UnityEditor.EditorApplication.isPlaying = false; //UnityEditorの再生を停止
        #else //UnityEditor以外の時
            Application.Quit(); //アプリケーションを終了
        #endif
    }
    
    //Noボタンが押された時の処理
    public void NoButtnoProcess()
    {
        StartCoroutine(DestroyProcess());
    }
    
    private IEnumerator DestroyProcess()
    {
        Animator onDestroyAnimator = this.GetComponent<Animator>();
        onDestroyAnimator.SetTrigger("Destroy");
        while (!onDestroyAnimator.GetCurrentAnimatorStateInfo(0).IsName("OnDestroy") ||
               onDestroyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
