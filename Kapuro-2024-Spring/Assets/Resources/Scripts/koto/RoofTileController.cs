using System.Collections.Generic;
using UnityEngine;

//RoofTileに関連する処理を行うクラス
public class RoofTileController : MonoBehaviour
{
    public List<GameObject> roofTiles; //生成した瓦のリスト
    public GameObject boss;
    public int allRoofTileNum; //生成する瓦の総数
    
    [SerializeField] private RoofTileGenerator roofTileGenerator;
    [SerializeField] private RoofTileDisplayer roofTileDisplayer;
    [SerializeField] private RoofTileEvaluater roofTileEvaluater;
    [SerializeField] private RoofTileDestroyer roofTileDestroyer;
    [SerializeField] private RoofTileEventHandler roofTileEventHandler;
    [SerializeField] private ScoreController scoreController;
    
    //初期化処理
    public void Initialize()
    {
        roofTileGenerator.Initialize();
        roofTileDisplayer.HideRoofTile();
        roofTileDisplayer.SetRoofTile();
    }

    //瓦を生成する
    public void Evaluate()
    {
        roofTileEvaluater.EvaluateRoofTile(GetCurrentRoofTile());
    }

    //瓦を破棄する
    public void Destroy()
    {
        bool isSetNext = roofTileDestroyer.DestroyEvaluatedRoofTile(GetCurrentRoofTile());

        if (isSetNext == true)
        {
            SetNewRoofTile();
        }
    }

    //新しく瓦をセットする
    private void SetNewRoofTile()
    {
        roofTileDisplayer.SetRoofTile();
        roofTileDisplayer.ActivateRoofTile();
        roofTileDisplayer.HideRoofTile();
    }

    //ゲーム終了時の処理
    public void GameEndProcess()
    {
        //TODO: ゲーム終了時の処理を追加
        if (roofTiles.Count == 0)
        {
            SceneController.ChangeSceneToTitle();
        }
    }
    
    //現在の瓦を取得する
    private GameObject GetCurrentRoofTile()
    {
        if (roofTiles.Count != 0)
        {
            return roofTiles[0];
        }
        else //最後の瓦の場合
        {
            return null;
        }
    }
}
