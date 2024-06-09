using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupInitializer : MonoBehaviour
{
    public static bool IsInitialized { get; private set; } = false; 
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize() {
        new GameObject("StartupInitializer", typeof(StartupInitializer));
    }
    
    private void Awake() {
        //ここに初期化処理を書く
        
        Destroy(gameObject); //初期化が済んだら自分を消す
        IsInitialized = true;
    }
}
