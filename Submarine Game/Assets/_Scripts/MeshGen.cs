using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGen : MonoBehaviour
{
    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;

    [SerializeField] private int xSize, zSize;

    private void Start(){
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update(){
        UpdateMesh();
    }

    private void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        Vector3 playerPos = GameManager.Instance.GetPlayer().transform.position;
        transform.position = new Vector3(playerPos.x - xSize / 2, 0, playerPos.z - zSize / 2);

        for (int i = 0, z = 0; z <= zSize; z++){
            for(int x = 0; x <= xSize; x++){
                float worldX = x + playerPos.x - (xSize / 2);
                float worldZ = z + playerPos.z - (zSize / 2);
                float y = Mathf.PerlinNoise(worldX * 0.3f, worldZ * 0.3f) * 2;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris] = vert;
                triangles[tris + 1] = triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 2] = triangles[tris + 3] = vert + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }


    }

    public void UpdateMesh(){
        mesh.Clear();

        CreateShape();

        mesh.vertices = vertices;
        mesh.triangles = triangles; 

        mesh.RecalculateNormals();
    }
}
