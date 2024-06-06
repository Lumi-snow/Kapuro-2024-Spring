using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameEndProcessing : MonoBehaviour
{
    private GameObject GameEndPopUp;

    private void Update()
    {
        GameEndHandler();
    }

    private void GameEndHandler()
    {
        if (Input.GetKey(KeyCode.Escape) == true)
        {
            Instantiate(PopUp);
        }
    }
}

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif