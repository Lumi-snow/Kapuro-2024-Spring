using AudioController;
using UnityEngine;

//RoofTileが正しく仕訳けられたか評価するクラス
public class RoofTileEvaluater : MonoBehaviour
{
    [SerializeField] private ScoreController scoreController; //スコアを管理するクラス
    [SerializeField] private RoofTileController roofTileController; //瓦を管理するクラス
    [SerializeField] private RoofTileEventHandler roofTileEventHandler; //瓦のイベントを管理するクラス
    [SerializeField] private BossController bossController; //ボスを管理するクラス

    //瓦の評価を行う
    public void EvaluateRoofTile(GameObject currentRoofTile)
    {
        if (currentRoofTile != null)
        {
            //瓦を置く
            if(Input.GetKeyDown(KeyCode.RightArrow) == true)
            {
                SEController.Instance.Play(SEPath.PutAndThrowRoofTile);
                switch (currentRoofTile.GetComponent<RoofTile>().roofTileType)
                {
                    case RoofTile.RoofTileType.NORMAL: //瓦が普通の場合
                        SendCorrect();
                        scoreController.AddScore(currentRoofTile.GetComponent<RoofTile>().Score); //スコアを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.INCORRECT; //評価を正解にする
                        break;
                    case RoofTile.RoofTileType.EXPENSIVE: //瓦が高価な場合
                        SendCorrect();
                        scoreController.AddScore(currentRoofTile.GetComponent<RoofTile>().Score); //スコアを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                    case RoofTile.RoofTileType.LEGEND: //瓦が伝説の場合
                        SendCorrect();
                        scoreController.AddScore(currentRoofTile.GetComponent<RoofTile>().Score); //スコアを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                    case RoofTile.RoofTileType.BROKEN: //瓦が壊れている場合
                        SendIncorrect();
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を不正解にする
                        break;
                    case RoofTile.RoofTileType.EVENT: //瓦がイベントの場合
                        SendCorrect();
                        scoreController.AddScore(currentRoofTile.GetComponent<RoofTile>().Score); //スコアを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        roofTileEventHandler.SetEvent(); //イベントを発生させる
                        roofTileEventHandler.EventHandler(); //イベントの生成
                        break;
                    default:
                        break;
                }
            }

            //瓦を捨てる
            if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
            {
                SEController.Instance.Play(SEPath.PutAndThrowRoofTile);
                switch (currentRoofTile.GetComponent<RoofTile>().roofTileType)
                {
                    case RoofTile.RoofTileType.NORMAL: //瓦が普通の場合
                        SendIncorrect();
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.INCORRECT; //評価を不正解にする
                        break;
                    case RoofTile.RoofTileType.EXPENSIVE: //瓦が高価な場合
                        SendIncorrect();
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を不正解にする
                        break;
                    case RoofTile.RoofTileType.LEGEND: //瓦が伝説の場合
                        SendIncorrect();
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を不正解にする
                        break;
                    case RoofTile.RoofTileType.BROKEN: //瓦が壊れている場合
                        SendCorrect();
                        scoreController.AddScore(currentRoofTile.GetComponent<RoofTile>().Score); //スコアを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                    case RoofTile.RoofTileType.EVENT:
                        SendIncorrect();
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.INCORRECT; //評価を不正解にする
                        roofTileEventHandler.SetEvent(); //イベントを発生させる
                        roofTileEventHandler.EventHandler(); //イベントの生成
                        break;
                    default:
                        break;
                }
            }

            //特殊動作
            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                AudioUtilizer.AudioUtilizer.PlayRandomAttackSE();
                switch (currentRoofTile.GetComponent<RoofTile>().roofTileType)
                {
                    case RoofTile.RoofTileType.KAWARA_YOKAI_DESCENDANT:
                        SendCorrect();
                        Debug.Log(currentRoofTile.GetComponent<RoofTile>().AttackPower);
                        bossController.Attack(currentRoofTile.GetComponent<RoofTile>().AttackPower); //ボスに攻撃
                        scoreController.AddScore(currentRoofTile.GetComponent<RoofTile>().Score); //スコアを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                }
            }
        }
    }
    
    //DEBUG
    //正解時の処理
    private void SendCorrect()
    {
        Debug.Log("正解");
    }
    
    //DEBUG
    //不正解時の処理
    private void SendIncorrect()
    {
        Debug.Log("不正解");
    }
}