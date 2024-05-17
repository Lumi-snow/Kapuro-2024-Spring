using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Panel : MonoBehaviour, IPanel
{
    [Header("�p�l�����Ɗe�{�^��")]
    [SerializeField] private TextMeshProUGUI panelTitle; //�p�l���̖��O
    [SerializeField] private Button nextButton; //���̃p�l���ւ̃{�^��
    [SerializeField] private Button prevButton; //�O�̃p�l���ւ̃{�^��

    public void initialize()
    {
        //�{�^���N���b�N���̏�����o�^
        nextButton.onClick.AddListener(onClickNextButton);
        prevButton.onClick.AddListener(onClickPrevButton);
    }

    //�p�l����\��
    public void show()
    {
        gameObject.SetActive(true);
    }

    //�p�l�����\��
    public void hide()
    {
        gameObject.SetActive(false);
    }

    //�p�l�����A�N�e�B�u����A�N�e�B�u��
    public bool isActive()
    {
        return gameObject.activeSelf;
    }

    //���̃p�l���ւ̃{�^���������ꂽ���̏���
    private void onClickNextButton()
    {
        UIController_HowToPlay.Instance.showNextPanel();
    }

    //�O�̃p�l���ւ̃{�^���������ꂽ���̏���
    private void onClickPrevButton()
    {
        UIController_HowToPlay.Instance.hidePrevPanel();
    }
}
