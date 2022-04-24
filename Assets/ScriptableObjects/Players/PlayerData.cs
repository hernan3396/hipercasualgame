using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data", order = 0)]
public class PlayerData : ScriptableObject
{
    public float InmunityTime;
    public float DeathTime; // se puede usar para hacer una animacion de muerte
    public int MaxHealth;
}