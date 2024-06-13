using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
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
}
