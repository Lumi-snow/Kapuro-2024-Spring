using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController_HowToPlay : MonoBehaviour
{
    //Buttons
    [Header("�V�[���ύX - button")]
    [SerializeField] private Button buttonBackToTitle; //[SerializeField]�t�B�[���h(�ϐ�)���C���X�y�N�^(�E�̃E�B���h�E)����ҏW�ł���悤�ɂ���

    //Panels
    List<GameObject> panelList = new List<GameObject>();

    [Header("�V�ѕ� - panel")]
    [Header("Panel01")]
    [SerializeField] private GameObject panelScene01;
    [SerializeField] private Button panelScene01NextButton;
    [SerializeField] private Button panelScene01PrevButton;
    [SerializeField] private TextMeshProUGUI panelScene01Title;

    [Header("Panel02")]
    [SerializeField] private GameObject panelScene02;
    [SerializeField] private Button panelScene02NextButton;
    [SerializeField] private Button panelScene02PrevButton;
    [SerializeField] private TextMeshProUGUI panelScene02Title;

    [Header("Panel03")]
    [SerializeField] private GameObject panelScene03;
    [SerializeField] private Button panelScene03NextButton;
    [SerializeField] private Button panelScene03PrevButton;
    [SerializeField] private TextMeshProUGUI panelScene03Title;

    [Header("Panel04")]
    [SerializeField] private GameObject panelScene04;
    [SerializeField] private Button panelScene04NextButton;
    [SerializeField] private Button panelScene04PrevButton;
    [SerializeField] private TextMeshProUGUI panelScene04Title;

    private void Start()
    {
        //Button��ClickEvent��ǉ�
        buttonBackToTitle.onClick.AddListener(OnClickButtonBackToTitle); //�N���b�N���ꂽ�玩���ň����̒��̊֐������s

        panelScene01NextButton.onClick.AddListener(OnClickButtonNextPanel);
        panelScene02NextButton.onClick.AddListener(OnClickButtonNextPanel);
        panelScene03NextButton.onClick.AddListener(OnClickButtonNextPanel);
        panelScene04NextButton.onClick.AddListener(OnClickButtonNextPanel);

        panelScene01PrevButton.onClick.AddListener(OnClickButtonPrevPanel);
        panelScene02PrevButton.onClick.AddListener(OnClickButtonPrevPanel);
        panelScene03PrevButton.onClick.AddListener(OnClickButtonPrevPanel);
        panelScene04PrevButton.onClick.AddListener(OnClickButtonPrevPanel);

        //panel�̏�����
        initialPanel();
    }

    private void OnClickButtonBackToTitle()
    {
        SceneController.ChangeSceneToTitle();
    }

    private void OnClickButtonNextPanel()
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if (panelList[i].activeSelf == true) //���g���A�N�e�B�u���m�F
            {
                panelList[i].SetActive(false); //���݂̃p�l�����A�N�e�B�u�ɂ���
                panelList[(i + 1) % panelList.Count].SetActive(true); //���̃p�l�����A�N�e�B�u�ɂ���
                break;
            }
        }
    }

    private void OnClickButtonPrevPanel()
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if (panelList[i].activeSelf == true)
            {
                panelList[i].SetActive(false);
                panelList[(i - 1 + panelList.Count) % panelList.Count].SetActive(true); //�O�̃p�l�����A�N�e�B�u�ɂ���
                break;
            }
        }
    }

    private void initialPanel()
    {
        //�p�l���𓮓I�Ɏ擾���ă��X�g�ɒǉ�
        for (int i = 1; i <= 4; i++)
        {
            string panelName = "PanelScene0" + i.ToString(); //"PanelScene01", "PanelScene02", "PanelScene03", "PanelScene04"��T��
            GameObject panel = GameObject.Find(panelName);

            //��O����
            if (panel != null)
            {
                panelList.Add(panel);
                Debug.Log("Added " + panelName + " to panelList");
            }
            else
            {
                Debug.LogWarning("Panel " + panelName + " not found!");
            }
        }

        panelScene01.SetActive(true);
        panelScene02.SetActive(false);
        panelScene03.SetActive(false);
        panelScene04.SetActive(false);
    }
}
