using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUpEraser : MonoBehaviour
{
    void Update()
    {
        //時間切れになったらアタッチされているオブジェクトを削除します
        if(TimeScript.instance.isTimeUp)
        {
            Destroy(gameObject);
            Debug.Log("a");
        }
    }
}
