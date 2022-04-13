using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            other.gameObject.GetComponent<EnemyMovement>().enabled = false;
        }
    }
}
