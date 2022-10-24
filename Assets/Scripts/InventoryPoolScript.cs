using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectsPool : MonoBehaviour
{
    //[SerializeField] private Pool playerProjectilesPool;
    //[SerializeField] private Pool enemyProjectilesPool;

    [SerializeField] private List<Pool> pools = new List<Pool>();

    //private Queue<GameObject> playerProjectileQueue = new Queue<GameObject>();
    //private Queue<GameObject> enemyProjectileQueue = new Queue<GameObject>();

    public Dictionary<string, Queue<GameObject>> poolsDictionary = new Dictionary<string, Queue<GameObject>>();

    public static ObjectsPool Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);

                objectQueue.Enqueue(obj);
            }

            poolsDictionary.Add(pool.tag, objectQueue);
        }
    }




    /*
    private void InitializePools()
    {
        for (int i=0; i < playerProjectilesPool.size; i++)
        {
            GameObject obj = Instantiate(playerProjectilesPool.prefab, transform);
            obj.SetActive(false);

            playerProjectileQueue.Enqueue(obj);
        }

        for (int i = 0; i < enemyProjectilesPool.size; i++)
        {
            GameObject obj = Instantiate(enemyProjectilesPool.prefab, transform);
            obj.SetActive(false);

            enemyProjectileQueue.Enqueue(obj);
        }
    }
    */

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolsDictionary.ContainsKey(tag)) { return null; }

        GameObject objectToSpawn = poolsDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolsDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    /*
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject objectToSpawn;

        if (tag == "PlayerProjectiles")
        {
            objectToSpawn = playerProjectileQueue.Dequeue();
        }
        else if (tag == "EnemyProjectiles")
        {
            objectToSpawn = enemyProjectileQueue.Dequeue();
        }
        else
        {
            return null;
        }

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        if (tag == "PlayerProjectiles")
        {
            playerProjectileQueue.Enqueue(objectToSpawn);
        }
        else if (tag == "EnemyProjectiles")
        {
            enemyProjectileQueue.Enqueue(objectToSpawn);
        }

        return objectToSpawn;
    }
    */

    public void DespawnToPool(GameObject obj, float timeDelay)
    {
        StartCoroutine(DespawnCoroutine(obj, timeDelay));
    }

    private IEnumerator DespawnCoroutine(GameObject obj, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        obj.transform.position = transform.position;
        obj.SetActive(false);
    }
}

[Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}
