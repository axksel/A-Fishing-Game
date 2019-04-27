using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{

    Particle[] particles = new Particle[25];
    LineRenderer lineRenderer;
    public GameObject topOfFishingLine;
    public GameObject cube;
    public Grid grid;
    public GameObject gm;
    public GameObject player;
    Vector2 closestPoint;
    Vector3 tmpHit;

    bool hitWater = false;
    bool doOnce = true;
    public bool throwing = false;
    public bool throwStarted = false;

    [Range(0f, 1f)]
    public float lineLength = 0.1f;
    [Range(-100f, 0f)]
    public float acceleration = -5f;
    [Range(0, 0.1f)]
    public float dt = 0.1f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = particles.Length;
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i] = new Particle();
            particles[i].Acceleration = new Vector3(0, acceleration, 0);
        }

    }

    void LateUpdate()
    {
        if (throwing)
        {
            for (int i = 1; i < particles.Length; i++)
            {
                Verlet(particles[i], dt);
            }

            particles[1].Pos = topOfFishingLine.transform.position;

            for (int j = 0; j < 5; j++)
            {
                for (int i = 1; i < particles.Length - 1; i++)
                {
                    PoleConstraint(particles[i + 1], particles[i], lineLength);
                }
            }

            for (int i = 1; i < particles.Length; i++)
            {
                lineRenderer.SetPosition(i, particles[i].Pos);
            }
            lineRenderer.SetPosition(0, topOfFishingLine.transform.position);


            if (hitWater)
            {
                cube.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                //cube.transform.position = new Vector3(cube.transform.position.x, grid.vertices[(int)closestPoint.x, (int)closestPoint.y].y - gm.transform.position.y, cube.transform.position.z);
                cube.transform.position = Vector3.Lerp(tmpHit, new Vector3(topOfFishingLine.transform.position.x, cube.transform.position.y, topOfFishingLine.transform.position.z), 1 - lineLength);

                float yMod = (Mathf.PerlinNoise(Time.time, 1));
                cube.transform.position += new Vector3(0, yMod, 0);

                particles[particles.Length - 1].Pos = cube.transform.position;


                if (lineLength < 0.005)
                {
                    cube.SetActive(false);
                    hitWater = false;
                    throwing = false;
                    lineRenderer.enabled = false;
                    doOnce = true;
                    transferFishes();

                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0f) // forward
                {
                    lineLength -= 0.01f;
                    lineLength = Mathf.Clamp(lineLength, 0, 1);

                }
                if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
                {
                    lineLength += 0.01f;
                    lineLength = Mathf.Clamp(lineLength, 0, 1);

                }
            }





            if (throwStarted)
            {
                cube.SetActive(true);
                if (!doOnce)
                {
                    closestPoint = FindClosestVert(grid.xSize / 2, grid.ySize / 2);
                    lineLength += 0.5f;
                    lineLength = Mathf.Clamp(lineLength, 0, 1);

                    if (lineRenderer.GetPosition(particles.Length - 1).y < grid.vertices[(int)closestPoint.x, (int)closestPoint.y].y && !hitWater)
                    {
                        tmpHit = lineRenderer.GetPosition(particles.Length - 1);
                        cube.transform.position = lineRenderer.GetPosition(particles.Length - 1);
                        hitWater = true;
                        throwStarted = false;
                        doOnce = true;
                    }
                    particles[particles.Length - 1].Pos = cube.transform.position;

                }

                if (doOnce)
                {
                    for (int i = 0; i < particles.Length; i++)
                    {
                        lineRenderer.SetPosition(i, topOfFishingLine.transform.position);
                    }

                    cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    cube.transform.position = player.transform.position + Vector3.up * 2;
                    doOnce = false;
                    cube.GetComponent<Rigidbody>().AddForce(player.transform.forward * 500);
                    particles[particles.Length - 1].Pos = cube.transform.position;
                }
            }
        }
    }


    void transferFishes()
    {
        foreach (GameObject f in cube.GetComponent<LureScript>().hookedFishs)
        {
            f.gameObject.transform.position = GameObject.FindGameObjectWithTag("FishStorage").transform.position;
            Destroy(f.GetComponent<FishAI>());
            f.GetComponent<Rigidbody>().useGravity = true;
        }
    }



    private void Verlet(Particle p, float dt)
    {
        Vector3 temp = p.Pos;
        p.Pos += p.Pos - p.OldPos + (p.Acceleration * dt * dt);
        p.OldPos = temp;
    }

    private void PoleConstraint(Particle p1, Particle p2, float restLength)
    {
        Vector3 delta = p2.Pos - p1.Pos;

        float deltaLength = delta.magnitude;

        float diff = (deltaLength - restLength) / deltaLength;

        p1.Pos += delta * diff * 0.5f;
        p2.Pos -= delta * diff * 0.5f;
    }

    public Vector2 FindClosestVert(int xIndex, int yIndex)
    {
        float longestDist = 1000000;
        int tmpX = 0;
        int tmpY = 0;

        for (int y = yIndex - 2; y <= yIndex + 1; y++)
        {
            for (int x = xIndex - 2; x <= xIndex + 1; x++)
            {
                float dist = Vector3.Distance(lineRenderer.GetPosition(particles.Length - 1), grid.vertices[x, y]);
                if (longestDist > dist)
                {
                    longestDist = dist;
                    tmpX = x;
                    tmpY = y;
                }
            }
        }
        return new Vector2(tmpX, tmpY);
    }
}
