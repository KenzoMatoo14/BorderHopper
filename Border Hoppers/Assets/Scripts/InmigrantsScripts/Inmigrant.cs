using UnityEngine;

public class Inmigrant : MonoBehaviour
{
    public int life = 1;
    public float speed = 2f;
    public int earn = 10;

    public GameObject audioSourcePrefab;
    private GameObject audioSourceInstance;
    private AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip deathSound;

    public Animator animator;

    public Animator getAnimator()
    {
        return animator;
    }

    protected virtual void Awake()
    {
        audioSourceInstance = Instantiate(audioSourcePrefab, transform);
        audioSource = audioSourceInstance.GetComponent<AudioSource>();

        BoxCollider boxCol = GetComponent<BoxCollider>();
        if (boxCol == null)
        {
            boxCol = gameObject.AddComponent<BoxCollider>();
        }
        boxCol.isTrigger = false;

        Vector3 baseSize = new Vector3(3f, 3f, 4f);

        // Ajustamos el tamaño en función de la escala del objeto
        Vector3 adjustedSize = new Vector3(
            baseSize.x / transform.lossyScale.x,
            baseSize.y / transform.lossyScale.y,
            baseSize.z / transform.lossyScale.z
        );

        boxCol.size = adjustedSize;
        boxCol.center = new Vector3(0, adjustedSize.y/2, adjustedSize.z); // Ajustar centro dinámicamente

        // Luego agregar Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true;
        rb.useGravity = false;
        Debug.Log(gameObject.name + " ahora tiene un Rigidbody.");
    }

    public virtual void TakeDamage(int damage)
    {
        if (life <= 0) return;

        life -= damage;
        Debug.Log("Inmigrant took " + damage + " damage! Remaining life: " + life);

        // When the immigrant dies
        if (life <= 0 && GameManager.manager != null)
        {
            speed = 0;
            animator.SetBool("isDead", true);
            audioSource.PlayOneShot(deathSound);
            GameManager.manager.AddCash(earn);
            Debug.Log(gameObject.name + " ha muerto. Se añadieron " + earn + " de dinero.");

            Destroy(gameObject, deathSound.length);
            GameManager.manager.ReduceEnemies();
        }
        else
        {
            audioSource.PlayOneShot(damageSound);
        }
    }
}