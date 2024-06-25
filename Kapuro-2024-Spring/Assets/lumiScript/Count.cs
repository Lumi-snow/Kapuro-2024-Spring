using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Count : MonoBehaviour
{
    public Text countText;
    public static int count;
    [SerializeField] private string tagName;
    private StartSignalScript startSignalScript;

    void Start()
    {
        count = 0;
        SetCountText();
        startSignalScript = FindObjectOfType<StartSignalScript>(); // StartSignalScriptのインスタンスを探す
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D obj)
    {
        if (startSignalScript != null && startSignalScript.signal == true)
        {
            if (obj.gameObject.tag == tagName)
            {
                count++;
                Debug.Log(count);
                SetCountText();
            }
        }
    }

    void SetCountText()
    {
        countText.text = count.ToString();
    }

    public static int getCount()
    {
        return count;
    }
 }
