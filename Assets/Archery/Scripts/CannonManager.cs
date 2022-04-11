using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    // sacado de https://www.youtube.com/watch?v=xHYmUGyCwQU
    [SerializeField] private LineRenderer _lr;
    [SerializeField] private GameObject _cannonBallPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _force = 5f;
    private const int _trajectoryPoints = 10;
    private Vector3 _initialVelocity;
    private Transform _transform;
    private Camera _cam;
    private bool _pressingMouse = false;

    private void Start()
    {
        _cam = Camera.main;
        _lr.positionCount = _trajectoryPoints;
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pressingMouse = true;
            _lr.enabled = true;
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _pressingMouse = false;
            _lr.enabled = false;
            Fire();
            return;
        }

        if (_pressingMouse)
        {
            Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            _transform.LookAt(mousePos);

            _initialVelocity = (mousePos - _firePoint.position) * _force;

            UpdateLineRenderer();
        }
    }

    private void Fire()
    {
        GameObject cannonBall = Instantiate(_cannonBallPrefab, _firePoint.position, Quaternion.identity);
        Rigidbody rb = cannonBall.GetComponent<Rigidbody>();
        rb.AddForce(_initialVelocity, ForceMode.Impulse);
    }

    private void UpdateLineRenderer()
    {
        float g = Physics.gravity.magnitude;
        float velocity = _initialVelocity.magnitude;
        float angle = Mathf.Atan2(_initialVelocity.y, _initialVelocity.x);

        float timeStep = 0.1f;
        float fTime = 0f;

        Vector3 start = _firePoint.position;

        for (int i = 0; i < _trajectoryPoints; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle);
            float dy = (velocity * fTime * Mathf.Sin(angle) - (g * fTime * fTime / 2f));
            Vector3 pos = new Vector3(start.x + dx, start.y + dy, 0);
            _lr.SetPosition(i, pos);
            fTime += timeStep;
        }
    }
}
