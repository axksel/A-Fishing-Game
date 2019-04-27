using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LureScript : MonoBehaviour
{

    public List<GameObject> hookedFishs = new List<GameObject>();


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Fish")
        {
            if(hookedFishs.Count == 0)
            {
                GameObject fish = collision.gameObject;
                fish.GetComponent<FishAI>().lurePos = transform;
                fish.GetComponent<FishAI>().isHooked();
                hookedFishs.Add(fish);
                fish.GetComponent<SphereCollider>().enabled = true;
            }
        }
    }
}
