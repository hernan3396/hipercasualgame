using UnityEngine;

public class Shooting : MonoBehaviour
{
    //TODO: Pasar el daño de la bala a algun lado
    #region Components
    private PoolManager _bulletPoolManager;
    private Transform _transform;
    protected AudioManager _aManager;
    #endregion

    private bool _canShoot = false;

    #region Aiming
    [Header("Aiming")]
    [SerializeField] private LayerMask _aimMask;
    private RaycastHit _hitPos;
    private Ray _mousePos;
    private Camera _cam;
    #endregion

    #region ShootingSetup
    [Header("Shooting Setup")]
    [SerializeField] private Transform _shootingModel;
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

        GameManager.GetInstance.onGameStart += OnGameStart;
        GameManager.GetInstance.onGamePause += OnGamePause;
        GameManager.GetInstance.onGameOver += OnGameOver;
        _aManager = GameManager.GetInstance.GetAudioManager;
    }

    private void Update()
    {
        if (!_canShoot) return;

        _mousePos = _cam.ScreenPointToRay(Input.mousePosition); // aims

        if (Physics.Raycast(_mousePos, out RaycastHit hit, Mathf.Infinity, _aimMask))
        {
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
        _aManager.LaserSound(); //sonido laser

        Vector3 shootingDir = (_hitPos.point - _shootingPoint.position).normalized; // calculates shooting direction
        _shootingModel.forward = shootingDir;
        bullet.transform.position = _shootingPoint.position; // places bullet at the right place

        // impulse bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shootingDir * _shootForce, ForceMode.Impulse);
    }

    private void OnGameStart()
    {
        _canShoot = true;
    }

    private void OnGamePause(bool value)
    {
        _canShoot = !value;
    }

    private void OnGameOver(bool value)
    {
        _canShoot = !value;
    }

    private void OnDestroy()
    {
        GameManager.GetInstance.onGameStart -= OnGameStart;
        GameManager.GetInstance.onGamePause -= OnGamePause;
        GameManager.GetInstance.onGameOver -= OnGameOver;
    }
}
