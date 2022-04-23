using UnityEngine;

public abstract class Enemy : Entity
{
    #region Components
    protected UIController _uiController;
    protected Animator _animator;
    protected Rigidbody _rb;
    #endregion

    #region Parameters
    [SerializeField] protected EnemyData _enemyData;
    protected Transform _playerPos;
    protected float _deathTime;
    protected int _scoreValue;
    protected int _speed;
    #endregion

    private void Awake()
    {
        // seteas los parametros
        _inmunityTime = _enemyData.InmunityTime;
        _scoreValue = _enemyData.ScoreValue;
        _deathTime = _enemyData.DeathTime;
        _maxHealth = _enemyData.MaxHealth;
        _currentHealth = _maxHealth;
        _speed = _enemyData.Speed;

        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.zero;

        _uiController = GameManager.GetInstance.GetUIController;
        _playerPos = GameManager.GetInstance.GetPlayerPosition;
    }

    public override void TakeDamage(int value, int knockback)
    {
        if (_isInmune) return; // esta en base, pero por lo visto es necesario aca tambien (?)
        base.TakeDamage(value, knockback);
    }

    protected override void StartDeath()
    {
        _animator.SetBool("isDead", true);
    }

    public override void Death()
    {
        _uiController.AddScore(_scoreValue);
        _rb.velocity = Vector3.zero;
        _currentHealth = _maxHealth;
        _isInmune = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TestCol();
            gameObject.SetActive(false);
        }
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
