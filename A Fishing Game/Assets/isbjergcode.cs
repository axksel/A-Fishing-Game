using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class isbjergcode : MonoBehaviour
{

    public GameObject boat;

    void Update()
    {
        transform.position += -boat.transform.forward*0.25f;
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag=="Boat")
        {
            SceneManager.LoadScene("StartMenu");
            SceneManager.UnloadSceneAsync("ToebsLuksusScene");

        }

       

    }
}
