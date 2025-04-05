using UnityEngine;

public class InmigrantSpawner : MonoBehaviour
{
    public static InmigrantSpawner spawner;

    public GameObject[] inmigrantPrefabs; // Array of different immigrant prefabs
    public int NumberInmigrant = 10;
    private int spawnedCount = 0; // Tracks how many have been spawned
    private float currentTime; // Time to next spawn

    void Awake()
    {
        if (spawner == null)
            spawner = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        currentTime = Random.Range(0.5f, 3f); // Set initial random time
    }

    public int getNumberInmigrants()
    {
        return NumberInmigrant;
    }

    void Update()
    {
        if (spawnedCount >= NumberInmigrant)
            return;

        // Countdown the timer
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            if (spawnedCount >= NumberInmigrant) return;

            // Ensure PathGenerator exists and has path vertices
            if (PathGenerator.Instance == null || PathGenerator.Instance.pathVertices.Count == 0)
            {
                Debug.LogWarning("PathGenerator is not ready or has no vertices!");
                return;
            }

            // Get the first vertex position
            Vector3 pathPos = PathGenerator.Instance.pathVertices[0].transform.position;
            Vector3 spawnPosition = new Vector3(pathPos.x + 1f, -0.3f, pathPos.z); //The Mexican model has a little gap of 0.3

            // Randomly select an immigrant type
            int randomIndex = Random.Range(0, inmigrantPrefabs.Length);
            GameObject selectedPrefab = inmigrantPrefabs[randomIndex];

            // Spawn the selected immigrant
            GameObject newInmigrant = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

            //Debug.Log("Enemy spawned after " + currentTime + " seconds");

            spawnedCount++; // Increase the count

            currentTime = Random.Range(0.5f, 3f); // Set next spawn time randomly
        }
    }
}
