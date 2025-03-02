using System.Collections.Generic;
using UnityEngine;

public class BorderPatrolVan : MonoBehaviour
{
    private List<GameObject> pathVertices; // Path waypoints
    private int currentTargetIndex; // Start from the last waypoint
    public float rotationSpeed = 5f;
    private float originalY;
    public float speed = 3f; // Unique speed for BorderPatrolVan
    public int life = 20; // Van's health

    void Start()
    {
        // Get path waypoints
        pathVertices = PathGenerator.Instance.pathVertices;

        // Store original Y position
        originalY = transform.position.y;

        // Start at last waypoint (if path exists)
        if (pathVertices.Count > 0)
        {
            currentTargetIndex = pathVertices.Count - 1; // Start from the last vertex
            transform.position = new Vector3(
                pathVertices[currentTargetIndex].transform.position.x,
                originalY,
                pathVertices[currentTargetIndex].transform.position.z
            );
        }
    }

    void Update()
    {
        if (pathVertices.Count == 0 || currentTargetIndex < 0)
            return; // Stop when no waypoints left

        // Get target position, keeping original Y
        Vector3 targetPosition = new Vector3(
            pathVertices[currentTargetIndex].transform.position.x,
            originalY,
            pathVertices[currentTargetIndex].transform.position.z
        );

        // Move towards target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Rotate smoothly towards target
        RotateTowards(targetPosition);

        // Check if reached target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentTargetIndex--; // Move to the previous vertex
        }
    }

    void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 90, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Handle collision with inmigrants
    private void OnTriggerEnter(Collider other)
    {
        Inmigrant inmigrant = other.GetComponent<Inmigrant>();
        if (inmigrant != null)
        {
            life -= inmigrant.life; // Reduce van's life by inmigrant's life

            Debug.Log("BorderPatrolVan took " + inmigrant.life + " damage! Remaining life: " + life);

            if (life <= 0)
            {
                Debug.Log("BorderPatrolVan destroyed!");
                Destroy(gameObject); // Destroy van if life reaches 0
            }

            inmigrant.TakeDamage(inmigrant.life); // Kill inmigrant
        }
    }
}
