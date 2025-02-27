using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 0;
    private Vector3 targetPosition; // Store the adjusted target position

    private void Awake()
    {
        // Asegurar que la bala tenga un Collider
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            col = gameObject.AddComponent<SphereCollider>(); // Agregar un SphereCollider si no tiene
        }
        col.isTrigger = true; // Activar isTrigger

        // Asegurar que la bala tenga un Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = false;
        rb.isKinematic = true; // Para evitar que sea afectada por la física
    }

    public void SetTarget(Transform newTarget)
    {
        if (newTarget == null) return;

        // Offset target position upwards to aim at the chest (adjust 1.5f based on enemy size)
        targetPosition = newTarget.position + Vector3.up * 3f;
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    void Update()
    {
        // Destroy bullet if it has no target
        if (targetPosition == Vector3.zero)
        {
            Destroy(gameObject);
            return;
        }

        // Move towards adjusted target position (chest)
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        Inmigrant enemy = other.GetComponent<Inmigrant>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // Aplicar daño del disparo
            Debug.Log("Impacto en " + enemy.gameObject.name + " con " + damage + " de daño.");
            Destroy(gameObject);
        }
    }
}
