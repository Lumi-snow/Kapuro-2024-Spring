using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController_HowToPlay : MonoBehaviour
{
    public static UIController_HowToPlay Instance { get; private set; } //���g�̃C���X�^���X

    //Buttons
    [Header("�V�[���ύX - button")]
    [SerializeField] private Button buttonBackToTitle; //[SerializeField]�t�B�[���h(�ϐ�)���C���X�y�N�^(�E�̃E�B���h�E)����ҏW�ł���悤�ɂ���

    //Panels
    List<Panel> panelList = new List<Panel>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; //���g�̃C���X�^���X��ݒ�
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Button��ClickEvent��ǉ�
        buttonBackToTitle.onClick.AddListener(OnClickButtonBackToTitle); //�N���b�N���ꂽ�玩���ň����̒��̊֐������s

        //panel�̏�����
        initializePanels();
    }

    private void OnClickButtonBackToTitle()
    {
        SceneController.ChangeSceneToTitle();
    }

    public void showNextPanel()
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if (panelList[i].isActive() == true) //���g���A�N�e�B�u���m�F
            {
                panelList[i].hide(); //���݂̃p�l�����A�N�e�B�u�ɂ���
                panelList[(i + 1) % panelList.Count].show(); //���̃p�l�����A�N�e�B�u�ɂ���
                break;
            }
        }
    }

    public void hidePrevPanel()
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            if (panelList[i].isActive() == true)
            {
                panelList[i].hide();
                panelList[(i - 1 + panelList.Count) % panelList.Count].show(); //�O�̃p�l�����A�N�e�B�u�ɂ���
                break;
            }
        }
    }

    private void initializePanels()
    {
        //�p�l���𓮓I�Ɏ擾���ă��X�g�ɒǉ�
        for (int i = 1; i <= 4; i++)
        {
            string panelName = "PanelScene0" + i.ToString(); //"PanelScene01", "PanelScene02", "PanelScene03", "PanelScene04"��T��
            GameObject panelObject = GameObject.Find(panelName); //panelName�����Ƃ�panelObject���擾

            if(panelObject != null)
            {
                Panel panel = panelObject.GetComponent<Panel>(); //panelObject����Panel�R���|�[�l���g���擾(�A�^�b�`����Ă���X�N���v�g�̂���)

                if(panel != null)
                {
                    panel.initialize(); //������
                    panelList.Add(panel); //List�ɒǉ�
                    panel.hide(); //��\���ɂ���

                    Debug.Log("Add " + panelName + "to panelList");
                }
                else
                {
                    Debug.LogWarning("Error occured while panel adding");
                }
            }
            else
            {
                Debug.LogWarning("Error occured while panel searching");
            }
        }

        //�p�l���̒ǉ��ɐ��������ꍇ�A1�Ԗڂ̃p�l����\��
        if(panelList.Count > 0)
        {
            panelList[0].show();
        }
    }
}
