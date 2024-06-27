using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Count : MonoBehaviour
{
    public Text countText;
    public int count;
    [SerializeField] private string tagName;
    private StartSignalScript startSignalScript;
    public static int countA;
    public static int countB;
    public static int countC;
    public static int countD;


    void Start()
    {
        count = 0;
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
                countText.text = count.ToString();

                if (obj.gameObject.tag == "ItemA")
                {
                    countA++; 
                    Debug.Log("Aとれた");
                }
                if (obj.gameObject.tag == "ItemB")
                    countB++;
                if (obj.gameObject.tag == "ItemC")
                    countC++;
                if (obj.gameObject.tag == "ItemD")
                    countD++;
            }
        }
    }
    public static int getCountA()
       {
           return countA;
       }
    public static int getCountB() {
        return countC;    
    }
    public static int getCountC()
    {
       return countC;
    }
    public static int getCountD()
    {
        return countD;
    }
}
 
