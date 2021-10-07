using System.Collections.Generic;
using UnityEngine;

namespace Training.Patterns.ObjectPooling
{
[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}

public class CubePooler : MonoBehaviour
{
    public List<Pool> poolList;
    public Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    private void Start()
    {
        foreach (var pool in poolList)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objPool);
        }
    }

    #region Singleton
    
    public static CubePooler _instance;

    private void Awake()
    {
        _instance = this;
    }

    #endregion

    public GameObject SpawnGameObjectFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("There is not tag -- " + tag + " --  in dictionary");
            return null;
        }

        var objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;
        objToSpawn.SetActive(true);
        
        IPooledObject iPooledObj = objToSpawn.GetComponent<IPooledObject>();
        if(iPooledObj != null) iPooledObj.OnObjectSpawn();
        
        poolDictionary[tag].Enqueue(objToSpawn);
        return  objToSpawn;
    }

}
}
