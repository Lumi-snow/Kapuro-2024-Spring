using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIControllerStartupProcessing : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private TextMeshProUGUI completeText;

    public void OnStartLoadingUI()
    {
        loadingText.gameObject.SetActive(true);
        completeText.gameObject.SetActive(false);
    }
    
    public void OnCompleteLoadingUI()
    {
        loadingText.gameObject.SetActive(false);
        completeText.gameObject.SetActive(true);
    }
}
