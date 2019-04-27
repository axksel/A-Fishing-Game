using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public int xSize,ySize;
    private Vector3[,] vertices;
    private Vector3[] vertices1d;
    private Mesh mesh;

    Vector3 rota;


    private float sizeModifier=10;
    int tmpX;
    int tmpY;


    void Awake()
    {

        GenerateVertices();
        WaveVertices();
        Generate();
      


    }
    private void FixedUpdate()
    {
        WaveVertices();
        Generate();
        SteerBoat();

    }
  


   


    public void SteerBoat()
    {
        Quaternion _facing = transform.rotation;
        Quaternion rot;

        transform.position =new Vector3(transform.position.x, -vertices[xSize/2,ySize/2].y+5,transform.position.z);
        rota = Vector3.Cross((vertices[xSize / 2, ySize / 2] - vertices[xSize / 2 + 1, ySize / 2]), (vertices[xSize / 2, ySize / 2] - vertices[xSize / 2, ySize / 2 + 1]));

        rot = Quaternion.LookRotation(mesh.normals[(xSize + 1) * (ySize + 1) / 2]);
        transform.up = rota;
        rot *= _facing;
        //transform.rotation = rot;



    }

    private void WaveVertices()
    {

        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float yMod = Mathf.PerlinNoise(y*0.03f + Time.time, x*0.03f+Time.time) * 10f;
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

                vertices[y,x] = new Vector3((x* sizeModifier) - xSize / 2 * sizeModifier, 0, (y* sizeModifier) - ySize / 2 * sizeModifier);

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

        Gizmos.DrawLine(vertices[xSize / 2, ySize / 2], vertices[xSize / 2, ySize / 2] + rota);
    }
}
