using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����I�u�W�F�N�g�̏����擾
        GameObject collidedObject = collision.gameObject;
        if (gameObject.name == collidedObject.name)
        {
            int myLevel = gameObject.GetComponent<SandStatus>().sizeLevel;//�ǂ�player�ɂ�sizeLevel�͖����̂ł����ŏ�����
            int otherLevel = collidedObject.GetComponent<SandStatus>().sizeLevel;//����

            if (myLevel == otherLevel)
            {
                Debug.Log("���́I (Level " + otherLevel + ")");
            }
        }
    }
}
