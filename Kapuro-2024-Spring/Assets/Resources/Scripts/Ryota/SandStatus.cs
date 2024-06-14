using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandStatus : MonoBehaviour
{
    public int sizeLevel = 0;
    int size = 0;

    void Update()
    {
        switch (sizeLevel)
        {
            case 1: size = 20; break;
            case 2: size = 35; break;
            case 3: size = 50; break;
            case 4: size = 65; break;
            case 5: size = 80; break;
        }

        gameObject.transform.localScale = new Vector3(size, size, size);
    }
}
