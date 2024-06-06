using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerTitle : MonoBehaviour
{
    //Buttons
    [SerializeField] private Button buttonScene01; //[SerializeField]�t�B�[���h(�ϐ�)���C���X�y�N�^(�E�̃E�B���h�E)����ҏW�ł���悤�ɂ���
    [SerializeField] private Button buttonScene02;
    [SerializeField] private Button buttonScene03;
    [SerializeField] private Button buttonScene04;
    [SerializeField] private Button buttonHowToPlay;

    //texts
    [SerializeField] private TextMeshProUGUI textTitle;

    //Button�̔z��@�C�x���g�n���h���ƃV�[���̖��O���i�[
    private List<SceneButton> sceneButtons = new List<SceneButton>();

    private void Start()
    {
        AddSceneButton(buttonScene01, "Scene01");
        AddSceneButton(buttonScene02, "Scene02");
        AddSceneButton(buttonScene03, "Scene03");
        AddSceneButton(buttonScene04, "Scene04");
        AddSceneButton(buttonHowToPlay, "HowToPlay");
    }

    private void AddSceneButton(Button button, string sceneName)
    {
        SceneButton sceneButton = button.gameObject.AddComponent<SceneButton>();
        sceneButton.SceneName = sceneName; //�Z�Z.�A�N�Z�T���ŃA�N�Z�T�ɃA�N�Z�X���邱�Ƃ��ł���
        sceneButtons.Add(sceneButton);
        button.onClick.AddListener(sceneButton.onClick);
    }
}
