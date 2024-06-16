using System.Collections.Generic;
using UnityEngine;

//���𐶐�����N���X
public class RoofTileGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> roofTileType; //����Prefab
    
    [SerializeField] private RoofTileController roofTileController; //RoofTileController
    [SerializeField] private BossController bossController; //BossController
    [SerializeField] private PrefabController prefabController; //PrefabController
    [SerializeField] private AddGameObjectController addGameObjectController; //AddGameObjectController
    
    [SerializeField] private GameObject roofTile; //����Prefab

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
        roofTile = addGameObjectController.NewGameObject;
        
        for(int i = 0; i < roofTileController.allRoofTileNum; i++)
        {
            GenerateRoofTile(); //���𐶐�
        }
        
        GenerateEventRoofTile(); //�C�x���g���𐶐�
    }

    //���𐶐�
    public void GenerateRoofTile()
    {
        int randomValue = UnityEngine.Random.Range(0, roofTileType.Count - 4); //���̎�ނ������_���Ɍ���
        int randomIndex = UnityEngine.Random.Range(3, (roofTileController.roofTiles.Count - 1) / 2); //�ǂ̃^�C�~���O�ŏo�������邩�������_���Ɍ���

        if (bossController.boss == null)
        {
            switch (randomValue)
            {
                case 0: //CorrectRoofTile�𐶐�
                    prefabController.InstantiatePrefab("CorrectRoofTile", Vector3.zero, Quaternion.identity, roofTile); //Prefab����CorrectRoofTile�𕡐�
                    GameObject correctRoofTile = prefabController.clonePrefab;
                    correctRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //CorrectRoofTile�̕]����NOT_EVALUATED�ɐݒ�
                    roofTileController.roofTiles.Add(correctRoofTile); //��������CorrectRoofTile�����X�g�ɒǉ�
                    break;
                case 1: //ExpensiveRoofTile�𐶐�
                    prefabController.InstantiatePrefab("ExpensiveRoofTile", Vector3.zero, Quaternion.identity, roofTile); //Prefab����ExpensiveRoofTile�𕡐�
                    GameObject expensiveRoofTile = prefabController.clonePrefab;
                    expensiveRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //ExpensiveRoofTile�̕]����NOT_EVALUATED�ɐݒ�
                    roofTileController.roofTiles.Add(expensiveRoofTile); //��������ExpensiveRoofTile�����X�g�ɒǉ�
                    break;
                case 2: //LegendRoofTile�𐶐�
                    prefabController.InstantiatePrefab("LegendRoofTile", Vector3.zero, Quaternion.identity, roofTile); //Prefab����LegendRoofTile�𕡐�
                    GameObject legendRoofTile = prefabController.clonePrefab;
                    legendRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //LegendRoofTile�̕]����NOT_EVALUATED�ɐݒ�
                    roofTileController.roofTiles.Add(legendRoofTile); //��������LegendRoofTile�����X�g�ɒǉ�
                    break;
                case 3: //BrokenRoofTile�𐶐�
                    prefabController.InstantiatePrefab("BrokenRoofTile", Vector3.zero, Quaternion.identity, roofTile); //Prefab����BrokenRoofTile�𕡐�
                    GameObject brokenRoofTile = prefabController.clonePrefab;
                    brokenRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //BrokenRoofTile�̕]����NOT_EVALUATED�ɐݒ�
                    roofTileController.roofTiles.Add(brokenRoofTile); //��������BrokenRoofTile�����X�g�ɒǉ�
                    break;
                default:
                    Debug.Log("Error occured in Generate(), RoofTileGenerator");
                    break;
            }
        }
        else
        {
            switch (randomValue)
            {
                case 0: //CorrectRoofTile�𐶐�
                    prefabController.InstantiatePrefab("CorrectRoofTile", Vector3.zero, Quaternion.identity, roofTile); //Prefab����CorrectRoofTile�𕡐�
                    GameObject correctRoofTile = prefabController.clonePrefab;
                    correctRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //CorrectRoofTile�̕]����NOT_EVALUATED�ɐݒ�
                    roofTileController.roofTiles.Insert(randomIndex, correctRoofTile); //��������CorrectRoofTile�����X�g�ɒǉ�
                    break;
                case 1: //ExpensiveRoofTile�𐶐�
                    prefabController.InstantiatePrefab("ExpensiveRoofTile", Vector3.zero, Quaternion.identity, roofTile); //Prefab����ExpensiveRoofTile�𕡐�
                    GameObject expensiveRoofTile = prefabController.clonePrefab;
                    expensiveRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //ExpensiveRoofTile�̕]����NOT_EVALUATED�ɐݒ�
                    roofTileController.roofTiles.Insert(randomIndex, expensiveRoofTile); //��������ExpensiveRoofTile�����X�g�ɒǉ�
                    break;
                case 2: //LegendRoofTile�𐶐�
                    prefabController.InstantiatePrefab("LegendRoofTile", Vector3.zero, Quaternion.identity, roofTile); //Prefab����LegendRoofTile�𕡐�
                    GameObject legendRoofTile = prefabController.clonePrefab;
                    legendRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //LegendRoofTile�̕]����NOT_EVALUATED�ɐݒ�
                    roofTileController.roofTiles.Add(legendRoofTile); //��������LegendRoofTile�����X�g�ɒǉ�
                    break;
                case 3: //BrokenRoofTile�𐶐�
                    prefabController.InstantiatePrefab("BrokenRoofTile", Vector3.zero, Quaternion.identity, roofTile); //Prefab����BrokenRoofTile�𕡐�
                    GameObject brokenRoofTile = prefabController.clonePrefab;
                    brokenRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //BrokenRoofTile�̕]����NOT_EVALUATED�ɐݒ�
                    roofTileController.roofTiles.Insert(randomIndex, brokenRoofTile); //��������BrokenRoofTile�����X�g�ɒǉ�
                    break;
                default:
                    Debug.Log("Error occured in Generate(), RoofTileGenerator");
                    break;
            }
        }
    }

    public void GenerateSpecialRoofTileForBoss()
    {
        switch(bossController.boss.GetComponent<AbstractBoss>().bossType)
        {
            case AbstractBoss.BossType.KAWARA_YOKAI:
                if (bossController.boss.GetComponent<AbstractBoss>().IsAllDescendantDead != true)
                {
                    for(int i = 0 ; i < bossController.boss.GetComponent<AbstractBoss>().AllDescendantNum ; i++)
                    {
                        int randomValue = UnityEngine.Random.Range(3, (roofTileController.roofTiles.Count - 1) / 2); //�ǂ̃^�C�~���O�ŏo�������邩�������_���Ɍ���
                        prefabController.InstantiatePrefab("KawaraYokai'sDescendant", Vector3.zero, Quaternion.identity, roofTile); //Prefab����KawaraYokaiDescendant�𕡐�
                        GameObject kawaraYokaisDescendant = prefabController.clonePrefab;
                        kawaraYokaisDescendant.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //KawaraYokaiDescendant�̕]����NOT_EVALUATED�ɐݒ�
                        roofTileController.roofTiles.Insert(randomValue, kawaraYokaisDescendant); //��������KawaraYokaiDescendant�����X�g�ɒǉ�

                        GenerateRoofTile(); //�ǉ��̊��𐶐�
                    }
                
                    bossController.boss.GetComponent<AbstractBoss>().IsAllDescendantDead = true; //AllDescendantNum��0�ɐݒ�
                }
                
                break;
            case AbstractBoss.BossType.SHISHIGAWARA:
                if (bossController.boss.GetComponent<AbstractBoss>().IsGenerateShishiGawaraWaterRoofTile == false)
                {
                    //���̉������銢�𐶐�
                    for(int i = 0 ; i < bossController.boss.GetComponent<AbstractBoss>().AllShishiGawaraWaterRoofTileNum ; i++)
                    {
                        int randomValue = UnityEngine.Random.Range(3, (roofTileController.roofTiles.Count - 1) / 2); //�ǂ̃^�C�~���O�ŏo�������邩�������_���Ɍ���
                        
                        prefabController.InstantiatePrefab("ShishiGawaraWaterRoofTile", Vector3.zero, Quaternion.identity, roofTile); //Prefab����shishiGawaraWaterRoofTile�𕡐�
                        GameObject shishiGawaraWaterRoofTile = prefabController.clonePrefab;
                        shishiGawaraWaterRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //shishiGawaraWaterRoofTile�̕]����NOT_EVALUATED�ɐݒ�
                        roofTileController.roofTiles.Insert(randomValue, shishiGawaraWaterRoofTile); //��������shishiGawaraWaterRoofTile�����X�g�ɒǉ�
                        
                        GenerateRoofTile(); //�ǉ��̊��𐶐�
                    }

                    //�J�𐶐�
                    for (int i = 0; i < bossController.boss.GetComponent<AbstractBoss>().AllShishiGawaraWhistleNum; i++)
                    {
                        int randomValue = UnityEngine.Random.Range(3, (roofTileController.roofTiles.Count - 1) / 2); //�ǂ̃^�C�~���O�ŏo�������邩�������_���Ɍ���
                        
                        prefabController.InstantiatePrefab("ShishiGawaraWhistle", Vector3.zero, Quaternion.identity, roofTile); //Prefab����shishiGawaraWaterRoofTile�𕡐�
                        GameObject shishiGawaraWhistle = prefabController.clonePrefab;
                        shishiGawaraWhistle.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //shishiGawaraWaterRoofTile�̕]����NOT_EVALUATED�ɐݒ�
                        roofTileController.roofTiles.Insert(randomValue, shishiGawaraWhistle); //��������shishiGawaraWaterRoofTile�����X�g�ɒǉ�
                        
                        GenerateRoofTile(); //�ǉ��̊��𐶐�
                    }
                    
                    bossController.boss.GetComponent<AbstractBoss>().IsGenerateShishiGawaraWaterRoofTile = true; //IsGenerateShishiGawaraWaterRoofTile��true�ɐݒ�
                }
                
                break;
            default:
                Debug.Log("Error occured in GenerateSpecialRoofTileForBoss(), RoofTileGenerator");
                break;
        }
    }

    private void GenerateEventRoofTile()
    {
        int generateIndex = roofTileController.allRoofTileNum - (roofTileController.allRoofTileNum / 3); //�ǂ̃^�C�~���O�ŏo�������邩�������_���Ɍ���
        
        prefabController.InstantiatePrefab("EventRoofTile", Vector3.zero, Quaternion.identity, roofTile); //Prefab����EventRoofTile�𕡐�
        GameObject eventRoofTile = prefabController.clonePrefab;
        eventRoofTile.GetComponent<RoofTile>().evaluateType = RoofTile.EvaluateType.NOT_EVALUATED; //EventRoofTile�̕]����NOT_EVALUATED�ɐݒ�
        roofTileController.roofTiles.Insert(generateIndex, eventRoofTile); //��������EventRoofTile�����X�g�ɒǉ�
    }
}
