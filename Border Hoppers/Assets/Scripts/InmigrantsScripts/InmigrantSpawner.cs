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
            // Randomly select an immigrant type
            int randomIndex = Random.Range(0, inmigrantPrefabs.Length);
            GameObject selectedPrefab = inmigrantPrefabs[randomIndex];

            // Spawn the selected immigrant
            GameObject newInmigrant = Instantiate(selectedPrefab, transform.position, Quaternion.identity);

            Debug.Log("Enemy spawned after " + currentTime + " seconds");

            spawnedCount++; // Increase the count

            currentTime = Random.Range(0.5f, 3f); // Set next spawn time randomly
        }
    }
}
