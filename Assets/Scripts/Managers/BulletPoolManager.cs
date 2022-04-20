using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bulletsPool;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _bulletsAmount = 20;
    private Transform _bulletsParent;

    private void Start()
    {
        _bulletsParent = transform;
        _bulletsPool = new List<GameObject>();

        GameObject go;

        for (int i = 0; i < _bulletsAmount; i++)
        {
            go = Instantiate(_bulletPrefab);
            go.transform.parent = _bulletsParent;
            go.SetActive(false);

            _bulletsPool.Add(go);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _bulletsAmount; i++)
        {
            if (!_bulletsPool[i].activeInHierarchy)
            {
                return _bulletsPool[i];
            }
        }
        return null;
    }
}
