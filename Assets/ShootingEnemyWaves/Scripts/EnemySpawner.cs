using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _enemySpawnPoint;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private float _spawnRate;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0.5f, _spawnRate);
    }

    private void SpawnEnemy()
    {
        Vector3 spawnVector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        spawnVector = spawnVector.normalized * _spawnDistance;

        Instantiate(_enemyPrefab, _enemySpawnPoint.position + spawnVector, Quaternion.identity);
    }
}
