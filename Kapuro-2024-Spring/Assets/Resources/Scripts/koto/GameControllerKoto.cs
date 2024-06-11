using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerKoto : AbstractGameController
{
    [SerializeField] private RoofTileController roofTileController;
    private void Awake()
    {
        
    }

    private void Start()
    {
        roofTileController.Initialize(); //瓦関係の処理の初期化
    }

    private void Update()
    {
        
    }
}