using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshProを扱う際に必要
 
public class UnpaintedAreaCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countText;
    int count;
    public static UnpaintedAreaCount instance;
 
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
 
    void Start()
    {
        VariableCollection variableCollection = GameObject.Find("VariableCollection").GetComponent<VariableCollection>();
        count = variableCollection.value_area;
    }
 
    void Update()
    {
        if (0 < count) 
        {
            countText.text = "Count: " + count.ToString("F1");
        }
        else if(0 >= count)
        {
            countText.text = "Clear!";
        }
    }
}
