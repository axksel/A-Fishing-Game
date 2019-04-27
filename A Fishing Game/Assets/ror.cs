using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ror : MonoBehaviour
{

    public bool enableRor = false;
    public PlayerController fiskeScript;
    public BoatController skibsScript;
    public GameObject playerCharacter;
    public bool inSailing = false;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



        if (enableRor && Input.GetKeyDown(KeyCode.E))
        {

            fiskeScript.enabled = false;
            skibsScript.enabled = true;
            text.text = "Press E to stop steering";
            playerCharacter.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            inSailing = true;
            enableRor = false;

        }

        else if (Input.GetKeyDown(KeyCode.E) && inSailing)
        {
            fiskeScript.enabled = true;
            skibsScript.enabled = false;
            playerCharacter.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            inSailing = false;
            enableRor = true;
        }

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            enableRor = true;
            text.text = "Press E to steer Boat";
        }

    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enableRor = false;
            text.text = "Press E to Fish";
        }

    }
}