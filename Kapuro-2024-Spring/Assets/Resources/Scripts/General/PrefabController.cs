using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

//PrefabのScriptableObject
[CreateAssetMenu(fileName = "PrefabData", menuName = "Prefabs")]
public class PrefabList : ScriptableObject
{
    public List<GameObject> prefabs;
}

//プレハブを管理するクラス
[Serializable]
public class PrefabController : MonoBehaviour
{
    //プレハブのScriptableObject
    [SerializeField] PrefabList prefabList;

    public GameObject clonePrefab;
    
    //新しいプレハブを追加する
    public void AddNewPrefab(GameObject newPrefab)
    {
        prefabList.prefabs.Add(newPrefab);
        
    }
    
    //任意のプレハブを取得する
    public GameObject GetPrefab(string prefabName)
    {
        return prefabList.prefabs.Find(prefab => prefab.name == prefabName);
    }
    
    //任意のプレハブを削除する
    public void RemovePrefab(string prefabName)
    {
        prefabList.prefabs.Remove(prefabList.prefabs.Find(prefab => prefab.name == prefabName));
    }
    
    //全てのプレハブを削除する
    public void RemoveAllPrefabs()
    {
        if (prefabList.prefabs.Count == 0)
        {
            Debug.Log("Notice: PrefabList is empty.");
            return;
        }
        
        prefabList.prefabs.Clear();
    }
    
    /*OverLoadされた関数群*/
    //任意のプレハブをインスタンス化する
    public void InstantiatePrefab(string prefabName, Vector3 position, Quaternion rotation, GameObject parent)
    {
        clonePrefab = Instantiate(GetPrefab(prefabName), position, rotation, parent.transform);
    }
    
    public void InstantiatePrefab(string prefabName, Vector3 position, Quaternion rotation)
    {
        clonePrefab = Instantiate(GetPrefab(prefabName), position, rotation);
    }
    
    public void InstantiatePrefab(string prefabName, Vector3 position)
    {
        clonePrefab = Instantiate(GetPrefab(prefabName), position, Quaternion.identity);
    }

    public void InstantiatePrefab(string prefabName)
    {
        clonePrefab = Instantiate(GetPrefab(prefabName), Vector3.zero, Quaternion.identity);
    }
    /*OverLoadされた関数群*/

    //非同期処理で全てのプレハブを削除する
    public async UniTask RemoveAllPrefabListAsync()
    {
        await UniTask.Delay(1000);
        RemoveAllPrefabs();
    }
}
