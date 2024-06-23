using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableCollection : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float xLimit;
    [SerializeField] public float yLimit;

    //蔵の半径
    [SerializeField] public float scale_kura;

    //UnpaintedAreaの半径
    [SerializeField] public float scale_shikkui;

    //UnpointedareaのZの値
    [SerializeField] public float z_shikkui;

}

