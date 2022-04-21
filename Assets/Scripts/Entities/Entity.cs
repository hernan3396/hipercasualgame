using UnityEngine;
using System.Collections;

abstract public class Entity : MonoBehaviour
{
    #region Parameters
    [SerializeField] protected int _currentHealth;
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected float _inmunityTime;
    [SerializeField] protected int _acceleration;
    [SerializeField] protected int _speed;
    protected bool _isInmune = false;
    #endregion

    virtual protected void Awake()
    {
        _currentHealth = _maxHealth;
    }

    virtual public void TakeDamage(int value)
    {
        if (_isInmune) return;
        _isInmune = true;

        _currentHealth -= value;

        if (_currentHealth <= 0)
        {
            Death();
            return;
        }

        StartCoroutine("ResetInmunity");
    }

    virtual protected IEnumerator ResetInmunity()
    {
        yield return new WaitForSeconds(_inmunityTime);
        _isInmune = false;
    }

    virtual protected void Death()
    {
        gameObject.SetActive(false);
    }
}
