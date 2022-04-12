using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;

    [SerializeField] private GameObject _enemyPrefab;
    // for spawning enemies
    [SerializeField] private float _initialXValue;
    [SerializeField] private float _finalXValue;
    [SerializeField] private float _initialYValue;
    [SerializeField] private float _finalYValue;
    [SerializeField] private float _freq = 1;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", _freq, _freq);
    }

    private void SpawnEnemy()
    {
        Vector3 randomPos = new Vector3(Random.Range(_initialXValue, _finalXValue), Random.Range(_initialYValue, _finalYValue), 0);
        GameObject enemy = Instantiate(_enemyPrefab, randomPos, Quaternion.Euler(0, 180, 0));
    }

    private void OnDestroy()
    {
        if (_instance != this)
        {
            _instance = this;
        }
    }

    public static EnemyManager GetInstance
    {
        get { return _instance; }
    }

    [SerializeField] private Transform _playerPos;

    public Transform GetPlayerPos
    {
        get { return _playerPos; }
    }
}
