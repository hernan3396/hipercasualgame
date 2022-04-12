using UnityEngine;
using EzySlice;

public class BladeTime : MonoBehaviour
{
    [SerializeField] private LayerMask _sliceableObjects;
    [SerializeField] private Transform _cuttingPlane;
    [SerializeField] private Material _crossMaterial;

    #region SlicePosition
    [SerializeField] private Vector3 _initialPoint;
    [SerializeField] private Vector3 _finalPoint;
    [SerializeField] private Vector3 _cutPos;
    private Camera _cam;
    #endregion

    private void Awake()
    {
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

            _cutPos = new Vector3(_finalPoint.x - _initialPoint.x, _finalPoint.y - _initialPoint.y, 0);
            RotateCuttingPlane(_cutPos);
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
}
