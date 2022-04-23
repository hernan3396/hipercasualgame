using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected EnemyData _enemyData;
    protected Transform _playerPos;
    protected float _deathTime;
    protected int _speed;

    protected Animator _animator;

    private void Awake()
    {
        // seteas los parametros
        _inmunityTime = _enemyData.InmunityTime;
        _deathTime = _enemyData.DeathTime;
        _maxHealth = _enemyData.MaxHealth;
        _currentHealth = _maxHealth;
        _speed = _enemyData.Speed;

        _animator = GetComponent<Animator>();

        _playerPos = GameManager.GetInstance.GetPlayerPosition;
    }

    public override void TakeDamage(int value)
    {
        if (_isInmune) return; // esta en base, pero por lo visto es necesario aca tambien (?)
        base.TakeDamage(value);
    }

    protected override void StartDeath()
    {
        _animator.SetBool("isDead", true);
    }

    public override void Death()
    {
        _currentHealth = _maxHealth;
        _isInmune = false;
        gameObject.SetActive(false);
    }

    public Transform TargetPos
    {
        get { return _playerPos; }
    }

    public int GetSpeed
    {
        get { return _speed; }
    }

    public float GetInmunityTime
    {
        get { return _inmunityTime; }
    }

    public float GetDeathTime
    {
        get { return _deathTime; }
    }
}
