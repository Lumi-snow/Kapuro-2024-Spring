using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using AudioUtilizer;
using UnityEngine;

public class RoofTileDestroyer : MonoBehaviour
{
    [SerializeField] private RoofTileController roofTileController;
    
    //瓦を破棄する
    public bool DestroyEvaluatedRoofTile(GameObject roofTile)
    {
        if (roofTile != null)
        {
            switch (roofTile.GetComponent<RoofTile>().evaluateType)
            {
                case RoofTile.EvaluateType.CORRECT: //瓦が正解の場合
                    //ここに破棄時の処理を追加
                    roofTileController.roofTiles.Remove(roofTile);
                    Destroy(roofTile);
                    return true; //次の瓦をセットするためのフラグ
                case RoofTile.EvaluateType.INCORRECT: //瓦が不正解の場合
                    //ここに破棄時の処理を追加
                    roofTileController.roofTiles.Remove(roofTile);
                    Destroy(roofTile);
                    return true; //次の瓦をセットするためのフラグ
                case RoofTile.EvaluateType.NOT_EVALUATED: //瓦が評価されていない場合
                    return false;
                default:
                    return false;
            }
        }
        
        return false;
    }
    
    //ボス撃破時関連の瓦を破棄する
    public void DestroySpecialRoofTileForBoss(GameObject roofTile)
    {
        if(roofTile != null)
        {
            switch(roofTile.GetComponent<RoofTile>().roofTileType)
            {
                case RoofTile.RoofTileType.KAWARA_YOKAI_DESCENDANT:
                    roofTileController.roofTiles.Remove(roofTile);
                    Destroy(roofTile);
                    break;
            }
        }
    }
    
    public void DestroySpecialRoofTileForCurrentIndex1and2(GameObject index1, GameObject index2)
    {
        if(index1 != null && index2 != null)
        {
            if(index1.GetComponent<RoofTile>().roofTileType == RoofTile.RoofTileType.KAWARA_YOKAI_DESCENDANT)
            {
                roofTileController.roofTiles.Remove(index1);
                Destroy(index1);
                roofTileController.roofTiles.Remove(index2);
                Destroy(index2);
            }
        }
    }
}
