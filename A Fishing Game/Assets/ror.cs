using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ror : MonoBehaviour
{

    public bool enableRor =false;
    public PlayerController fiskeScript;
    public BoatController skibsScript;
    public GameObject playerCharacter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        if(enableRor && Input.GetKeyDown(KeyCode.E))
        {
            fiskeScript.enabled = false;
            skibsScript.enabled = true;
            playerCharacter.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            enableRor = false;
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            fiskeScript.enabled = true;
            skibsScript.enabled = false;
            playerCharacter.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        }

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("lolololo");
            enableRor = true;
        }

    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enableRor = false;
        }

    }
}
