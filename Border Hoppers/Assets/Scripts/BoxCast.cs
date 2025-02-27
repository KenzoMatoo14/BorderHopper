//Attach this script to a GameObject. Make sure it has a Collider component by clicking the Add Component button. Then click Physics>Box Collider to attach a Box Collider component.
//This script creates a BoxCast in front of the GameObject and outputs a message if another Collider is hit with the Collider’s name.
//It also draws where the ray and BoxCast extends to. Just press the Gizmos button to see it in Play Mode.
//Make sure to have another GameObject with a Collider component for the BoxCast to collide with.

using UnityEngine;

public class BoxCast : MonoBehaviour
{

    private Transform target; // Closest target
    private bool hasTarget = false; // Check if there's a valid target  
    private Tower tower;        // Reference to Tower stats 

    void Start()
    {
        // Get the Tower component (works for any tower type)
        tower = GetComponent<Tower>();

        if (tower == null)
        {
            Debug.LogError("BoxCast script requires a Tower component on the same GameObject.");
        }
    }
    void Update()
    {
        FindClosestInmigrant();
    }

    void FindClosestInmigrant()
    {
        GameObject[] inmigrants = GameObject.FindGameObjectsWithTag("Inmigrant");
        float closestDistance = tower.range; // Get range from the Tower class
        Transform closestTarget = null;

        foreach (GameObject inmigrant in inmigrants)
        {
            float distance = Vector3.Distance(transform.position, inmigrant.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = inmigrant.transform;
            }
        }

        if (closestTarget != null)
        {
            target = closestTarget;
            hasTarget = true;
        }
        else
        {
            hasTarget = false;
        }
    }

    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        if (tower == null)
        {
            tower = GetComponent<Tower>(); // Try to assign in case it hasn't been set
            if (tower == null) return; // Exit if still null
        }

        Gizmos.color = Color.red;

        if (hasTarget && target != null)
        {
            // Adjust officer's starting position upwards
            Vector3 startPos = transform.position + Vector3.up * 3f; // Moves up 1.5 units from officer's feet

            // Adjust target's position upwards
            Vector3 adjustedTargetPos = target.position + Vector3.up * 3f; // Moves up 1.5 units from target's feet

            Gizmos.color = Color.green;
            Gizmos.DrawLine(startPos, adjustedTargetPos); // Draw line from officer's head to target's head
        }
        // Draw only the floor outline of the range (XZ plane)
        DrawCircle(transform.position, tower.range); // 40 points for a smooth circle
    }
    // Helper function to draw a circle in the XZ plane
    void DrawCircle(Vector3 center, float radius)
    {
        float angleStep = 360f / 40;
        Vector3 prevPoint = center + new Vector3(radius, 0, 0); // Start at (radius, 0)

        for (int i = 1; i <= 40; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 newPoint = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            Gizmos.DrawLine(prevPoint, newPoint);
            prevPoint = newPoint;
        }
    }


}