using System.Collections;
using System.Collections.Generic;
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
        
    void Start()
    {
        
    }

    void Update()
    {
        time = time + Time.deltaTime;

        if(time > 3.0f)
        {

            float x = Random.Range(rangeA.position.x, rangeB.position.x);
            float y = Random.Range(rangeA.position.y, rangeB.position.y);

            Instantiate(createprefab, new Vector2(x, y), createprefab.transform.rotation);

            time = 0f;
        }
    }
}
