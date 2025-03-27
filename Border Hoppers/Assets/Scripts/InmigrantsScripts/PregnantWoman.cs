using UnityEngine;

public class PregnantWoman : Inmigrant
{
    public GameObject orphanPrefab; // Assign this in the Inspector
    void Awake()
    {
        base.Awake();

        life = 3;
        speed = 6f;
        earn = 15;
    }

    public override void TakeDamage(int damage)
    {
        if (life <= 0) return;

        Vector3 spawnPosition = transform.position; // Store the position before calling base method

        base.TakeDamage(damage); // Call parent logic (which destroys the object)

        // If the PregnantWoman is dead, instantiate an Orphan
        if (life <= 0 && orphanPrefab != null)
        {
            Instantiate(orphanPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("A PregnantWoman died, an Orphan has been created.");
        }
    }
}
