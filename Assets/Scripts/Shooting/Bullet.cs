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
}
