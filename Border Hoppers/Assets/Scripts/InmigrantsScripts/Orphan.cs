using UnityEngine;

public class Orphan : Inmigrant
{
    void Awake()
    {
        base.Awake();

        life = 2;
        speed = 12;
        earn = 5;
    }
}
