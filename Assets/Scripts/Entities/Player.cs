using UnityEngine;

public class Player : Entity
{
    public void TestCol()
    {
        Debug.Log("Choco");
    }

    protected override void StartDeath()
    {
        throw new System.NotImplementedException();
    }

    public override void Death()
    {
        throw new System.NotImplementedException();
    }
}
