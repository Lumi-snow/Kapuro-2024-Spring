using System.Collections;
using System.Collections.Generic;
using AudioController;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject boss;
    private bool isBossDead = false;
    public bool IsBossDead
    {
        get => isBossDead;
        set => isBossDead = value;
    }
    
    private bool isBossDaedBGM = false;
    public bool IsBossDeadBGM
    {
        get => isBossDaedBGM;
        set => isBossDaedBGM = value;
    }

    [SerializeField] private BossGenerater bossGenerater;
    [SerializeField] private BossDisplayer bossDisplayer;
    [SerializeField] private BossDestroyer bossDestroyer;
    
    public void Initialize()
    {
        bossGenerater.Initialize();
    }

    public void SetNewBoss()
    {
        if (boss != null)
        {
            bossDisplayer.SetBoss();
            bossDisplayer.ActivateBoss();
        }
    }

    public void Attack(int attackPower)
    {
        boss.GetComponent<AbstractBoss>().Hp -= attackPower;
        boss.GetComponent<AbstractBoss>().HpSlider -= attackPower;
    }
    
    public void DestroyBoss()
    {
        if(boss != null)
            bossDestroyer.DestroyBoss();
    }

    public void SetBossBGM()
    {
        if (boss != null &&
            boss.GetComponent<AbstractBoss>().Hp < boss.GetComponent<AbstractBoss>().HpMax / 2 &&
            boss.GetComponent<AbstractBoss>().IsBossHpHalf == false) //Hpが半分未満なら 
        {
            BGMSwitcher.CrossFade(BGMPath.BossBGM01, 3);
            boss.GetComponent<AbstractBoss>().IsBossHpHalf = true;
        }
        
        if (IsBossDead == true && IsBossDeadBGM == false) //ボスが破棄されているなら
        {
            BGMSwitcher.CrossFade(BGMPath.NormalBGM01, 3);
            IsBossDeadBGM = true;
        }
    }
}
