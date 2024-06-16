using System.Collections;
using AudioController;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerater : MonoBehaviour
{
    [SerializeField] private List<GameObject> bossList;
    
    [SerializeField] private BossController bossController;
    [SerializeField] private UIControllerKoto uiControllerKoto;
    [SerializeField] private PrefabController prefabController;
    [SerializeField] private AddGameObjectController addGameObjectController;
    
    //初期化
    public void Initialize()
    {
        //PrefabControllerに瓦のPrefabを登録
        foreach(GameObject boss in bossList)
        {
            prefabController.AddNewPrefab(boss);
        }
        
        //空のゲームオブジェクトをCanvasの子として生成
        addGameObjectController.SetPairGameObject("Boss", "Canvas");
        addGameObjectController.AddGameObject();
    }

    public void GenerateBoss(ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE eventType)
    {
        switch (eventType)
        {
            case ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.BOSS_YOKAI01:
                SEController.Instance.Play(SEPath.BossExpression01);
                prefabController.InstantiatePrefab("KawaraYokai", Vector3.zero, Quaternion.identity, addGameObjectController.NewGameObject);
                bossController.boss = prefabController.clonePrefab;
                uiControllerKoto.OnExpressBossExpressionText();
                uiControllerKoto.PressSpaceText();
                break;
            case ConstantNumberKoto.ConstantNumberKoto.EVENT_TYPE.BOSS_YOKAI02: 
                prefabController.InstantiatePrefab("Boss02", Vector3.zero, Quaternion.identity, addGameObjectController.NewGameObject);
                bossController.boss = prefabController.clonePrefab;
                break;
        }
    }
}
