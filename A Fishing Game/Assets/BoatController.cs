using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    GameObject ror;
    // Update is called once per frame

    void Start()
    {
         ror = GameObject.FindGameObjectWithTag("Ror");

    }
    void Update()
    {


        if(Input.GetKey(KeyCode.A))
        {
            ror.transform.Rotate(0, -2, 0, Space.Self);

            transform.Rotate(Vector3.up,-5f);

        }
        if (Input.GetKey(KeyCode.D))
        {
            ror.transform.Rotate(0, 2, 0, Space.Self);

            transform.Rotate(Vector3.up, 5f);

        }
    }
}
