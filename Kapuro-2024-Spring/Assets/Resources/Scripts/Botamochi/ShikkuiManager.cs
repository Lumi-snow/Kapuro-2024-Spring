using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShikkuiManager : MonoBehaviour
{
    private ShikkuiStatus shikkuiStatus;
    public GameObject ShikkuiPrefab;
    GameObject Kura;
    GameObject Player;

    //蔵の半径
    float scale_kura;

    //Shikkuiの半径
    float scale_shikkui;

    void Start()
    {
        this.shikkuiStatus = new ShikkuiStatus();

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
        int i = shikkuiStatus.Status;
        //nullチェック
        if (Player == null)
        {
            gameObject.GetComponent<ShikkuiManager>().enabled = false;
        }

        //Aを押したら漆喰を壁に貼り付ける
        if (i == 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                GenerateShikkui(0);
                shikkuiStatus.Status = 1;
                Debug.Log(shikkuiStatus.Status);
            }
        }

        //Aを押したら漆喰を塗る
        else if (i == 1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                //GenerateShikkui(1);
                shikkuiStatus.Status = 0;
                Debug.Log(shikkuiStatus.Status);
            }
        }

        //Aを押したら漆喰をこて板に乗せる
        else if (i == 2)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                GenerateShikkui(1);
                shikkuiStatus.Status = 0;
                Debug.Log(shikkuiStatus.Status);
            }
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
