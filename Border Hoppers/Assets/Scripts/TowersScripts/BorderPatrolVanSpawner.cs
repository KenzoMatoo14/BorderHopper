using System.Collections.Generic;
using UnityEngine;

public class BorderPatrolVanSpawner : Tower
{
    // Constructor
    public BorderPatrolVanSpawner()
    {
        damage = 20;
        range = 1f;
        cost = 5;
        firerate = 0f;
        prioritizeBlackMan = false;
    }

    public static BorderPatrolVanSpawner Instance;
    public GameObject borderPatrolVanPrefab; // Prefab for the van
    private int spawnedCount = 0; // Count of spawned vans
    private float spawnTimer = 0f; // Time left for next spawn
    public float spawnRate = 20; // How often a van spawns

    private List<GameObject> pathVertices; // Waypoints from PathGenerator
    void Start()
    {
        pathVertices = PathGenerator.Instance.pathVertices; // Get waypoints
        if (pathVertices.Count > 0)
        {
            SpawnVan(); // Spawn one immediately
            spawnTimer = spawnRate; // Reset timer to 20s for next spawn
        }
    }

    void Update()
    {
        if (pathVertices.Count == 0)
            return;

        // Countdown spawn timer
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            SpawnVan();
            spawnTimer = spawnRate; // Reset spawn timer
        }
    }

    void SpawnVan()
    {
        // Get last waypoint position
        Vector3 spawnPosition = pathVertices[pathVertices.Count - 1].transform.position;

        // Spawn the van at the last waypoint
        Instantiate(borderPatrolVanPrefab, spawnPosition, Quaternion.identity);

        spawnedCount++; // Increase count
    }
}