using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class ObjectPool : MonoBehaviour
{
    // Start is called before the first frame update

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    #region  Singelton
    public static ObjectPool Instance;
    private void Awake() 
    {
        Instance = this;
    }
    #endregion 


    private void Start() 
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        } 
    }

    public GameObject SpawnFromPool(string tag, Vector2 position)
    {
        
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Pool with tag"+ tag + "doesn't exsist!");
            return null; 
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
      
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
          

        IPoolObject pooledObj= objectToSpawn.GetComponent<IPoolObject>();

        if(pooledObj != null )
        {   
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);
        
        return objectToSpawn;
    }

}