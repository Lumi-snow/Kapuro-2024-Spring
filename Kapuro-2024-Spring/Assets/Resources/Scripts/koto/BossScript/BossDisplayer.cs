using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossDisplayer : MonoBehaviour
{
    [SerializeField] private BossController bossController;

    public void ActivateBoss()
    {
        if (bossController.boss != null)
        {
            bossController.boss.SetActive(true);
        }
    }
    
    public void HideBoss()
    {
        if (bossController.boss != null)
        {
            bossController.boss.SetActive(false);
        }
    }

    public void SetBoss()
    {
        if (bossController.boss != null && bossController.boss.GetComponent<AbstractBoss>().IsLocationSet == false)
        {
            bossController.boss.GetComponent<AbstractBoss>().SetMyself();
            bossController.boss.GetComponent<AbstractBoss>().IsLocationSet = true;
        }
    }
}
