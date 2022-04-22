using UnityEngine.AI;
using UnityEngine;

public class TempEnemy : MonoBehaviour
{
    private UIController _uiController;
    [SerializeField] private int _value = 100;
    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth;
    private Transform _playerPosition;
    private Transform _transform;
    private NavMeshAgent _agent;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _transform = GetComponent<Transform>();

        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        _playerPosition = GameManager.GetInstance.GetPlayerPosition;
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.SetDestination(_playerPosition.position);

        _uiController = GameManager.GetInstance.GetUIController;
    }

    private void OnEnable()
    {
        _playerPosition = GameManager.GetInstance.GetPlayerPosition;

        _agent.SetDestination(_playerPosition.position);
    }

    private void Update()
    {
        float distance = Vector3.Distance(_playerPosition.position, transform.position);

        if (distance <= 2)
        {
            _playerPosition.gameObject.GetComponent<TempPlayer>().TakeDamage(1);
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            Death();
    }

    private void Death()
    {
        _uiController.AddScore(_value);
        _currentHealth = _maxHealth;
        gameObject.SetActive(false);
    }
}
