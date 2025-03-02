using UnityEngine;

public class TacoBell : MonoBehaviour
{
    public GameObject audioSourcePrefab;
    private GameObject audioSourceInstance;
    private AudioSource audioSource;
    public AudioClip arrival;
    private void Awake()
    {
        audioSourceInstance = Instantiate(audioSourcePrefab, transform);
        audioSource = audioSourceInstance.GetComponent<AudioSource>();

        Collider col = GetComponent<Collider>();

        if (col == null)
        {
            col = gameObject.AddComponent<BoxCollider>();
            Debug.Log(" BoxCollider added to" + gameObject.name);
        }

        col.isTrigger = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        Inmigrant inmigrant = other.GetComponent<Inmigrant>();

        if (inmigrant != null)
        {
            Debug.Log("Inmigrant arrived");
            Hire(inmigrant.life);
            audioSource.PlayOneShot(arrival);
            Destroy(other.gameObject);
        }

    }

    public void Hire(int crim)
    {
        GameManager.manager.IncreaseCriminality(crim);
    }
}
