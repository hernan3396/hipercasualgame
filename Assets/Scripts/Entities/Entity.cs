using UnityEngine;
using System.Collections;

abstract public class Entity : MonoBehaviour
{
    #region Parameters
    protected float _inmunityTime;
    protected int _maxHealth;
    [SerializeField] protected int _currentHealth;
    protected bool _isInmune = false;
    #endregion

    // protected abstract void Test();

    public virtual void TakeDamage(int value)
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

    protected virtual IEnumerator ResetInmunity()
    {
        yield return new WaitForSeconds(_inmunityTime);
        _isInmune = false;
    }

    protected virtual void Death()
    {
        gameObject.SetActive(false);
    }
}
