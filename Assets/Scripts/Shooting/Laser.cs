using UnityEngine;

public class Laser : MonoBehaviour
{
    #region Components
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LaserData _laserData;
    private AudioManager _aManager;
    private Transform _transform;
    #endregion

    private bool _canShoot = false;

    #region Aiming
    [Header("Aiming")]
    [SerializeField] private LayerMask _aimMask; // ray collides with this
    private Vector3 _hitPos; // where that ray hits (world space)
    private Ray _mousePos; // mouse position as a ray
    private Camera _cam; // main camera
    #endregion

    #region ShootingSetup
    [Header("Shooting Setup")]
    [SerializeField] private Transform _endPoint; // where laser ends, can change to a value but it works flawlessly
    #endregion

    private void Start()
    {
        _aManager = GameManager.GetInstance.GetAudioManager;
        _cam = GameManager.GetInstance.GetCamera;
        _transform = GetComponent<Transform>();

        GameManager.GetInstance.onGameStart += OnGameStart;
        GameManager.GetInstance.onGamePause += OnGamePause;
        GameManager.GetInstance.onGameOver += OnGameOver;
    }

    private void Update()
    {
        if (!_canShoot) return;

        if (Input.GetMouseButtonDown(0))
        {
            Aim();
            Shoot();
        }
    }

    /// <Summary>
    /// Sets mouse/touch position to world space
    /// </Summary>
    private void Aim()
    {
        _mousePos = _cam.ScreenPointToRay(Input.mousePosition); // position where you clicked

        // checks for collision
        if (Physics.Raycast(_mousePos, out RaycastHit hit, Mathf.Infinity, _aimMask))
            _hitPos = hit.point;
    }

    private void Shoot()
    {
        CancelInvoke("ClearLaser"); // clears timer so that new laser wont de-spawn faster than desired when spamming

        Vector3 shootingDir = CalculateShotingDir(_hitPos, _transform.position);
        _transform.forward = shootingDir; // rotates character

        _aManager.LaserSound(); //sonido laser

        Invoke("ClearLaser", _laserData.Duration); // clears laser after some time

        if (Physics.Raycast(_transform.position, shootingDir, out RaycastHit hit))
        {
            // si choca actualizar con esta posicion
            UpdateLaserPos(1, hit.point);

            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_laserData.Damage, _laserData.KnockbackForce); // make enemy take damage
            }

            return;
        }

        // sino con la posicion final
        UpdateLaserPos(1, _endPoint.position);
    }

    private Vector3 CalculateShotingDir(Vector3 finalPos, Vector3 origin)
    {
        Vector3 direction = finalPos - origin; // calculates shooting direction
        direction = new Vector3(direction.x, 0, direction.z); // using local space, so we remove Y axis to avoid unwanted rotation

        return direction;
    }

    private void UpdateLaserPos(int index, Vector3 pos)
    {
        _lineRenderer.SetPosition(index, _transform.InverseTransformPoint(pos));
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
