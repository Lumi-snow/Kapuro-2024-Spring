using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingUnpaintedArea : MonoBehaviour
{
    //塗り残し
    GameObject Player;

    //UnpaintedAreaの半径
    [SerializeField] float scale;

    void Start()
    {
        this.Player = GameObject.Find("Player");
    }

    void Update()
    {
        /*
        nullチェック
        TimeUp後にNullRefferenceExceptionが発生するのを防ぐ
        */
        if (gameObject == null || Player == null)
        {
            gameObject.GetComponent<RemovingUnpaintedArea>().enabled = false;
        }

        //キーを押した際にそれぞれのオブジェクトの座標を参照し、ある値であったらUnpaintedAreaを削除する
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 Pos_UnpaintedArea = gameObject.transform.position;
            Vector3 Pos_Player = Player.transform.position;
            //Playerの中心がUnpaintedArea内にあれば削除する
            if (Pos_Player.x < Pos_UnpaintedArea.x + scale && Pos_Player.x > Pos_UnpaintedArea.x - scale &&
            Pos_Player.y < Pos_UnpaintedArea.y + scale && Pos_Player.y > Pos_UnpaintedArea.y - scale)
            {
                Destroy(gameObject);
            }
        }
    }
}
