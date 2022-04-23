using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Bullet Data", order = 0)]
public class BulletData : ScriptableObject
{
    public int _knockbackForce = 50;
    public float _duration = 2;
    public int _damage = 1;
}