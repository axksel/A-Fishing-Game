﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {


        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up,-5f);

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, 5f);

        }
    }
}
