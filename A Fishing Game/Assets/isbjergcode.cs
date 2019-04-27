using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isbjergcode : MonoBehaviour
{

    public GameObject boat;

    void Update()
    {
        transform.position += -boat.transform.forward*0.5f;
    }

    void OnCollisionEnter(Collision collision)
    {

            Debug.Log("game over");
        
    }
}
