using UnityEngine;

public class Mexican : Inmigrant
{
    // Constructor-like initialization
    void Awake()
    {
        base.Awake();

        life = 4;  // Set life to 4
        speed = 8f; // Set speed to 8
        crim = 20; // Set criminality to 20
    }
}
