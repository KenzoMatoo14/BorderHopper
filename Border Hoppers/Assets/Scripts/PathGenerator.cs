using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public static PathGenerator Instance; // Singleton instance
        
    public GameObject Path;
    public GameObject PathVertex;
    public int numberOfVertices = 8; // Number of vertices in the path
    private float spacing = 5; // Distance between each vertex
    public List<GameObject> pathVertices = new List<GameObject>(); // List to store the path vertices

    void Awake()
    {
        // Ensure there is only one instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

        if(numberOfVertices % 2 != 0)
        {
            numberOfVertices += 1;
        }

        GeneratePath(); 
    }

    void GeneratePath()
    {
        Vector3 currentPosition = Vector3.zero;
        Vector3 newPosition = Vector3.zero;
        Vector3 currentPathPosition = Vector3.zero;
        Vector3 newPathPosition = Vector3.zero;

        for (int i = 0; i < numberOfVertices; i++)
        {
            GameObject vertex = Instantiate(PathVertex, currentPosition, Quaternion.identity);
            
            pathVertices.Add(vertex);

            float distanceX = i * spacing * 2;
            float distanceZ = Random.Range(-5, 6) * 5; // Random multiple of 5 between -25 and 25

            if (i % 2 == 0 && i != 0) // Si es par
            {
                newPosition = new Vector3(currentPosition.x, 0, distanceZ);
            }
            else // Si es impar
            {
                newPosition = new Vector3(distanceX, 0, currentPosition.z);
            }


            vertex.transform.position = newPosition;
            vertex.transform.parent = transform;

            // Generar caminos en intervalos de 5 en 5
            GeneratePathBetween(currentPosition, newPosition);

            currentPosition = newPosition; // Actualizar la posici�n actual

        }
        void GeneratePathBetween(Vector3 start, Vector3 end)
        {
            Vector3 direction = (end - start).normalized; // Direcci�n hacia el siguiente v�rtice
            float distance = Vector3.Distance(start, end); // Distancia total

            for (float d = 5; d <= distance; d += 5) // Coloca Path en intervalos de 5
            {
                Vector3 position = start + direction * d;
                GameObject path = Instantiate(Path, position, Quaternion.identity);
                path.transform.parent = transform;
            }
        }
    }

}