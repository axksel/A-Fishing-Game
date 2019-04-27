using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForce : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> decos = new List<GameObject>();
    int force = 750;
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            decos.Add(gameObject.transform.GetChild(i).gameObject);
        }
        StartCoroutine(randForce());
    }

    public IEnumerator randForce()
    {
        float x = Random.Range(-force, force);
        float y = Random.Range(-force, force);
        float z = Random.Range(-force, force);
        for (int i = 0; i < decos.Count; i++)
        {
            decos[i].GetComponent<Rigidbody>().AddForce(new Vector3(x, y, z));
        }

        yield return new WaitForSeconds(Random.Range(2.5f,4f));

        StartCoroutine(randForce());
    }
}
