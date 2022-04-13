using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _playerPosition;
    private Transform _transform;
    private NavMeshAgent _agent;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _transform = GetComponent<Transform>();
    }

    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Update()
    {
        _agent.SetDestination(_playerPosition);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}
