using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject WallLeft;
    GameObject WallRight;
    float speed = 0.8f;
    int distance = 50;//•Ç‚Æplayer‚Ì‹——£

    // Start is called before the first frame update
    void Start()
    {
        this.WallLeft = GameObject.Find("WallLeft");
        this.WallRight = GameObject.Find("WallRight");
    }

    // Update is called once per frame
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
