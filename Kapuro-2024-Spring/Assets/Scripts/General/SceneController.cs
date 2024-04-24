using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController
{
    public static void ChangeSceneToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public static void ChangeSceneToScene01()
    {
        SceneManager.LoadScene("Scene01");
    }

    public static void ChangeSceneToScene02()
    {
        SceneManager.LoadScene("Scene02");
    }

    public static void ChangeSceneToScene03()
    {
        SceneManager.LoadScene("Scene03");
    }

    public static void ChangeSceneToScene04()
    {
        SceneManager.LoadScene("Scene04");
    }
}
