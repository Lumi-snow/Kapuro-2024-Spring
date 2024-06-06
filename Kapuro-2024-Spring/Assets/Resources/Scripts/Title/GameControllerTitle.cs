using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerTitle : AbstractGameController
{
    [SerializeField] private GameEndProcessing gameEndProcessing;

    void Awake()
    {
        
    }
    
    void Start()
    {
        gameEndProcessing.Initialize();
    }

    void Update()
    {
        gameEndProcessing.GameEndHandller();
        gameEndProcessing.GameEndPopUpNullHandller();
    }
}
