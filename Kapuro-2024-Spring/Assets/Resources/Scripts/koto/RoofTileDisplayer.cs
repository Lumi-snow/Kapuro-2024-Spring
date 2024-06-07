using UnityEngine;

//瓦を表示に関するクラス
public class RoofTileDisplayer : MonoBehaviour
{
    [SerializeField] private RoofTileController roofTileController;
    [SerializeField] private RoofTileGenerator roofTileGenerator;
    
    //1番目と2番目の瓦をセット
    public void SetRoofTile()
    {
        roofTileController.roofTiles[0].transform.localPosition = new Vector3(-550, -400, 0);
        roofTileController.roofTiles[0].transform.localScale = new Vector3(50, 50, 0);
        roofTileController.roofTiles[1].transform.localPosition = new Vector3(0, -400, 0);
        roofTileController.roofTiles[1].transform.localScale = new Vector3(50, 50, 0);
    }
    
    //３番目以下の瓦を消す
    public void HideRoofTile()
    {
        for(int i = 2 ; i < roofTileController.roofTiles.Count ; i++)
        {
            roofTileController.roofTiles[i].gameObject.SetActive(false);
        }
    }
}
