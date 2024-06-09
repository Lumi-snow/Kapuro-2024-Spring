using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

//Prefab��ScriptableObject
[CreateAssetMenu(fileName = "PrefabData", menuName = "Prefabs")]
public class PrefabList : ScriptableObject
{
    public List<GameObject> prefabs;
}

//�v���n�u���Ǘ�����N���X
[Serializable]
public class PrefabController : MonoBehaviour
{
    //�v���n�u��ScriptableObject
    [SerializeField] PrefabList prefabList;

    public GameObject clonePrefab;
    
    //�V�����v���n�u��ǉ�����
    public void AddNewPrefab(GameObject newPrefab)
    {
        prefabList.prefabs.Add(newPrefab);
        
    }
    
    //�C�ӂ̃v���n�u���擾����
    public GameObject GetPrefab(string prefabName)
    {
        return prefabList.prefabs.Find(prefab => prefab.name == prefabName);
    }
    
    //�C�ӂ̃v���n�u���폜����
    public void RemovePrefab(string prefabName)
    {
        prefabList.prefabs.Remove(prefabList.prefabs.Find(prefab => prefab.name == prefabName));
    }
    
    //�S�Ẵv���n�u���폜����
    public void RemoveAllPrefabs()
    {
        if (prefabList.prefabs.Count == 0)
        {
            Debug.Log("Notice: PrefabList is empty.");
            return;
        }
        
        prefabList.prefabs.Clear();
    }
    
    /*OverLoad���ꂽ�֐��Q*/
    //�C�ӂ̃v���n�u���C���X�^���X������
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
    /*OverLoad���ꂽ�֐��Q*/

    //�񓯊������őS�Ẵv���n�u���폜����
    public async UniTask RemoveAllPrefabListAsync()
    {
        await UniTask.Delay(1000);
        RemoveAllPrefabs();
    }
}
