using UnityEngine;

//瓦を表示に関するクラス
public class RoofTileDisplayer : MonoBehaviour
{
    [SerializeField] private RoofTileController roofTileController;
    [SerializeField] private RoofTileGenerator roofTileGenerator;
    
    //1番目と2番目の瓦をセット
    public void SetRoofTile()
    {
        if (roofTileController.roofTiles.Count >= 2)
        {
            roofTileController.roofTiles[0].transform.localPosition = new Vector3(0, -400, 0);
            roofTileController.roofTiles[0].transform.localScale = new Vector3(50, 50, 1);
            roofTileController.roofTiles[1].transform.localPosition = new Vector3(0, 0, 0);
            roofTileController.roofTiles[1].transform.localScale = new Vector3(50, 50, 1);
        }
        else if(roofTileController.roofTiles.Count != 0) //最後の瓦の場合
        {
            roofTileController.roofTiles[0].transform.localPosition = new Vector3(0, -400, 0);
            roofTileController.roofTiles[0].transform.localScale = new Vector3(50, 50, 1);
        }
    }
    
    //３番目以下の瓦を消す
    public void HideRoofTile()
    {
        for(int i = 2 ; i < roofTileController.roofTiles.Count ; i++)
        {
            roofTileController.roofTiles[i].gameObject.SetActive(false);
        }
    }

    //２番目の瓦を表示
    public void ActivateRoofTile()
    {
        if(roofTileController.roofTiles.Count >= 2)
            roofTileController.roofTiles[1].gameObject.SetActive(true);
    }
}
