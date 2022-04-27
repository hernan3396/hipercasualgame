using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Components
    #endregion
    [SerializeField] private BulletData _bulletData;
    private Rigidbody _rb;
    #region Parameters

    private int _knockbackForce;
    private float _duration;
    private int _damage;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _knockbackForce = _bulletData._knockbackForce;
        _duration = _bulletData._duration;
        _damage = _bulletData._damage;
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
        if (other.gameObject.CompareTag("ScoreObject")) return;

        CancelInvoke("DisableBullet");

        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage, _knockbackForce);
        }

        DisableBullet();
    }
}
