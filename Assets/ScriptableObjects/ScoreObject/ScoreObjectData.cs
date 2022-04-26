using UnityEngine;

[CreateAssetMenu(fileName = "ScoreObjectData", menuName = "ScoreObject Data", order = 0)]
public class ScoreObjectData : ScriptableObject
{
    public int Value = 100;
    public int Speed = 50;
}