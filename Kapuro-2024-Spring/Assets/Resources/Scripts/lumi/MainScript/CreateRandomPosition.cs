using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateRandomPosition : MonoBehaviour
{
    [SerializeField]
    [Tooltip("itemA")]
    private GameObject createprefab;

    [SerializeField]
    [Tooltip("RangeA")]
    private Transform rangeA;

    [SerializeField]
    [Tooltip("RangeB")]
    private Transform rangeB;

    private float time;
    private StartSignalScript startSignalScript;
    private float previousTime;

    void Start()
    {
        startSignalScript = FindObjectOfType<StartSignalScript>(); // StartSignalScriptのインスタンスを探す
    }

    void Update()
    {
        float time = TimerController.CountDownTime;

        if (startSignalScript != null && startSignalScript.signal == true)
        {
            float currentTime = Mathf.Ceil(time);

            if(currentTime % 3 == 0 && currentTime != previousTime)
            {

                float x = Random.Range(rangeA.position.x, rangeB.position.x);
                float y = Random.Range(rangeA.position.y, rangeB.position.y);

                Instantiate(createprefab, new Vector2(x, y), createprefab.transform.rotation);
                previousTime = currentTime;
            }

            if (TimerController.CountDownTime <= 0.0f)
            {
                time = 0f;
            }
        }
    }
}
