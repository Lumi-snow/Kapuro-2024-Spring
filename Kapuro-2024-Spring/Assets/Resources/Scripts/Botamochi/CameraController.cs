using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ほぼbota_PlayerControllerと同じです
public class CameraController : MonoBehaviour
{
    float speed;
    float xLimit;
    float yLimit;

    void Start()
    {
        VariableCollection variableCollection = GameObject.Find("VariableCollection").GetComponent<VariableCollection>();
        this.xLimit = variableCollection.xLimit;
        this.yLimit = variableCollection.yLimit;
        this.speed = variableCollection.speed;
    }

    void Update()
    {
        //時間切れになるとカメラは停止する
        if (!TimeScript.instance.isTimeUp)
        {
            move();
        }
    }

    void move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -speed, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, speed, 0);
        }

        //オブジェクトの現在座標を取得
        Vector3 currentPos = transform.position;

        //Mathf.Clampにより、範囲を超えないようにする
        currentPos.x = Mathf.Clamp(currentPos.x, -xLimit, xLimit);
        currentPos.y = Mathf.Clamp(currentPos.y, -yLimit, yLimit);

        //positionをcurrentposにする
        transform.position = currentPos;
    }
}

/*
Mathf.Clampのリファレンス https://docs.unity3d.com/ja/2017.4/ScriptReference/Mathf.Clamp.html
*/
