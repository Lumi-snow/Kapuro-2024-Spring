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
        if (bossController.boss != null)
        {
            bossController.boss.transform.localPosition = new Vector3(0, 300, 0);
            bossController.boss.transform.localScale = new Vector3(50, 50, 0);
            bossController.boss.GetComponent<AbstractBoss>().HpSlider = bossController.boss.GetComponent<AbstractBoss>().Hp;
        }
    }
}
