using System.Collections.Generic;
using UnityEngine;

public enum PoolingTag
{
    BallHit_R, BallHit_B, PinHit_B, BallHit_Trail
}

[System.Serializable]
public class Pool
{
    public PoolingTag effectTag;
    public GameObject prefab;
    public int preSpawnSize;
}

public class PoolingManager : MonoBehaviour
{
    public List<Pool> pools;
    private Dictionary<PoolingTag, Queue<GameObject>> poolDictionary;
    private Dictionary<PoolingTag, GameObject> prefabDictionary;
    private Dictionary<PoolingTag, Transform> parentTransforms;

    public static PoolingManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        poolDictionary = new Dictionary<PoolingTag, Queue<GameObject>>(pools.Count);
        prefabDictionary = new Dictionary<PoolingTag, GameObject>(pools.Count);
        parentTransforms = new Dictionary<PoolingTag, Transform>(pools.Count);

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            prefabDictionary[pool.effectTag] = pool.prefab;


            GameObject parentObject = new GameObject(pool.effectTag.ToString());
            parentObject.transform.parent = this.transform;
            parentTransforms[pool.effectTag] = parentObject.transform;

            for (int i = 0; i < pool.preSpawnSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab, parentObject.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.effectTag, objectPool);
        }
    }

    public GameObject GetFromPool(PoolingTag effectTag, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (!poolDictionary.ContainsKey(effectTag))
        {
            return null;
        }

        Queue<GameObject> queue = poolDictionary[effectTag];
        GameObject objectToSpawn;

        if (queue.Count == 0)
        {
            objectToSpawn = Instantiate(prefabDictionary[effectTag], position, rotation, parentTransforms[effectTag]);
        }
        else
        {
            objectToSpawn = queue.Dequeue();
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            objectToSpawn.SetActive(true);
        }

        objectToSpawn.GetComponent<PoolingTarget>().Init();

        if (parent != null)
        {
            objectToSpawn.transform.SetParent(parent);
        }

        return objectToSpawn;
    }

    public void ReturnToPool(PoolingTag effectTag, GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
        objectToReturn.transform.SetParent(parentTransforms[effectTag]);
        poolDictionary[effectTag].Enqueue(objectToReturn);
    }
}
