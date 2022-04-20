using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Components
    private EnemyPoolManager _enemyPoolManager;
    private Transform _transform;
    #endregion

    #region SpawnParameters
    [Header("Spawn Parameters")]
    [SerializeField] private int _spawnDistance = 10;
    [SerializeField] private float _spawnRate = 1;
    #endregion

    private void Start()
    {
        _enemyPoolManager = GameManager.GetInstance.GetEnemyPoolManager;
        _transform = GetComponent<Transform>();

        InvokeRepeating("Spawn", _spawnRate, _spawnRate);
    }

    private void Spawn()
    {
        // temporal code
        GameObject enemy = _enemyPoolManager.GetPooledObject();

        if (!enemy) return;

        enemy.SetActive(true);

        // calculates enemy spawn position
        Vector3 spawnVector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        spawnVector = (spawnVector.normalized * _spawnDistance) + _transform.position;

        enemy.transform.position = spawnVector;
    }
}
