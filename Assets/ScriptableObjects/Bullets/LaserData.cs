using UnityEngine;

[CreateAssetMenu(fileName = "LaserData", menuName = "Laser Data", order = 0)]
public class LaserData : ScriptableObject
{
    public int KnockbackForce = 50;
    public float Duration = 2;
    public int Damage = 1;
}