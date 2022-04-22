using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _duration = 2;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        Invoke("DisableBullet", _duration);
    }

    private void DisableBullet()
    {
        // sets velocity to 0 or else when it spawns again it will mantain previous velocity
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) return;

        CancelInvoke("DisableBullet");

        // if (other.gameObject.TryGetComponent(out Enemy enemy))
        // {
        //     enemy.TakeDamage(1);
        // }

        // esto es temporal

        if (other.gameObject.TryGetComponent(out TempEnemy enemy))
            enemy.TakeDamage(1);

        DisableBullet();
    }
}
