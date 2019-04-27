using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isbjergcode : MonoBehaviour
{

    public GameObject boat;

    void Update()
    {
        transform.position += -boat.transform.forward*0.25f;
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag=="Player")
            Debug.Log("game over");
        
    }
}
