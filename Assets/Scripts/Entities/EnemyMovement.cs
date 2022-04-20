using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform _playerPos;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _playerPos = GameManager.GetInstance.GetPlayerPosition; // realmente se puede usar el (0,0,0)
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _agent.SetDestination(_playerPos.position);
    }
}
