using UnityEngine;

public class Guineo : MonoBehaviour
{
    public GameObject guineoPrefab; // Assign the Guineo prefab in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monke")) // Make sure the Monke has the "Monke" tag
        {
            GuineoManager.Instance.AddGuineo(); // Update the UI counter
            SpawnNewGuineo();
            Destroy(gameObject); // Destroy the current Guineo
        }
    }

    void SpawnNewGuineo()
    {
        float randomX = Random.Range(-5f, 5f);
        float randomZ = Random.Range(-5f, 5f);
        Vector3 spawnPosition = new Vector3(randomX, 1f, randomZ);
        Quaternion spawnRotation = Quaternion.Euler(0, 180, 0);

        Instantiate(guineoPrefab, spawnPosition, spawnRotation);
    }
}
