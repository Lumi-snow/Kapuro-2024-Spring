using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class moveToMain : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
          LoadScene();
        }
    }
    void LoadScene()
    {
        SceneController.ChangeScene("lumi");
    }
}
