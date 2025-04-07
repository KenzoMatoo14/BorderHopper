using UnityEngine;

public class Mexican : Inmigrant
{
    void Awake()
    {
        base.Awake();

        life = 20;  
        speed = 6f;
        earn = 10;
    }
}
