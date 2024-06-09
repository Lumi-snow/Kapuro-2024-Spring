using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerStartupProcessing : AbstractGameController
{
    [SerializeField] private UIControllerStartupProcessing uiControllerStartupProcessing;
    [SerializeField] private PrefabController prefabController;
    
    //非同期で初期化処理を行う
    private async UniTask Awake()
    {
        uiControllerStartupProcessing.OnStartLoadingUI(); //ローディングUIを表示
        GameObject startupInitializer = GameObject.Find("StartupInitializer"); //StartupInitializerを探す
        
        await startupInitializer.GetComponent<StartupInitializer>().InitializeStartupProcess(); //StartupInitializerの初期化処理
        
        uiControllerStartupProcessing.OnCompleteLoadingUI(); //ローディングUIを非表示
    }
    
    private async void Start()
    {
        await UniTask.Delay(5000);
    }
    
    private void Update()
    {
        if (StartupInitializer.IsInitialized == true)
        {
            SceneManager.LoadScene("Title"); //初期化がすんだらタイトルシーンに移動
        }
    }
}
