using UnityEngine;

[CreateAssetMenu(fileName = "Prefabs")]
public class Prefabs : ScriptableObject
{
    [SerializeField] private GameObject[] prefabs;

    private static Prefabs instance;
    public static Prefabs Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Resources.Load<Prefabs>("Prefabs");
            }

            return instance;
        }
    }

    public static GameObject GetPrefabFromType<T>()
    {
        GameObject result = null;

        foreach(var prefab in Prefabs.Instance.prefabs)
        {
            if(prefab.GetComponent<T>() != null)
            {
                result = prefab;
                return result;
            }
        }

        Debug.LogError(typeof(T) + " ‚Í“o˜^‚³‚ê‚Ä‚¢‚È‚¢Prefab‚Å‚·");
        return null;
    }
}
