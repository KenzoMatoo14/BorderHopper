using System.Collections.Generic;
using UnityEngine;

public class InmigrantMovement : MonoBehaviour
{
    private List<GameObject> pathVertices; // List of path vertices
    private int currentTargetIndex = 0; // Index of the next vertex to move towards
    public float rotationSpeed = 5f;
    private float originalY; // Stores the original Y position
    private Inmigrant inmigrant; // Generic reference to the base class

    void Start()
    {
        inmigrant = GetComponent<Inmigrant>();
        // Get the path vertices from the PathGenerator Singleton
        pathVertices = PathGenerator.Instance.pathVertices;

        // Store the original Y position
        originalY = transform.position.y;

        // Make sure we have a path before moving
        if (pathVertices.Count > 0)
        {
            // Start at first vertex (keeping the original Y position)
            transform.position = new Vector3(
                pathVertices[0].transform.position.x,
                originalY,
                pathVertices[0].transform.position.z
            );
        }
    }

    void Update()
    {
        if (pathVertices.Count == 0 || currentTargetIndex >= pathVertices.Count)
            return; // If there are no waypoints, do nothing

        // Get the target position, but keep the original Y value
        Vector3 targetPosition = new Vector3(
            pathVertices[currentTargetIndex].transform.position.x,
            originalY,
            pathVertices[currentTargetIndex].transform.position.z
        );

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, inmigrant.speed * Time.deltaTime);

        // Rotate smoothly towards the next target position
        RotateTowards(targetPosition);

        // Check if we have reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentTargetIndex++; // Move to the next vertex
        }
    }
    void RotateTowards(Vector3 target)
    {
        // Get the direction to the target
        Vector3 direction = (target - transform.position).normalized;

        // Keep rotation only on Y axis
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 90, direction.z));

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
