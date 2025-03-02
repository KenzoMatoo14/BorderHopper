using UnityEngine;

public class Mexican : Inmigrant
{
    void Awake()
    {
        base.Awake();

        life = 4;  
        speed = 6f; 
    }
}
