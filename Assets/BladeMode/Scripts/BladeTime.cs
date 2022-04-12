using UnityEngine;
using EzySlice;

public class BladeTime : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Transform _cubeSpawner;
    #region Slicing
    [Header("Slicing")]
    [SerializeField] private LayerMask _sliceableObjects;
    [SerializeField] private Transform _cuttingPlane;
    [SerializeField] private Material _crossMaterial;
    #endregion

    #region SlicePosition
    [Header("Slice Position")]
    [SerializeField] private Vector3 _initialPoint;
    [SerializeField] private Vector3 _finalPoint;
    [SerializeField] private Vector3 _cutPos;
    private Camera _cam;
    #endregion

    #region SlicingChecking
    [Header("Slicing Checking")]
    [SerializeField] private Vector3 _lrDirection;
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        _cam = Camera.main;
    }

    private void Update()
    {
        Ray castPoint;
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            // sets initial cut position
            castPoint = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, _sliceableObjects))
                _initialPoint = hit.point;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // sets final cut position
            castPoint = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, _sliceableObjects))
                _finalPoint = hit.point;

            if (_initialPoint == Vector3.zero) return;
            if (_finalPoint == Vector3.zero) return;

            // sets plane rotation
            _cutPos = new Vector3(_finalPoint.x - _initialPoint.x, _finalPoint.y - _initialPoint.y, 0);
            _cutPos.Normalize();

            RotateCuttingPlane(_cutPos);

            // checks if rotation is similar to objective
            if (Mathf.Abs(_cutPos.x - _lrDirection.x) >= 0.1) return;
            if (Mathf.Abs(_cutPos.y - _lrDirection.y) >= 0.1) return;

            Slice();
        }
    }

    #region Slicing
    private void Slice()
    {
        Collider[] hits = Physics.OverlapBox(_cuttingPlane.position, new Vector3(20, 0.1f, 20), _cuttingPlane.rotation, _sliceableObjects);

        if (hits.Length <= 0)
            return;

        for (int i = 0; i < hits.Length; i++)
        {
            SlicedHull hull = SliceObject(hits[i].gameObject, _crossMaterial);

            if (hull != null)
            {
                GameObject bottom = hull.CreateLowerHull(hits[i].gameObject, _crossMaterial);
                GameObject top = hull.CreateUpperHull(hits[i].gameObject, _crossMaterial);
                AddHullComponents(bottom);
                AddHullComponents(top);
                Destroy(hits[i].gameObject);
            }
        }

        Instantiate(_cubePrefab, _cubeSpawner.position, Quaternion.identity);
    }

    private void AddHullComponents(GameObject go)
    {
        go.layer = 6;
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        MeshCollider collider = go.AddComponent<MeshCollider>();
        collider.convex = true;

        rb.AddExplosionForce(100, go.transform.position, 20);
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        if (obj.GetComponent<MeshFilter>() == null)
            return null;

        return obj.Slice(_cuttingPlane.position, _cuttingPlane.up, crossSectionMaterial);
    }
    #endregion

    private void RotateCuttingPlane(Vector3 rotationVector)
    {
        Quaternion rotation = Quaternion.LookRotation(rotationVector, Vector3.up);
        _cuttingPlane.rotation = rotation;
        _initialPoint = Vector3.zero;
        _finalPoint = Vector3.zero;
    }
    // lo hice singleton para facilitarme la vida
    private static BladeTime _instance;

    private void OnDestroy()
    {
        if (_instance != this)
        {
            _instance = this;
        }
    }

    public static BladeTime GetInstance
    {
        get { return _instance; }
    }

    public Vector3 SetLrDirection
    {
        get { return _lrDirection; }
        set { _lrDirection = value; }
    }
}
