using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject boss;
    
    [SerializeField] private BossGenerater bossGenerater;
    
    public void Initialize()
    {
        bossGenerater.Initialize();
    }
}
