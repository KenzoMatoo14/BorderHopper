using UnityEngine;

public class BlackMan : Inmigrant
{
    void Awake()
    {
        base.Awake();

        life = 10;  
        speed = 3f; 
    }
}
