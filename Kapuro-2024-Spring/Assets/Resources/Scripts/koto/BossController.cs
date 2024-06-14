using System.Collections;
using System.Collections.Generic;
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
}
