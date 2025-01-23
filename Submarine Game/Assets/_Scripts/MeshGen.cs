using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGen : MonoBehaviour
{
    [SerializeField] GameObject[] spawns;
    private List<GameObject> bubbleSpawners = new List<GameObject>();
    private List<Vector3> bubbleLocs = new List<Vector3>();

    private float bubbleTime;
    private bool spawnBubbles = true;

    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;

    [SerializeField] private int xSize, zSize; 

    private void Start(){
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update(){
        if(GameManager.Instance.GetPlayer() != null){
            UpdateMesh();
        }

        if(bubbleTime < 0){
            spawnBubbles = true;
            bubbleTime = 2f; 
        }
        bubbleTime -= Time.deltaTime;
    }

    private void CreateShape(){
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        Vector3 playerPos = GameManager.Instance.GetPlayer().transform.position;
        transform.position = new Vector3(playerPos.x - xSize / 2, 0, playerPos.z - zSize / 2);



        for (int i = 0, z = 0; z <= zSize; z++){
            for(int x = 0; x <= xSize; x++){
                float worldX = x + playerPos.x - (xSize / 2);
                float worldZ = z + playerPos.z - (zSize / 2);
                float y = Mathf.PerlinNoise(worldX * 0.3f, worldZ * 0.3f) * 2;

                Vector3 location = new Vector3(x, y, z); 

                vertices[i] = new Vector3(x, y, z);
                i++;

                if (y > 1.8f){
                    bubbleLocs.Add(location);
                }
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++){
            for (int x = 0; x < xSize; x++){
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
        bubbleLocs.Clear();

        CreateShape();

        mesh.vertices = vertices;
        mesh.triangles = triangles; 

        mesh.RecalculateNormals();

        if (spawnBubbles){
            if (bubbleLocs.Count > 40 || bubbleSpawners.Count > 50){
                Debug.LogError("too many particles");
                print(bubbleLocs.Count);
                return;
            }

            for (int x = 0; x < bubbleLocs.Count; x++){
                bool alreadyExists = false;

                bubbleSpawners.RemoveAll(item => item == null);
                foreach (GameObject bubbleSp in bubbleSpawners){
                    if (Vector3.Distance(bubbleSp.transform.position, transform.TransformPoint(bubbleLocs[x])) < 30){
                        alreadyExists = true;
                        break;
                    }
                }

                if (!alreadyExists || bubbleSpawners.Count < 10){
                    GameObject bubble = Instantiate(spawns[Random.Range(0,spawns.Length)], transform.TransformPoint(bubbleLocs[x]), transform.rotation);
                    bubbleSpawners.Add(bubble);
                }
            }

            spawnBubbles = false;
        }
    }
}
