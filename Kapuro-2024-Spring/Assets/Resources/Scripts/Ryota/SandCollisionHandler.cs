using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCollisionHandler : MonoBehaviour
{
    GameObject sandGenerator;

    void Start()
    {
        this.sandGenerator = GameObject.Find("SandGenerator");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトの情報を取得
        GameObject collidedObject = collision.gameObject;
        if (gameObject.name == collidedObject.name)
        {
            int myLevel = gameObject.GetComponent<SandStatus>().sizeLevel;//壁やplayerにはsizeLevelは無いのでここで初期化
            int otherLevel = collidedObject.GetComponent<SandStatus>().sizeLevel;//同文

            if (myLevel == otherLevel)
            {
                sandGenerator.GetComponent<SandGenerator>().createLevelUpSand(gameObject, collidedObject);
                Destroy(gameObject);
            }
        }
    }
}
