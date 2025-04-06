using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    //public static PathGenerator Instance; // Singleton instance

    public GameObject Path;
    public GameObject PathVertex;
    public GameObject Ground;
    public GameObject emptyObject;
    public GameObject TacoBell;
    public GameObject Border;
    private int numberOfVertices; // Number of vertices in the path
    public List<GameObject> pathVertices = new List<GameObject>(); // List to store the path vertices
    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>(); // List to save the path's location
    public GameObject[] sceneryObjects;
    public GameObject[] sceneryBackground;


    void Start()
    {
        StartCoroutine(WaitForPathGenerator());
    }

    IEnumerator WaitForPathGenerator()
    {
        while (PathGenerator.Instance == null || PathGenerator.Instance.pathVertices.Count == 0)
        {
            Debug.Log("Waiting for PathGenerator to populate pathVertices...");
            yield return null; // Waits until next frame
        }

        // Grab the number of vertices from PathGenerator
        numberOfVertices = PathGenerator.Instance.numberOfVertices;
        pathVertices = PathGenerator.Instance.pathVertices;

        //Debug.Log("pathVertices populated. Proceeding with map generation.");
        GenerateMap();
    }

    void GenerateMap()
    {
        float xRange = numberOfVertices * 10 + 30;
        float zMinusRange = -60;
        float zPlusRange = 60;

        Vector3 lastVertex = pathVertices[pathVertices.Count - 1].transform.position;
        Vector3 firstVertex = pathVertices[0].transform.position;

        GameObject BorderInstance = Instantiate(Border);
        BorderInstance.transform.position = new Vector3(firstVertex.x, firstVertex.y, firstVertex.z+40);

        GameObject tacoBellInstance = Instantiate(TacoBell);
        tacoBellInstance.transform.position = new Vector3(lastVertex.x -10, lastVertex.y, lastVertex.z);
        float originalXRotation = TacoBell.transform.rotation.eulerAngles.x;
        tacoBellInstance.transform.rotation = Quaternion.Euler(originalXRotation, -90, 0);

        GameObject ground = Instantiate(Ground, new Vector3(xRange / 2, -0.1f, 0f), Quaternion.identity);
        ground.transform.localScale = new Vector3(xRange, 0.1f, 2*zPlusRange);

        // Llenamos occupiedPositions con las posiciones de los caminos
        FillOccupiedPositions();

        for (float i = 5; i < xRange-20; i+=5)
        {
            for (float j = zMinusRange+10; j <= zPlusRange-10; j+=5)
            {
                Vector3 position = new Vector3(i, 0f, j);

                if (!occupiedPositions.Contains(position))
                {
                    GameObject placer = Instantiate(emptyObject, position, Quaternion.identity);
                    placer.GetComponent<Renderer>().enabled = false;
                }
            }
        }

        // Instantiate scenary elements
        HashSet<Vector3> usedPositions = new HashSet<Vector3>();
        int totalSceneryObjects = numberOfVertices*8;
        int placedObjects = 0;

        while (placedObjects < totalSceneryObjects)
        {
            float x = Random.Range(0, xRange);
            float z = Random.Range(zMinusRange, zPlusRange);
            Vector3 position = new Vector3(x, 0f, z);

            if (!occupiedPositions.Contains(position) && x % 5 != 0 && z % 5 != 0 && !usedPositions.Contains(position))
            {
                int index = Random.Range(0, sceneryObjects.Length);
                GameObject scenery = Instantiate(sceneryObjects[index], position, Quaternion.identity);
                scenery.transform.localScale *= 1.2f;
                if (index != 0)
                {
                    scenery.transform.rotation = Quaternion.Euler(270, 0, 0);
                }
                else
                {
                    scenery.transform.position = new Vector3(x, 0.85f, z);
                }
                usedPositions.Add(position);
                placedObjects++;
            }
        }

        float totalBackgroundObjects = xRange/50;
        int placedBackgroundObjects = 0;
        float X = 20;
        while (placedBackgroundObjects < totalBackgroundObjects)
        {
            Vector3 positionLeft = new Vector3(X, 0f, zMinusRange-10);
            Vector3 positionRight = new Vector3(X, 0f, zPlusRange+10);
            int index = Random.Range(0, sceneryBackground.Length);
            GameObject mountainL = Instantiate(sceneryBackground[index], positionLeft, Quaternion.identity);
            GameObject mountainR = Instantiate(sceneryBackground[index], positionRight, Quaternion.identity);
            if (index == 1 || index == 0)
            {
                mountainL.transform.position = new Vector3(X, 0f, zMinusRange - 20);
                mountainR.transform.position = new Vector3(X, 0f, zPlusRange + 20);
            }   
            X += 50;
            placedBackgroundObjects++;
            Debug.Log("Mountain instantiated in: " + positionLeft + " and " + positionRight);
        }
    }
    void FillOccupiedPositions()
    {
        occupiedPositions.Clear();

        for (int i = 0; i < pathVertices.Count - 1; i++)
        {
            Vector3 start = pathVertices[i].transform.position;
            Vector3 end = pathVertices[i + 1].transform.position;

            Vector3 direction = (end - start).normalized;
            float distance = Vector3.Distance(start, end);

            // Agregamos los puntos intermedios cada 5 unidades
            for (float d = 0; d <= distance; d += 5)
            {
                Vector3 position = start + direction * d;
                occupiedPositions.Add(position);
            }
        }
    }


}
