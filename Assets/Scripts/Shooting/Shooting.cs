using UnityEngine;

public class Shooting : MonoBehaviour
{
    //TODO: Pasar el daño de la bala a algun lado
    #region Components
    private PoolManager _bulletPoolManager;
    private Transform _transform;
    #endregion

    #region Aiming
    [Header("Aiming")]
    [SerializeField] private LayerMask _aimMask;
    private RaycastHit _hitPos;
    private Ray _mousePos;
    private Camera _cam;
    #endregion

    #region ShootingSetup
    [Header("Shooting Setup")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private int _armLength; // spawns bullet at weapon + arm length
    #endregion

    #region ShootingParameters
    [Header("Shooting Parameters")]
    [SerializeField, Range(0, 100)] private int _shootForce = 5;
    #endregion

    private void Start()
    {
        if (TryGetComponent<Transform>(out Transform transform)) _transform = transform;

        _bulletPoolManager = GameManager.GetInstance.GetBulletPoolManager;
        _cam = GameManager.GetInstance.GetCamera;
    }

    private void Update()
    {
        _mousePos = _cam.ScreenPointToRay(Input.mousePosition); // aims

        if (Physics.Raycast(_mousePos, out RaycastHit hit, Mathf.Infinity, _aimMask))
        {
            // sirve para rotar a los brazos
            _hitPos = hit;
        }

        // shoots
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = _bulletPoolManager.GetPooledObject();

        if (!bullet) return;

        bullet.SetActive(true);

        Vector3 shootingDir = (_hitPos.point - _shootingPoint.position).normalized; // calculates shooting direction
        bullet.transform.position = (_shootingPoint.position + shootingDir) * _armLength; // places bullet at the right place

        // impulse bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shootingDir * _shootForce, ForceMode.Impulse);
    }
}
