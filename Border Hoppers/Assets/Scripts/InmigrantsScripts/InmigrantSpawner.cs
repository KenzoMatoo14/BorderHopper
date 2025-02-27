using UnityEngine;

public class InmigrantSpawner : MonoBehaviour
{
    public static InmigrantSpawner spawner;

    public GameObject Inmigrant;
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
            // Spawn a new immigrant
            GameObject c = Instantiate(Inmigrant);
            c.transform.position = transform.position;

            Debug.Log("Enemy spawned after " + currentTime + " seconds");

            spawnedCount++; // Increase the count

            currentTime = Random.Range(0.5f, 3f); // Set next spawn time randomly
        }
    }
}
