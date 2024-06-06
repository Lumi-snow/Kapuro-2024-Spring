using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Prefabの抽象クラス
[Serializable]
public abstract class PrefabData : MonoBehaviour
{
    protected List<Button> abstractButtons;
    protected List<TextMeshProUGUI> abstractTexts;
    
    protected string prefabName;
    protected GameObject prefab;

    public PrefabData() { }

    public PrefabData(GameObject prefab, string prefabName)
    {
        this.prefab = prefab;
        this.prefabName = prefabName;
    }
}