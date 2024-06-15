using System.Collections;
using System.Collections.Generic;
using AudioController;
using UnityEngine;

public class BossDestroyer : MonoBehaviour
{
    [SerializeField] private BossController bossController;

    public void DestroyBoss()
    {
        if (bossController.boss.GetComponent<AbstractBoss>().Hp < 0)
        {
            //ここに破棄時の処理を書く
            SEController.Instance.Play(SEPath.DestroyBoss);
            Destroy(bossController.boss);
            bossController.IsBossDead = true;
        }
    }
}
