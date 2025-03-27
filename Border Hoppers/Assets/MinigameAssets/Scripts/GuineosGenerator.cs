using UnityEngine;

public class GuineosGenerator : MonoBehaviour
{
    public GameObject guineoPrefab;
    void Start()
    {
        SpawnGuineo();
    }

    void SpawnGuineo()
    {
        float randomX = Random.Range(-5f, 5f);
        float randomZ = Random.Range(-5f, 5f);
        Vector3 spawnPosition = new Vector3(randomX, 1f, randomZ); // Adjust Y if needed
        Quaternion spawnRotation = Quaternion.Euler(0, 180, 0);

        Instantiate(guineoPrefab, spawnPosition, spawnRotation);
    }
}
