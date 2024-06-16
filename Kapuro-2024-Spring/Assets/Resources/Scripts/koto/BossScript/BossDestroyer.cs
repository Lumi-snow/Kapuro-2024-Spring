using System.Collections;
using System.Collections.Generic;
using AudioController;
using UnityEngine;

public class BossDestroyer : MonoBehaviour
{
    [SerializeField] private BossController bossController;
    [SerializeField] private UIControllerKoto uiControllerKoto;

    public void DestroyBoss(GameObject boss)
    {
        if (boss != null)
        {
            switch (boss.GetComponent<AbstractBoss>().bossType)
            {
                case AbstractBoss.BossType.KAWARA_YOKAI:
                    if (boss.GetComponent<AbstractBoss>().Hp < 0)
                    {
                        //ここに破棄時の処理を書く
                        SEController.Instance.Play(SEPath.DestroyBoss);
                        uiControllerKoto.DeflaggerForBossDestroy();
                        Destroy(bossController.boss);
                        bossController.IsBossDead = true;
                    }
                
                    break;
                case AbstractBoss.BossType.SHISHIGAWARA:
                    if (boss.GetComponent<AbstractBoss>().Hp < 0)
                    {
                        //ここに破棄時の処理を書く
                        SEController.Instance.Play(SEPath.DestroyBoss);
                        uiControllerKoto.DeflaggerForBossDestroy();
                        Destroy(bossController.boss);
                        bossController.IsBossDead = true;
                    }
                    else if (boss.GetComponent<AbstractBoss>().AwakingPoint >= boss.GetComponent<AbstractBoss>().MaxAwakingPoint) //覚醒ポイントが最大値に達した場合
                    {
                        //ここに破棄時の処理を書く
                        SEController.Instance.Play(SEPath.ShishiGawaraFailure);
                        uiControllerKoto.DeflaggerForBossDestroy();
                        Destroy(bossController.boss);
                        bossController.IsBossDead = true;
                    }

                    break;
            }
        }
    }
}
