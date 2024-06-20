using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using AudioUtilizer;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RoofTileDestroyer : MonoBehaviour
{
    [SerializeField] private RoofTileController roofTileController;
    
    //瓦を破棄する
    public async UniTask<bool> DestroyEvaluatedRoofTile(GameObject roofTile)
    {
        if (roofTile != null)
        {
            switch (roofTile.GetComponent<RoofTile>().evaluateType)
            {
                case RoofTile.EvaluateType.CORRECT: //瓦が正解の場合
                    await roofTile.GetComponent<RoofTile>().OnDestroyProcess().AttachExternalCancellation(cancellationToken: this.GetCancellationTokenOnDestroy()); //非同期で瓦破棄時の処理を実行
                    roofTileController.roofTiles.Remove(roofTile);
                    Destroy(roofTile);
                    return true; //次の瓦をセットするためのフラグ
                case RoofTile.EvaluateType.INCORRECT: //瓦が不正解の場合
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
                /*KawaraYokai*/
                case RoofTile.RoofTileType.KAWARA_YOKAI_DESCENDANT:
                    roofTileController.roofTiles.Remove(roofTile);
                    Destroy(roofTile);
                    break;
                /*ShishiGawara*/
                case RoofTile.RoofTileType.SHISHIGAWARA_WATER:
                    roofTileController.roofTiles.Remove(roofTile);
                    Destroy(roofTile);
                    break;
                case RoofTile.RoofTileType.SHISHIGAWARA_WHISTLE:
                    roofTileController.roofTiles.Remove(roofTile);
                    Destroy(roofTile);
                    break;
                case RoofTile.RoofTileType.SHISHIGAWARA_EVENT:
                    roofTileController.roofTiles.Remove(roofTile);
                    Destroy(roofTile);
                    break;
                /*kawaraBouzu*/
                case RoofTile.RoofTileType.KAWARA_BOUZU_ABURA:
                    roofTileController.roofTiles.Remove(roofTile);
                    Destroy(roofTile);
                    break;
                case RoofTile.RoofTileType.KAWARA_BOUZU_MAME:
                    roofTileController.roofTiles.Remove(roofTile);
                    Destroy(roofTile);
                    break;
                case RoofTile.RoofTileType.KAWARA_BOUZU_KYOU:
                    roofTileController.roofTiles.Remove(roofTile);
                    Destroy(roofTile);
                    break;
                case RoofTile.RoofTileType.KAWARA_BOUZU_EVENT:
                    roofTileController.roofTiles.Remove(roofTile);
                    Destroy(roofTile);
                    break;
            }
        }
    }
    
    //特定のインデックスの瓦を破棄する
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
            else if(index1.GetComponent<RoofTile>().roofTileType == RoofTile.RoofTileType.SHISHIGAWARA_WATER || index1.GetComponent<RoofTile>().roofTileType == RoofTile.RoofTileType.SHISHIGAWARA_WHISTLE || index1.GetComponent<RoofTile>().roofTileType == RoofTile.RoofTileType.SHISHIGAWARA_EVENT)
            {
                roofTileController.roofTiles.Remove(index1);
                Destroy(index1);
                roofTileController.roofTiles.Remove(index2);
                Destroy(index2);
            }
            else if(index1.GetComponent<RoofTile>().roofTileType == RoofTile.RoofTileType.KAWARA_BOUZU_ABURA || index1.GetComponent<RoofTile>().roofTileType == RoofTile.RoofTileType.KAWARA_BOUZU_MAME || index1.GetComponent<RoofTile>().roofTileType == RoofTile.RoofTileType.KAWARA_BOUZU_KYOU || index1.GetComponent<RoofTile>().roofTileType == RoofTile.RoofTileType.KAWARA_BOUZU_EVENT)
            {
                roofTileController.roofTiles.Remove(index1);
                Destroy(index1);
                roofTileController.roofTiles.Remove(index2);
                Destroy(index2);
            }
        }
    }
}
