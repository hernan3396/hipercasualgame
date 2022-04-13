using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _targeteableMask;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float _shootingForce = 5;
    private Ray _mousePos;
    private RaycastHit _hit;
    private Camera _cam;
    private Transform _transform;
    private bool _isShooting = false;
    private Vector3 _shootingDirection;

    private void Start()
    {
        _cam = Camera.main;
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        // get mouse coord (screen) and transforms to world position
        _mousePos = _cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_mousePos, out _hit, Mathf.Infinity, _targeteableMask))
            transform.forward = _hit.point;

        if (Input.GetMouseButton(0) && !_isShooting)
        {
            //shoot
            StartCoroutine("ShootOnRate");
        }
    }

    private IEnumerator ShootOnRate()
    {
        _isShooting = true;
        yield return new WaitForSeconds(fireRate);
        Shoot();
        _isShooting = false;
    }

    private void Shoot()
    {
        GameObject go = Instantiate(_bulletPrefab, _shootingPoint.position, _shootingPoint.rotation);
        _shootingDirection = (_hit.point - _shootingPoint.position) * _shootingForce;

        if (go)
        {
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(_shootingDirection, ForceMode.Impulse);
        }
    }
}
