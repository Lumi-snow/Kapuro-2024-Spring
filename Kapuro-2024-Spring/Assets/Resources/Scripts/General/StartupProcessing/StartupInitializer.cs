using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StartupInitializer : MonoBehaviour
{
    [SerializeField] private GameObject prefabController;
    public static bool IsInitialized { get; private set; } = false; //初期化が終わったかどうか
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize() {
        new GameObject("StartupInitializer", typeof(StartupInitializer));
    }
    
    public async UniTask InitializeStartupProcess() {
        //ここに初期化処理を書く
        var tasks = new List<UniTask> //非同期処理で行うタスクをリストに格納
        {
            InitialPrefabController(), // 非同期処理でPrefabのリストを削除
            GenerateGameDataController() // GameDataControllerを生成
        };

        await UniTask.WhenAll(tasks); //すべてのタスクが終わるまでここで待機
        Destroy(gameObject); //初期化が済んだら自分を消す
        IsInitialized = true;
    }

    //PrefabListのリストを初期化
    private async UniTask InitialPrefabController()
    {
        prefabController = GameObject.Find("PrefabController");
        await prefabController.GetComponent<PrefabController>().RemoveAllPrefabListAsync(); //プレハブリストを初期化
        Debug.Log("Notice: PrefabController is initialized.");
    }
    
    private async UniTask GenerateGameDataController()
    {
        if (GameDataController.isExist() == false) // GameDataControllerがまだ生成されていない場合
        {
            new GameObject("GameDataController", typeof(GameDataController));
            Debug.Log("Notice: GameDataController is generated.");
        }
        else
        {
            Debug.LogWarning("Notice: GameDataController already exists.");
        }
        
        await UniTask.DelayFrame(1);
    }
}
