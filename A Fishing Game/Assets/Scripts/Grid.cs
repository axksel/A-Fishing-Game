using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public int xSize,ySize;
    private Vector3[,] vertices;
    private Vector3[] vertices1d;
    private Mesh mesh;
    public GameObject boat;
    int tmpX;
    int tmpY;


    void Awake()
    {
        GenerateVertices();
        WaveVertices();
        Generate();
        LocateBoat();


    }
    private void FixedUpdate()
    {
        WaveVertices();
        Generate();
        SteerBoat();
        LocateBoat(tmpX,tmpY);
    }
  
    public void LocateBoat()
    {
        float longestDist = 1000000;


        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float dist = Vector3.Distance(boat.transform.position, vertices[x, y]);
                if (longestDist > dist)
                {
                    longestDist = dist;
                    tmpX = x;
                    tmpY = y;
                }
            }
        }
        Debug.Log(tmpY + "    "+ tmpX);

    }

    public void LocateBoat(int xIndex,int yIndex )
    {
        float longestDist = 1000000;


        for (int y = yIndex-1; y <= yIndex+1; y++)
        {
            for (int x = xIndex-1; x <= xIndex + 1; x++)
            {
                float dist = Vector3.Distance(boat.transform.position, vertices[x, y]);
                if (longestDist > dist)
                {
                    longestDist = dist;
                    tmpX = x;
                    tmpY = y;
                }
            }
        }
        Debug.Log(tmpY + "    " + tmpX);

    }



    public void SteerBoat()
    {
        float yTmp = boat.transform.rotation.eulerAngles.y;

        boat.transform.position =new Vector3(boat.transform.position.x, Vector3.Lerp(boat.transform.position, vertices[tmpX, tmpY] + new Vector3(0, 2, 0), 0.8f).y,boat.transform.position.z);
        boat.transform.rotation = Quaternion.Lerp(boat.transform.rotation,Quaternion.Euler(mesh.normals[tmpX + xSize * tmpY] *100), 0.8f);


        Vector3 tmp = boat.transform.localEulerAngles;
        tmp.y = yTmp;
        boat.transform.localEulerAngles = tmp;
    }

    private void WaveVertices()
    {

        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float yMod = Mathf.PerlinNoise(y*0.03f + Time.time, x*0.03f+Time.time) * 50f;
                vertices[y, x].y = yMod;

            }
        }
       
        ConvertVertices();

    }
    private void ConvertVertices()
    {
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices1d[i] = vertices[y, x];
            }
        }
        mesh.vertices = vertices1d;

    }


    private void GenerateVertices()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        vertices1d = new Vector3[(xSize + 1)* (ySize + 1)];
        vertices = new Vector3[(xSize + 1) , (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {

                vertices[y,x] = new Vector3(x*10, 0, y*10);

            }
        }
        ConvertVertices();
        mesh.vertices = vertices1d;
    }


private void Generate()
    {

        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {

                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                tangents[i] = tangent;
            }
        }
       
        mesh.uv = uv;
        mesh.tangents = tangents;

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices1d[i], 0.1f);
        }
    }
}
