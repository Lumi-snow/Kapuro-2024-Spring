using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerResult : MonoBehaviour
{
    [SerializeField] private RankingSample rankingSample;
    
    [SerializeField] public TMP_InputField nameInputField;
    [SerializeField] public TMP_InputField scoreInputField;
    [SerializeField] public TextMeshProUGUI rankingText;
    
    [SerializeField] private Button nameSendButton;
    [SerializeField] private Button scoreSendButton;
    [SerializeField] private Button getRankingbutton;

    private void Start()
    {
        nameInputField.onValueChanged.AddListener(OnNameInputFieldChanged);
        scoreInputField.onValueChanged.AddListener(OnScoreInputFieldChanged);
        
        nameSendButton.onClick.AddListener(OnNameSendButtonClicked);
        scoreSendButton.onClick.AddListener(OnScoreSendButtonClicked);
        getRankingbutton.onClick.AddListener(OnGetRankingButtonClicked);
    }
    
    private void OnNameInputFieldChanged(string name)
    {
        nameInputField.text = name;
    }
    
    private void OnScoreInputFieldChanged(string score)
    {
        scoreInputField.text = score;
    }
    
    private void OnNameSendButtonClicked()
    {
        rankingSample.UpdateUserName(nameInputField);
    }
    
    private void OnScoreSendButtonClicked()
    {
        rankingSample.UpdatePlayerStatistics(scoreInputField);
    }
    
    private void OnGetRankingButtonClicked()
    {
        rankingSample.GetLeaderboard();
    }
}
