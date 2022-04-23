using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Components
    [SerializeField] private PoolManager _enemyPoolManager;
    private Transform _transform;
    #endregion

    #region SpawnParameters
    [Header("Spawn Parameters")]
    [SerializeField] private int _spawnDistance = 10;
    [SerializeField] private float _simpleEnemySpawnRate = 1;
    #endregion

    private void Start()
    {
        _transform = GetComponent<Transform>();

        InvokeRepeating("SpawnSimpleEnemy", _simpleEnemySpawnRate, _simpleEnemySpawnRate);
    }

    private void SpawnSimpleEnemy()
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
