using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy Data", order = 0)]
public class EnemyData : ScriptableObject
{
    public float InmunityTime;
    public int MaxHealth;
    public int Speed;
}