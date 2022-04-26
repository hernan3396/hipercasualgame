using System;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Player : Entity
{
    #region Components
    private UIController _uiController;
    private Animator _animator;
    private AudioManager _aManager;
    #endregion

    [SerializeField] private protected PlayerData _playerData;
    private bool _gamePaused = false;

    #region Parameters
    protected float _deathTime;
    protected bool _isGameOver;
    #endregion

    private void Awake()
    {
        _inmunityTime = _playerData.InmunityTime;
        _deathTime = _playerData.DeathTime;
        _maxHealth = _playerData.MaxHealth;
        _currentHealth = _maxHealth;

        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _uiController = GameManager.GetInstance.GetUIController;
        _aManager = GameManager.GetInstance.GetAudioManager;
        GameManager.GetInstance.onGameOver += OnGameOver;
        GameManager.GetInstance.onGamePause += OnGamePause;
    }

    public override void TakeDamage(int value, int knockback)
    {
        if (_isGameOver) return;
        if (_isInmune) return; // esta en base, pero por lo visto es necesario aca tambien (?)
        _aManager.PlayerHitSound();//sonido golpe player
        base.TakeDamage(value, knockback);
        _uiController.UpdateLifes(_currentHealth);
        _animator.SetBool("isDamaged", true);
    }

    protected override void StartDeath()
    {
        if (_isGameOver) return;
        _animator.SetBool("startDeath", true);
    }

    public override void Death()
    {
        // aca podrias usar la animacion de muerte
        if (_isGameOver) return;
        GameManager.GetInstance.GameOver();
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

    public float GetInmunityTime
    {
        get { return _inmunityTime; }
    }

    public float GetDeathTime
    {
        get { return _deathTime; }
    }

    public bool IsInmune
    {
        get { return _isInmune; }
        set { _isInmune = value; }
    }

    public bool IsPlayerPaused
    {
        get { return _gamePaused; }
    }
}
