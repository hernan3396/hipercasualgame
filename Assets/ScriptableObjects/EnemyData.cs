using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy Data", order = 0)]
public class EnemyData : ScriptableObject
{
    public float InmunityTime; // Tiempo que es inmune entre golpes
    public float DeathTime; // Tiempo que pasa muerto
    public int MaxHealth; // Vida maxima
    public int Speed; // Velocidad
}