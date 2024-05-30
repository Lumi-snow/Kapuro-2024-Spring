using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //後から調整しやすいようにSerializeField化してあります。とりあえず0.05で動かします。
    [SerializeField] float speed; 
    //x, y方向の限界値　今は仮の値です
    float xLimit = 8.5f;
    float yLimit = 4.5f;

    void Update()
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
