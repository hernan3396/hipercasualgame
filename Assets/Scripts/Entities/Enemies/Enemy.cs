using UnityEngine;
using UnityEngine.AI;
using System.Collections;

abstract public class Enemy : Entity
{
    [SerializeField] protected EnemyData _enemyData;
    private Transform _playerPos;
    private NavMeshAgent _agent;
    private int _speed;
    private int _acceleration;

    protected void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    //TODO: algunas de estas cosas pasarlas a simple enemy
    //TODO: ordenar esto
    protected void Start()
    {
        _acceleration = _enemyData.Acceleration;
        _maxHealth = _enemyData.MaxHealth;
        _currentHealth = _maxHealth;
        _speed = _enemyData.Speed;

        _playerPos = GameManager.GetInstance.GetPlayerPosition;

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _agent.SetDestination(_playerPos.position);

        StartEnemy();
    }

    protected override IEnumerator ResetInmunity()
    {
        StopEnemy();
        yield return new WaitForSeconds(_inmunityTime);

        _isInmune = false;
        StartEnemy();
    }

    private void StopEnemy()
    {
        _agent.isStopped = true;
        _agent.acceleration = 0;
        _agent.speed = 0;
    }

    private void StartEnemy()
    {
        _agent.isStopped = false;
        _agent.acceleration = _acceleration;
        _agent.speed = _speed;
    }

    protected override void Death()
    {
        _currentHealth = _maxHealth;
        _isInmune = false;
        base.Death();
    }
}
