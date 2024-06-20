using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpaintedAreaGenerator : MonoBehaviour
{
    public GameObject UnpaintedAreaPrefab;
    GameObject Kura;

    //蔵の半径
    float scale_kura;

    //UnpaintedAreaの半径
    float scale_area;

    //UnpointedareaのZの値
    float z;

    //何個のUnpaintedAreaが生成されるかを決める
    int value;
    void Start()
    {
        VariableCollection variableCollection = GameObject.Find("VariableCollection").GetComponent<VariableCollection>();
        this.scale_kura = variableCollection.scale_kura;
        this.scale_area = variableCollection.scale_area;
        this.z = variableCollection.z_area;
        this.value = variableCollection.value_area;


        this.Kura = GameObject.Find("Kura");
        //UnpaintedAreaが蔵をはみ出ないようにするために使用
        float scale = scale_kura - scale_area;
        Vector3 Pos_Kura = Kura.transform.position;

        //UnpaintedAreaを指定数生成
        for (int i = 0; i < value; i++)
        {
            GameObject go = Instantiate(UnpaintedAreaPrefab);

            float px = Random.Range(Pos_Kura.x - scale, Pos_Kura.x + scale);
            float py = Random.Range(Pos_Kura.y - scale, Pos_Kura.y + scale);

            go.transform.position = new Vector3(px, py, z);

        }
    }
}
