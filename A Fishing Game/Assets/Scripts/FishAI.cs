using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    [SerializeField]
    float maxYPos, minYPos, maxXPos, minXPos, maxZPos, minZPos;



    private float startTime;
    public float startSpeed = 5f;
    public float currentSpeed = 5f;
    public float burstSpeed = 0.01f;
    Rigidbody rb;
    Vector3 EAV;
    bool turning = false;
    bool hooked = false;
    public Transform lurePos;
    float t;
    float timeToReachTarget = 3f;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        transform.Rotate(new Vector3(Random.Range(-20, 20), 180, 0));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hooked)
        {
            Move();
        }
        else
        {
            GoToLure();
        }


    }

    public void GoToLure()
    {
        t += Time.deltaTime / timeToReachTarget;
        transform.position = Vector3.Lerp(transform.position, lurePos.position, t);
    }

    public void isHooked()
    {
        hooked = true;
    }


    void Move()
    {
        rb.velocity = -transform.forward * currentSpeed;
        turnScript();
        if (turning == false)
        {
            if (Random.Range(0f, 1f) < 0.02)
            {
                StartCoroutine(burstOfSpeed());
            }

        }


    }

    void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.tag == "Boat")
        {
            rb.AddForce((transform.position - col.gameObject.transform.position) * 200);
        }
    }

    IEnumerator burstOfSpeed()
    {
        currentSpeed += 5;
        yield return new WaitForSeconds(0.2f);
        currentSpeed = startSpeed;
    }

    void Turn(Vector3 turnVector)
    {
        Quaternion deltaRotation = Quaternion.Euler(turnVector * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
        turning = true;
    }

    void turnScript()
    {
        if (transform.position.x >= maxXPos || transform.position.z >= maxZPos)
        {
            EAV = new Vector3(0, 100, 0);

            Turn(EAV);
        }
        else if (transform.position.x <= minXPos || transform.position.z <= minZPos)
        {
            EAV = new Vector3(0, 100, 0);
            Turn(EAV);
        }
        else
        {
            EAV = new Vector3(0, 0, 0);
            turning = false;
        }

        if (transform.position.y > maxYPos)
        {
            Turn(new Vector3(Random.Range(-20, -10), 0, 0));
        }
        else if (transform.position.y < minYPos)
        {
            Turn(new Vector3(Random.Range(10, 20), 0, 0));
        }
        else { turning = false; }

    }


    Vector3 UpOrDown()
    {
        if (transform.position.y > minYPos)
        {
            return new Vector3(Random.Range(-50, 0), 0, 0);
        }
        else
        {
            return new Vector3(Random.Range(0, 50), 0, 0);
        }
    }

    Vector3 rightOrLeft()
    {
        if (Random.Range(0f, 1f) > 0.5)
        {
            return new Vector3(0, Random.Range(-10, 0), 0);
        }
        else
        {
            return new Vector3(0, Random.Range(0, 10), 0);
        }
    }

}