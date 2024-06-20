using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameEndProcessing : MonoBehaviour
{
    [SerializeField] private AddGameObjectController addGameObjectController;
    [SerializeField] private PrefabController prefabController;
    [SerializeField] private GameObject gameEndPopUpPrefab;

    //ゲーム終了時のポップアップのクローン
    private GameObject cloneGameEndPopUpPrefab = null;

    //ゲーム終了時のポップアップがアクティブかどうか false-> アクティブでない true-> アクティブ
    private bool isGameEndPopUpActive;
    public bool IsGameEndPopUpActive
    {
        get => isGameEndPopUpActive;
        set => isGameEndPopUpActive = value;
    }

    //初期化処理
    public void Initialize()
    {
        //ゲーム終了時のポップアップをScriptableObjectに追加
        prefabController.AddNewPrefab(gameEndPopUpPrefab);
    }

    //Escキーが押された時の処理
    public void GameEndHandller()
    {
        if (Input.GetKey(KeyCode.Escape) == true && !IsGameEndPopUpActive)
        {
            addGameObjectController.SetPairGameObject("GameEndPopUp", "Canvas");
            addGameObjectController.AddGameObject();
            prefabController.InstantiatePrefab(gameEndPopUpPrefab.name, Vector3.zero, Quaternion.identity, addGameObjectController.NewGameObject);
            cloneGameEndPopUpPrefab = prefabController.clonePrefab;
            IsGameEndPopUpActive = true;
        }
    }
    
    //ゲーム終了時のポップアップが出ていないの時の処理
    public void GameEndPopUpNullHandller()
    {
        if (cloneGameEndPopUpPrefab == null)
        {
            IsGameEndPopUpActive = false;
            Destroy(addGameObjectController.NewGameObject);
        }
    }
}