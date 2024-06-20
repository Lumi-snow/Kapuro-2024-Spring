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
                    /*Normal*/
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
                    /*ShishiGawara*/
                    case RoofTile.RoofTileType.SHISHIGAWARA_WATER: //瓦が獅子瓦の水の場合
                        SendIncorrect();
                        AudioUtilizer.AudioUtilizer.PlayShishiGawaraWaterSE(); //水の音を再生
                        bossController.boss.GetComponent<AbstractBoss>().AddAwakingPoint(currentRoofTile.GetComponent<RoofTile>().AwakingPointPower); //ボスの覚醒ポイントを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.INCORRECT; //評価を不正解にする
                        break;
                    case RoofTile.RoofTileType.SHISHIGAWARA_WHISTLE: //瓦が獅子瓦の笛の場合
                        SendIncorrect();
                        AudioUtilizer.AudioUtilizer.PlayShishiGawaraWaterSE(); //水の音を再生
                        bossController.boss.GetComponent<AbstractBoss>().AddAwakingPoint(currentRoofTile.GetComponent<RoofTile>().ShishiGawaraWhistleAttackPower); //ボスの覚醒ポイントを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.INCORRECT; //評価を不正解にする
                        break;
                    case RoofTile.RoofTileType.SHISHIGAWARA_EVENT:
                        SendCorrect();
                        //ここに音声
                        currentRoofTile.GetComponent<RoofTile>().ShishiGawaraMessageEvent();
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                    /*KawaraBouzu*/
                    case RoofTile.RoofTileType.KAWARA_BOUZU_ABURA:
                        SendIncorrect();
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.INCORRECT; //評価を不正解にする
                        break;
                    case RoofTile.RoofTileType.KAWARA_BOUZU_MAME:
                        SendIncorrect();
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.INCORRECT; //評価を不正解にする
                        break;
                    case RoofTile.RoofTileType.KAWARA_BOUZU_KYOU:
                        SendCorrect();
                        bossController.boss.GetComponent<AbstractBoss>().AttackKawaraBouzu(currentRoofTile.GetComponent<RoofTile>().KawaraBouzuKyouRoofTileAttackPower); //ボスに攻撃
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                    case RoofTile.RoofTileType.KAWARA_BOUZU_EVENT:
                        SendCorrect();
                        currentRoofTile.GetComponent<RoofTile>().KawaraBouzuMessageEvent();
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                }
            }

            //瓦を捨てる
            if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
            {
                SEController.Instance.Play(SEPath.PutAndThrowRoofTile);
                switch (currentRoofTile.GetComponent<RoofTile>().roofTileType)
                {
                    /*Normal*/
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
                    /*ShishiGawara*/
                    case RoofTile.RoofTileType.SHISHIGAWARA_WATER:
                        SendIncorrect();
                        AudioUtilizer.AudioUtilizer.PlayShishiGawaraWaterSE(); //水の音を再生
                        bossController.boss.GetComponent<AbstractBoss>().AddAwakingPoint(currentRoofTile.GetComponent<RoofTile>().AwakingPointPower); //ボスの覚醒ポイントを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.INCORRECT; //評価を不正解にする
                        break;
                    case RoofTile.RoofTileType.SHISHIGAWARA_WHISTLE:
                        SendIncorrect();
                        AudioUtilizer.AudioUtilizer.PlayShishiGawaraWaterSE(); //水の音を再生
                        bossController.boss.GetComponent<AbstractBoss>().AddAwakingPoint(currentRoofTile.GetComponent<RoofTile>().ShishiGawaraWhistleAttackPower); //ボスの覚醒ポイントを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.INCORRECT; //評価を不正解にする
                        break;
                    case RoofTile.RoofTileType.SHISHIGAWARA_EVENT:
                        SendCorrect();
                        //ここに音声
                        currentRoofTile.GetComponent<RoofTile>().ShishiGawaraMessageEvent();
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                    /*KawaraBouzu*/
                    case RoofTile.RoofTileType.KAWARA_BOUZU_ABURA:
                        SendIncorrect();
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.INCORRECT; //評価を不正解にする
                        break;
                    case RoofTile.RoofTileType.KAWARA_BOUZU_MAME:
                        SendIncorrect();
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を不正解にする
                        break;
                    case RoofTile.RoofTileType.KAWARA_BOUZU_KYOU:
                        SendCorrect();
                        bossController.boss.GetComponent<AbstractBoss>().AttackBouzu(currentRoofTile.GetComponent<RoofTile>().KawaraBouzuKyouRoofTileAttackPower); //坊主に攻撃
                        scoreController.AddScore(currentRoofTile.GetComponent<RoofTile>().Score); //スコアを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                    case RoofTile.RoofTileType.KAWARA_BOUZU_EVENT:
                        SendCorrect();
                        currentRoofTile.GetComponent<RoofTile>().KawaraBouzuMessageEvent();
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                }
            }

            //特殊動作
            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                switch (currentRoofTile.GetComponent<RoofTile>().roofTileType)
                {
                    /*KawaraYokai*/
                    case RoofTile.RoofTileType.KAWARA_YOKAI_DESCENDANT:
                        SendCorrect();
                        AudioUtilizer.AudioUtilizer.PlayRandomAttackSE(); //攻撃SEを再生
                        bossController.boss.GetComponent<AbstractBoss>()
                            .AttackKawaraYokai(currentRoofTile.GetComponent<RoofTile>().AttackPower); //ボスに攻撃
                        scoreController.AddScore(currentRoofTile.GetComponent<RoofTile>().Score); //スコアを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                    /*ShishiGawara*/
                    case RoofTile.RoofTileType.SHISHIGAWARA_WATER:
                        SendCorrect();
                        AudioUtilizer.AudioUtilizer.PlayRandomShishiGawaraWhistleSE(); //攻撃SEを再生
                        bossController.boss.GetComponent<AbstractBoss>().AttackShishiGawara(currentRoofTile.GetComponent<RoofTile>().AwakingPointPower); //ボスに攻撃
                        scoreController.AddScore(currentRoofTile.GetComponent<RoofTile>().Score); //スコアを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                    case RoofTile.RoofTileType.SHISHIGAWARA_WHISTLE:
                        SendCorrect();
                        AudioUtilizer.AudioUtilizer.PlayRandomShishiGawaraWhistleSE(); //攻撃SEを再生
                        bossController.boss.GetComponent<AbstractBoss>().AttackShishiGawara(currentRoofTile.GetComponent<RoofTile>().ShishiGawaraWhistleAttackPower); //ボスに攻撃
                        scoreController.AddScore(currentRoofTile.GetComponent<RoofTile>().Score); //スコアを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                    case RoofTile.RoofTileType.SHISHIGAWARA_EVENT:
                        SendCorrect();
                        //ここに音声
                        currentRoofTile.GetComponent<RoofTile>().ShishiGawaraMessageEvent();
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                    /*KawaraBouzu*/
                    case RoofTile.RoofTileType.KAWARA_BOUZU_ABURA:
                        SendCorrect();
                        bossController.boss.GetComponent<AbstractBoss>().AttackKawaraBouzu(currentRoofTile.GetComponent<RoofTile>().KawaraBouzuAburaRoofTileAttackPower); //ボスに攻撃
                        scoreController.AddScore(currentRoofTile.GetComponent<RoofTile>().Score); //スコアを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                    case RoofTile.RoofTileType.KAWARA_BOUZU_MAME:
                        SendCorrect();
                        bossController.boss.GetComponent<AbstractBoss>().AttackKawaraBouzu(currentRoofTile.GetComponent<RoofTile>().KawaraBouzuMameRoofTileAttackPower); //ボスに攻撃
                        scoreController.AddScore(currentRoofTile.GetComponent<RoofTile>().Score); //スコアを加算
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を正解にする
                        break;
                    case RoofTile.RoofTileType.KAWARA_BOUZU_KYOU:
                        SendCorrect();
                        bossController.boss.GetComponent<AbstractBoss>().AttackKawaraBouzu(-currentRoofTile.GetComponent<RoofTile>().KawaraBouzuKyouRoofTileAttackPower); //ボスが回復
                        currentRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.CORRECT; //評価を不正解にする
                        break;
                    case RoofTile.RoofTileType.KAWARA_BOUZU_EVENT:
                        SendCorrect();
                        currentRoofTile.GetComponent<RoofTile>().KawaraBouzuMessageEvent();
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