using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{

    Particle[] particles = new Particle[25];
    LineRenderer lineRenderer;
    public GameObject topOfFishingLine;
    public GameObject cube;
    bool hitWater = false;
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
        }

    }

    void FixedUpdate()
    {
        for (int i = 1; i < particles.Length; i++)
        {
            particles[i].Acceleration = new Vector3(0, acceleration, 0);
            Verlet(particles[i], dt);
        }
         
        particles[1].Pos = topOfFishingLine.transform.position;
        if (hitWater)
        {
            particles[particles.Length - 1].Pos = cube.transform.position;
            lineLength -= 0.005f;
            lineLength = Mathf.Clamp(lineLength, 0, 1);
            if (Vector3.Distance(cube.transform.position, lineRenderer.GetPosition(particles.Length - 1)) > 1)
            {
                cube.transform.position = Vector3.Lerp(cube.transform.position, lineRenderer.GetPosition(particles.Length - 1), 0.5f);
                cube.transform.position -= new Vector3(0, cube.transform.position.y + 2.5f, 0);
            }
            if(lineLength < 0.005)
            {
                hitWater = false;
            }
        }
        
        for (int j = 0; j < 10; j++)
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

        if(lineRenderer.GetPosition(particles.Length - 1).y < -2 && !hitWater)
        {
            cube.transform.position = lineRenderer.GetPosition(particles.Length - 1);
            hitWater = true;
            throwStarted = false;
        }
        if (throwStarted)
        {
            lineLength += 0.005f;
            lineLength = Mathf.Clamp(lineLength, 0, 1);
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

    public void Test()
    {
        Debug.Log("HAhaha");
    }
}
