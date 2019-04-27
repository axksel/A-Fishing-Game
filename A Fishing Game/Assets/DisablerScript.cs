using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablerScript : MonoBehaviour
{

    public GameObject fiskeStang;
    public GameObject grid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {

        if(collision.gameObject.tag=="Player")
        {

            fiskeStang.SetActive(false);
        grid.SetActive(false);
        }

    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            fiskeStang.SetActive(true);
            grid.SetActive(true);
        }
    }
}
