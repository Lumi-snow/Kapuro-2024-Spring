using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Buttons
    [SerializeField] private Button buttonScene01; //[SerializeField]�t�B�[���h(�ϐ�)���C���X�y�N�^(�E�̃E�B���h�E)����ҏW�ł���悤�ɂ���
    [SerializeField] private Button buttonScene02;
    [SerializeField] private Button buttonScene03;
    [SerializeField] private Button buttonScene04;

    //texts
    [SerializeField] private TextMeshProUGUI textTitle;

    private void Start()
    {
        //Button��ClickEvent��ǉ�
        buttonScene01.onClick.AddListener(OnClickButtonScene01); //�N���b�N���ꂽ�玩���ň����̒��̊֐������s
        buttonScene02.onClick.AddListener(OnClickButtonScene02);
        buttonScene03.onClick.AddListener(OnClickButtonScene03);
        buttonScene04.onClick.AddListener(OnClickButtonScene04);
    }

    private void OnClickButtonScene01()
    {
        SceneController.ChangeSceneToScene01();
    }

    private void OnClickButtonScene02()
    {
        SceneController.ChangeSceneToScene02();
    }

    private void OnClickButtonScene03()
    {
        SceneController.ChangeSceneToScene03();
    }

    private void OnClickButtonScene04()
    {
        SceneController.ChangeSceneToScene04();
    }

}
