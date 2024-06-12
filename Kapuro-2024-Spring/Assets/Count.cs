using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count : MonoBehaviour
{
    public Text countText;
    private int count;

    void Start()
    {
        count = 0;
        SetCountText();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.tag == "ItemA")
        {
            count++;
            Debug.Log(count);
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = count.ToString();
    }
}
