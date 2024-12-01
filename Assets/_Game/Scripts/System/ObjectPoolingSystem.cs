
using UnityEngine;

public class ObjectPoolingSystem : MonoBehaviour
{
    public static GameObject PoolingObject(Transform parent, GameObject objectPrefab)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.activeInHierarchy)
            {
                child.gameObject.SetActive(true);
                return child.gameObject;
            }
        }
        return Instantiate(objectPrefab,parent);
    }
}
