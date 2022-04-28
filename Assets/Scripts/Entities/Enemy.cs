using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]

public abstract class Enemy : Entity
{
    #region Components
    protected UIController _uiController;
    protected Animator _animator;
    protected Rigidbody _rb;
    #endregion

    private bool _isGameOver = false;
    private bool _gamePaused = false;

    #region Parameters
    [SerializeField] protected GameObject _scoreObject;
    [SerializeField] protected EnemyData _enemyData;
    protected Transform _playerPos;
    protected float _deathTime;
    private int _atkDamage;
    protected int _speed;
    #endregion

    private void Awake()
    {
        SetParameters();

        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.zero;

    }

    private void Start()
    {
        _uiController = GameManager.GetInstance.GetUIController;
        _playerPos = GameManager.GetInstance.GetPlayerPosition;

        GameManager.GetInstance.onGamePause += OnGamePause;
        GameManager.GetInstance.onGameOver += OnGameOver;
    }

    private void SetParameters()
    {
        // seteas los parametros
        _inmunityTime = _enemyData.InmunityTime;
        _atkDamage = _enemyData.AtkDamage;
        _deathTime = _enemyData.DeathTime;
        _maxHealth = _enemyData.MaxHealth;
        _currentHealth = _maxHealth;
        _speed = _enemyData.Speed;
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
        Instantiate(_scoreObject, transform.position, Quaternion.identity);
        _rb.velocity = Vector3.zero;
        _currentHealth = _maxHealth;
        _isInmune = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(_atkDamage, 0);

            if (_isGameOver) return; // crea un efecto que los enemigos te pisotean
            gameObject.SetActive(false);
        }
    }

    private void OnGamePause(bool value)
    {
        _gamePaused = value;
    }

    private void OnGameOver(bool value)
    {
        _isGameOver = value;
    }

    private void OnDestroy()
    {
        GameManager.GetInstance.onGamePause -= OnGamePause;
        GameManager.GetInstance.onGameOver -= OnGameOver;
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

    public bool IsEnemyPaused
    {
        get { return _gamePaused; }
    }
}
