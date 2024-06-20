using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShikkuiGenerator : MonoBehaviour
{
    public GameObject ShikkuiPrefab;
    GameObject Kura;
    GameObject Player;

    //蔵の半径
    float scale_kura;

    //Shikkuiの半径
    float scale_shikkui;

    void Start()
    {
        VariableCollection variableCollection = GameObject.Find("VariableCollection").GetComponent<VariableCollection>();
        this.scale_kura = variableCollection.scale_kura;
        this.scale_shikkui = variableCollection.scale_shikkui;


        this.Kura = GameObject.Find("Kura");
        this.Player = GameObject.Find("Player");
        //Shikkuiが蔵をはみ出ないようにするために使用
        float scale = scale_kura - scale_shikkui;
        Vector3 Pos_Kura = Kura.transform.position;
    }

    void Update()
    {
        //nullチェック
        if (Player == null)
        {
            gameObject.GetComponent<ShikkuiGenerator>().enabled = false;
        }

        //Aを押したら漆喰を壁に貼り付ける
        if (Input.GetKeyDown(KeyCode.A))
        {
            GenerateShikkui(0);
            Debug.Log("a");
        }
    }

    void GenerateShikkui(int a)
    {
        //Shikkuiをplayerの位置に生成
        if (a == 0)
        {
            GameObject go = Instantiate(ShikkuiPrefab);

            float x = Player.transform.position.x;
            float y = Player.transform.position.y;
            float z = Player.transform.position.z;

            go.transform.position = new Vector3(x, y, z);

        }
    }
}
