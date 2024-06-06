using UnityEngine;

public class AddGameObjectController : MonoBehaviour
{
    private GameObject newGameObject; //追加するGameObject
    public GameObject NewGameObject //追加するGameObjectのプロパティ
    {
        get => newGameObject;
    }
    
    private GameObject parentObject; //親となるGameObject
    public GameObject ParentObject //親となるGameObjectのプロパティ
    {
        get => parentObject;
    }

    //GameObjectのペアを設定する
    public void SetPairGameObject(string newObjectName, string parentObjectName)
    {
        newGameObject = new GameObject(newObjectName);
        parentObject = GameObject.Find(parentObjectName);
    }

    //親となるGameObjectを設定する
    public void SetParentGameObject(string parentObjectName)
    {
        parentObject = GameObject.Find(parentObjectName);
    }

    //新しいGameObjectを設定する
    public void SetNewGameObject(string newObjectName)
    {
        newGameObject = new GameObject(newObjectName);
    }

    //新しいGameObjectを追加する
    public void AddGameObject()
    {
        newGameObject.transform.SetParent(parentObject.transform, false);
    }
}