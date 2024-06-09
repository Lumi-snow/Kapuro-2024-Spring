using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerStartupProcessing : AbstractGameController
{
    [SerializeField] private StartupInitializer startupInitializer;
    private void Awake()
    {
        
    }
    
    private void Start()
    {
        
    }
    
    private void Update()
    {
        if (StartupInitializer.IsInitialized == true)
        {
            SceneManager.LoadScene("Title"); //初期化がすんだらタイトルシーンに移動
        }
    }
}
