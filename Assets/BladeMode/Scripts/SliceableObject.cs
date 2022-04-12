using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableObject : MonoBehaviour
{
    [SerializeField] private LineRenderer _lr;
    [SerializeField] private Vector3 _lrDirection;
    private Vector3 _lrInitialPoint;
    private Vector3 _lrFinalPoint;

    // Start is called before the first frame update
    void Start()
    {
        Randomize();
    }

    private void Randomize()
    {
        _lr.SetPosition(0, new Vector3(-1.73f, Random.Range(-1.66f, 1), -1.4f));
        _lr.SetPosition(1, new Vector3(2, Random.Range(-1.66f, 1), -1.4f));

        _lrInitialPoint = _lr.GetPosition(0);
        _lrFinalPoint = _lr.GetPosition(1);

        _lrDirection = _lrFinalPoint - _lrInitialPoint;

        BladeTime.GetInstance.SetLrDirection = _lrDirection.normalized;
    }
}
