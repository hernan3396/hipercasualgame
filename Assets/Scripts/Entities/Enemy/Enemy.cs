using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected EnemyData _enemyData;
    protected Transform _playerPos;
    protected int _speed;

    private Animator _animator;

    //TODO: algunas de estas cosas pasarlas a simple enemy
    //TODO: ordenar esto
    protected void Start()
    {
        // seteas los parametros
        _maxHealth = _enemyData.MaxHealth;
        _currentHealth = _maxHealth;
        _speed = _enemyData.Speed;

        _playerPos = GameManager.GetInstance.GetPlayerPosition;

        _animator = GetComponent<Animator>();
    }

    public override void TakeDamage(int value)
    {
        _animator.SetBool("isDamaged", true);
        base.TakeDamage(value);
    }

    protected override void Death()
    {
        _currentHealth = _maxHealth;
        _isInmune = false;
        base.Death();
    }

    public Transform TargetPos
    {
        get { return _playerPos; }
    }

    public int GetSpeed
    {
        get { return _speed; }
    }
}
