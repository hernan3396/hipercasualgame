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
    private bool _isGameOver = false;
    private bool _gamePaused = false;
    #endregion

    private void Start()
    {
        _transform = GetComponent<Transform>();

        GameManager.GetInstance.onGameOver += OnGameOver;
        GameManager.GetInstance.onGamePause += OnGamePause;
        GameManager.GetInstance.onGameStart += OnGameStart;
    }

    private void SpawnEnemy()
    {
        if (_isGameOver) return;
        if (_gamePaused) return;

        GameObject enemy = _enemyPoolManager.GetPooledObject();

        if (!enemy) return;

        enemy.SetActive(true);

        // calculates enemy spawn position
        Vector3 spawnVector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        spawnVector = (spawnVector.normalized * _spawnDistance) + _transform.position;

        enemy.transform.position = spawnVector;
    }

    private void OnGameStart()
    {
        InvokeRepeating("SpawnEnemy", _simpleEnemySpawnRate, _simpleEnemySpawnRate);
    }

    private void OnGamePause(bool value)
    {
        _gamePaused = value;
    }

    private void OnGameOver(bool value)
    {
        _isGameOver = value;
        CancelInvoke("SpawnEnemy");
    }

    private void OnDestroy()
    {
        GameManager.GetInstance.onGameStart -= OnGameStart;
        GameManager.GetInstance.onGamePause -= OnGamePause;
        GameManager.GetInstance.onGameOver -= OnGameOver;
    }
}
