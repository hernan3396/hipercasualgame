using UnityEngine;
using EzySlice;

public class BladeTime : MonoBehaviour
{
    [SerializeField] private LayerMask _sliceableObjects;
    [SerializeField] private Transform _cuttingPlane;
    [SerializeField] private float _sensitivity = 5;
    [SerializeField] private Material _crossMaterial;

    private void Update()
    {
        RotatePlane();

        if (Input.GetMouseButtonDown(0))
        {
            Slice();
        }
    }

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

    private void RotatePlane()
    {
        _cuttingPlane.eulerAngles += new Vector3(0, 0, -Input.GetAxis("Mouse X") * _sensitivity);
    }
}
