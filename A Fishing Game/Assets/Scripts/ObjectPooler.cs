using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler: MonoBehaviour

{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    // Start is called before the first frame update

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
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

        StartCoroutine(spawn("Fish", transform.position, Quaternion.identity));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawn(string tag, Vector3 pos, Quaternion rot)
    {
        SpawnFromPool("Fish", transform.position, Quaternion.identity).transform.Rotate(new Vector3(0,Random.Range(-90,90),0));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(spawn("Fish", transform.position, Quaternion.identity));
    }

    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Pool tag error");
            return null;
        }
        GameObject objspawn = poolDictionary[tag].Dequeue();
        objspawn.SetActive(true);
        objspawn.transform.position = pos;
        objspawn.transform.rotation = rot;

        poolDictionary[tag].Enqueue(objspawn);

        return objspawn;
    }
}
