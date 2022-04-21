using UnityEngine;
using UnityEngine.AI;
using System.Collections;

abstract public class Enemy : Entity
{
    private Transform _playerPos;
    private NavMeshAgent _agent;

    protected override void Awake()
    {
        base.Awake();

        _agent = GetComponent<NavMeshAgent>();

        StartEnemy();
    }

    //TODO: algunas de estas cosas pasarlas a simple enemy
    private void Start()
    {
        _playerPos = GameManager.GetInstance.GetPlayerPosition;

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _agent.SetDestination(_playerPos.position);
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
