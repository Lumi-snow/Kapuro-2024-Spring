using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AbstractBoss : MonoBehaviour
{
    private Slider hpSlider;
    public abstract float HpSlider { get; set; }
    
    public enum BossType
    {
        KAWARA_YOKAI,
        BOSS_YOKAI02,
    };
    public abstract BossType bossType { get; }

    private int hp;
    public abstract int Hp { get; set; }
    
    private int allDescendantNum;
    public abstract int AllDescendantNum { get; set; }
    
    private bool isAllDescendantDead;
    public abstract bool IsAllDescendantDead { get; set; }
}
