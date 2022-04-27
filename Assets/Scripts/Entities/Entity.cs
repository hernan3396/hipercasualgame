using UnityEngine;

abstract public class Entity : MonoBehaviour
{
    #region Parameters
    [SerializeField] protected int _currentHealth;
    protected bool _isInmune = false;
    protected float _inmunityTime;
    protected int _maxHealth;
    #endregion

    public virtual void TakeDamage(int value, int knockback)
    {
        // luego me di cuenta que el knockback no era necesario en todos
        // pero no me dio tiempo de sacarlo
        if (_isInmune) return;
        _isInmune = true;

        _currentHealth -= value;

        if (_currentHealth <= 0)
        {
            StartDeath();
            return;
        }
    }

    protected abstract void StartDeath();

    public abstract void Death();

    public bool GetIsInmune
    {
        get { return _isInmune; }
        set { _isInmune = value; }
    }
}
