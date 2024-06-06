using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingUnpaintedArea : MonoBehaviour
{
    //塗り残し
    GameObject UnpaintedArea;
    GameObject Player;

    //UnpaintedAreaの半径
    [SerializeField] float scale;

    void Start()
    {
        this.UnpaintedArea = GameObject.Find("UnpaintedArea");
        this.Player = GameObject.Find("Player");
    }

    void Update()
    {
        //キーを押した際にそれぞれのオブジェクトの座標を参照し、ある値であったらUnpaintedAreaを削除する
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 Pos_UnpaintedArea = UnpaintedArea.transform.position;
            Vector3 Pos_Player = Player.transform.position;
            //Playerの中心がUnpaintedArea内にあれば削除する
            if (Pos_Player.x < Pos_UnpaintedArea.x + scale && Pos_Player.x > Pos_UnpaintedArea.x - scale &&
            Pos_Player.y < Pos_UnpaintedArea.y + scale && Pos_Player.y > Pos_UnpaintedArea.y - scale)
            {
                Destroy(UnpaintedArea);
            }
        }
    }
}
