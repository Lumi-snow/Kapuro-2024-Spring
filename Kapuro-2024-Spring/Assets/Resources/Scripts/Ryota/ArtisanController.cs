using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtisanController : MonoBehaviour
{
    GameObject WallLeft;
    GameObject WallRight;
    float speed = 3f;
    int distance = 50;//•Ç‚Æplayer‚Ì‹——£

    void Start()
    {
        this.WallLeft = GameObject.Find("WallLeft");
        this.WallRight = GameObject.Find("WallRight");
    }

    void Update()
    {
        
        float Px = transform.position.x;//player‚ÌxÀ•W
        float Lx = this.WallLeft.transform.position.x;
        float Rx = this.WallRight.transform.position.x;

        if (Input.GetKey(KeyCode.LeftArrow) && Px > Lx + distance)
        {
            transform.Translate(-1 * speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow) && Px < Rx - distance)
        {
            transform.Translate(speed, 0, 0);
        }

    }
}
