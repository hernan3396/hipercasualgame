using UnityEngine;

public class Player : Entity
{
    protected override void Death()
    {
        base.Death();
        Debug.Log("Perdiste");
    }
}
