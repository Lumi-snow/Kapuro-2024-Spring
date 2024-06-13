using System.Collections.Generic;
using UnityEngine;

//���𐶐�����N���X
public class RoofTileGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> roofTileType; //����Prefab
    
    [SerializeField] private RoofTileController roofTileController; //RoofTileController
    [SerializeField] private PrefabController prefabController; //PrefabController
    [SerializeField] private AddGameObjectController addGameObjectController; //AddGameObjectController

    //������
    public void Initialize()
    {
        //PrefabController�Ɋ���Prefab��o�^
        foreach(GameObject _roofTile in roofTileType)
        {
            prefabController.AddNewPrefab(_roofTile);
        }
        
        //��̃Q�[���I�u�W�F�N�g��Canvas�̎q�Ƃ��Đ���
        addGameObjectController.SetPairGameObject("RoofTiles", "Canvas");
        addGameObjectController.AddGameObject();
        
        for(int i = 0; i < roofTileController.allRoofTileNum; i++)
        {
            GenerateRoofTile(); //���𐶐�
        }
    }

    //���𐶐�
    public void GenerateRoofTile()
    {
        int randomValue = UnityEngine.Random.Range(0, roofTileType.Count); //���̎�ނ������_���Ɍ���

        switch (randomValue)
        {
            case 0: //CorrectRoofTile�𐶐�
                prefabController.InstantiatePrefab("CorrectRoofTile", Vector3.zero, Quaternion.identity, addGameObjectController.NewGameObject); //Prefab����CorrectRoofTile�𕡐�
                GameObject correctRoofTile = prefabController.clonePrefab;
                correctRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //CorrectRoofTile�̕]����NOT_EVALUATED�ɐݒ�
                roofTileController.roofTiles.Add(correctRoofTile); //��������CorrectRoofTile�����X�g�ɒǉ�
                break;
            case 1: //BrokenRoofTile�𐶐�
                prefabController.InstantiatePrefab("BrokenRoofTile", Vector3.zero, Quaternion.identity, addGameObjectController.NewGameObject); //Prefab����BrokenRoofTile�𕡐�
                GameObject brokenRoofTile = prefabController.clonePrefab;
                brokenRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //BrokenRoofTile�̕]����NOT_EVALUATED�ɐݒ�
                roofTileController.roofTiles.Add(brokenRoofTile); //��������BrokenRoofTile�����X�g�ɒǉ�
                break;
            default:
                Debug.Log("Error occured in Generate(), RoofTileGenerator");
                break;
        }
    }

    //DEBUG List�̒��g��\��
    private void PrintList()
    {
        foreach(GameObject _roofTile in roofTileController.roofTiles)
        {
            if (_roofTile != null)
            {
                Debug.Log("���a���I");
                Debug.Log(_roofTile.GetType().Name);
            }
            else
            {
                Debug.Log("Error occured in PrintList(), RoofGenerator");
            }   
        }
    }
}
