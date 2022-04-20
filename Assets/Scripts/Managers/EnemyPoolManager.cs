using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemiesPool;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _enemiesAmount = 20;
    private Transform _enemiesParent;

    private void Start()
    {
        _enemiesParent = transform;
        _enemiesPool = new List<GameObject>();

        GameObject go;

        for (int i = 0; i < _enemiesAmount; i++)
        {
            go = Instantiate(_enemyPrefab);
            go.transform.parent = _enemiesParent;
            go.SetActive(false);

            _enemiesPool.Add(go);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _enemiesAmount; i++)
        {
            if (!_enemiesPool[i].activeInHierarchy)
            {
                return _enemiesPool[i];
            }
        }
        return null;
    }
}