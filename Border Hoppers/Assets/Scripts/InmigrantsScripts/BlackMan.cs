using UnityEngine;

public class BlackMan : Inmigrant
{
    void Awake()
    {
        base.Awake();

        life = 50;  
        speed = 3f;
        earn = 15;
    }
}
