using UnityEngine;

public class TowerShooting : MonoBehaviour
{

    public GameObject audioSourcePrefab;
    private GameObject audioSourceInstance;
    private AudioSource audioSource;
    public AudioClip shot;

    public GameObject bulletPrefab;  // Bullet prefab to shoot
    private float currentTime;       // Timer for shooting
    private Transform target;        // Current target
    private Tower tower;        // Reference to Tower stats

    void Start()
    {
        tower = GetComponent<Tower>(); // Get tower stats
        currentTime = currentTime = 0f; // Start timer at 0 to allow first shot immediately

        audioSourceInstance = Instantiate(audioSourcePrefab, transform);
        audioSource = audioSourceInstance.GetComponent<AudioSource>();
    }

    void Update()
    {
        FindClosestInmigrant(); // Continuously find the closest target

        if (target != null)
        {
            RotateTowardsTarget();

            // Fire at intervals based on firerate
            if (currentTime <= 0f)
            {
                Shoot();
                currentTime = tower.firerate; // Reset timer
            }
        }
        // Decrease timer
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
        }
    }

    void RotateTowardsTarget()
    {
        if (target == null) return;

        // Direction to the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Only rotate on the Y-axis for 3D space
        float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(-90, angleY, 0);

        // Smoothly rotate towards the target
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    void FindClosestInmigrant()
    {
        GameObject[] inmigrants = GameObject.FindGameObjectsWithTag("Inmigrant");
        float closestDistance = tower.range; // Only consider within range
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

        target = closestTarget; // Set the closest target within range
    }
    void Shoot()
    {
        if (target == null) return;

        // Spawn the bullet in front of the tower based on its forward direction
        Vector3 spawnPosition = transform.position + transform.forward * 2.0f + Vector3.up * 2.0f;
        Quaternion spawnRotation = Quaternion.LookRotation(transform.forward); // Face forward

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetTarget(target);
            bulletScript.SetDamage(tower.damage);
            audioSource.PlayOneShot(shot);
        }
    }
    
}
