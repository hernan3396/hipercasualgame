using UnityEngine;

public class Laser : MonoBehaviour
{
    #region Components
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LaserData _laserData;
    private Transform _transform;
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
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private Transform _endPoint;
    #endregion

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _cam = GameManager.GetInstance.GetCamera;


        GameManager.GetInstance.onGameStart += OnGameStart;
        GameManager.GetInstance.onGamePause += OnGamePause;
        GameManager.GetInstance.onGameOver += OnGameOver;
    }

    private void Update()
    {
        if (!_canShoot) return;

        _mousePos = _cam.ScreenPointToRay(Input.mousePosition); // aims

        if (Input.GetMouseButtonDown(0))
        {
            // checks for collision
            if (Physics.Raycast(_mousePos, out RaycastHit hit, Mathf.Infinity, _aimMask))
                _hitPos = hit;

            Shoot();
        }
    }

    private void Shoot()
    {
        CancelInvoke("ClearLaser");

        Vector3 shootingDir = (_hitPos.point - _shootingPoint.position); // calculates shooting direction
        shootingDir = new Vector3(shootingDir.x, 0, shootingDir.z); // using local space, so we remove Y axis to avoid unwanted rotation

        _transform.forward = shootingDir; // rotates character

        Invoke("ClearLaser", _laserData.Duration);
        if (Physics.Raycast(_transform.position, shootingDir, out RaycastHit hit))
        {
            if (hit.collider)
            {
                _lineRenderer.SetPosition(1, _transform.InverseTransformPoint(hit.point));

                if (hit.collider.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(_laserData.Damage, _laserData.KnockbackForce);
                }
            }

            return;
        }

        _lineRenderer.SetPosition(1, _transform.InverseTransformPoint(_endPoint.position));
    }

    private void ClearLaser()
    {
        _lineRenderer.SetPosition(1, Vector3.zero);
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
