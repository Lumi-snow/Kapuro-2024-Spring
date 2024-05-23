using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //�V�[���Ǘ��ɕK�{

public static class SceneController
{
    public static void ChangeSceneToTitle()
    {
        SceneManager.LoadScene("Title");
        Debug.Log("Title�Ɉړ����܂���");
    }

    public static void ChangeSceneToScene01()
    {
        SceneManager.LoadScene("Scene01");
        Debug.Log("Scene01�Ɉړ����܂���");
    }

    public static void ChangeSceneToScene02()
    {
        SceneManager.LoadScene("Scene02");
        Debug.Log("Scene02�Ɉړ����܂���");
    }

    public static void ChangeSceneToScene03()
    {
        SceneManager.LoadScene("Scene03");
        Debug.Log("Scene03�Ɉړ����܂���");
    }

    public static void ChangeSceneToScene04()
    {
        SceneManager.LoadScene("Scene04");
        Debug.Log("Scene04�Ɉړ����܂���");
    }

    public static void ChangeSceneToHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
        Debug.Log("HowToPlay�Ɉړ����܂���");
    }

    public static void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log(sceneName + "�Ɉړ����܂���");
    }
}

/*�V�[���̒ǉ����@*/

///1,Unity�G�f�B�^�����File����New Scene��I��
///2,Unity�G�f�B�^�����File����Save as...��Scenes�t�H���_��Scene��ۑ�
///3,Unity�G�f�B�^�����Build settings����Add open scenes�Ō��݂̃V�[����ǉ�����
///4,�����Ɋ֐�������(static�C���q��Y�ꂸ�ɁI)
///5,�N���X��.�֐����ŃV�[�����Ăяo��
