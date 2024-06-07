using System.Collections.Generic;
using UnityEngine;

//RoofTileに関連する処理を行うクラス
public class RoofTileController : MonoBehaviour
{
    public List<GameObject> roofTiles; //生成した瓦のリスト
    public int allRoofTileNum; //生成する瓦の総数
    
    [SerializeField] private RoofTileGenerator roofTileGenerator;
    [SerializeField] private RoofTileDisplayer roofTileDisplayer;
    
    public void Initialize()
    {
        roofTileGenerator.Initialize();
        roofTileDisplayer.HideRoofTile();
        roofTileDisplayer.SetRoofTile();
    }
}
