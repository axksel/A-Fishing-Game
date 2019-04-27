using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isbjergSpawner : MonoBehaviour
{

    public int timeBetweenSpawns = 20;
    public float timer;
    public int timeBetweenSpawns2 = 10;
    public float timer2=3;
    public GameObject isbjerg;
    public GameObject boat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer=timer+Time.deltaTime+Random.Range(0,1);
        timer2 = timer2 + Time.deltaTime + Random.Range(0, 1);

        if (timer >timeBetweenSpawns)
        {
          GameObject tmp = Instantiate(isbjerg,new Vector3(Random.Range(-250,250),0, 250),Quaternion.identity);
            GameObject tmp2 = Instantiate(isbjerg, new Vector3(Random.Range(-250, 250), 0, -250), Quaternion.identity);
            GameObject tmp3 = Instantiate(isbjerg, new Vector3(-250, 0, Random.Range(-250, 250)), Quaternion.identity);
            GameObject tmp4 = Instantiate(isbjerg, new Vector3(250, 0, Random.Range(-250, 250)), Quaternion.identity);
            GameObject tmp5 = Instantiate(isbjerg, boat.transform.position+boat.transform.forward*250, Quaternion.identity);

            tmp2.GetComponent<isbjergcode>().boat = boat;
            tmp.GetComponent<isbjergcode>().boat = boat;
            tmp3.GetComponent<isbjergcode>().boat = boat;
            tmp4.GetComponent<isbjergcode>().boat = boat;
            tmp5.GetComponent<isbjergcode>().boat = boat;

            timer = 0;
        }


        if (timer2> timeBetweenSpawns2)
        {

            GameObject tmp5 = Instantiate(isbjerg, boat.transform.position + boat.transform.forward * 250, Quaternion.identity);

           
            tmp5.GetComponent<isbjergcode>().boat = boat;

            timer2 = 0;
        }

    }
}
