using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _shootingStrength = 1;
    [SerializeField] private float _shootingFreq = 2;
    [SerializeField] private GameObject _cannonBallPrefab;
    [SerializeField] private Transform _firePoint;
    private EnemyManager _enemyManager;
    private Transform _transform;
    private Vector3 _target;

    // Start is called before the first frame update
    void Start()
    {
        _enemyManager = EnemyManager.GetInstance;
        _target = _enemyManager.GetPlayerPos.position;
        _transform = GetComponent<Transform>();

        InvokeRepeating("Shoot", _shootingFreq, _shootingFreq);
    }

    private void Shoot()
    {
        GameObject cannonBall = Instantiate(_cannonBallPrefab, _firePoint.position, Quaternion.identity);
        Rigidbody rb = cannonBall.GetComponent<Rigidbody>();
        Vector3 direction = transform.position - _target;
        rb.AddForce(-direction.normalized * _shootingStrength, ForceMode.Impulse);
    }
}
