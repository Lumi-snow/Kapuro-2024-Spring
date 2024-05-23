using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButton : MonoBehaviour, ISceneButton
{
    [SerializeField] private string sceneName; //�V�[���̖��O

    //�A�N�Z�T
    public string SceneName
    {
        set //setSceneName()
        {
            sceneName = value; //value(���O�̕ύX�s��)�Œl��ݒ肷��
        }
        get //getSceneName()
        {
            return sceneName;
        }
    }

    public void onClick()
    {
        SceneController.ChangeScene(sceneName);
    }
}
