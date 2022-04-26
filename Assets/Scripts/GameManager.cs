using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    #region PoolsManagers
    [Header("Pools Managers")]
    [SerializeField] private PoolManager _simpleEnemyPoolManager;
    [SerializeField] private PoolManager _bulletPoolManager;
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private UIController _uiController;
    [SerializeField] private Transform _playerPos;
    private Camera _camera;
    #endregion

    #region GameStart
    public delegate void OnGameStart();
    public event OnGameStart onGameStart;
    #endregion

    #region GamePause
    public delegate void OnGamePause(bool isGamePaused);
    public event OnGamePause onGamePause;
    private bool _isGamePaused = false;
    #endregion

    #region GameOver
    public delegate void OnGameOver(bool isGameOver);
    public event OnGameOver onGameOver;
    private bool _isGameOver = false;
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        _camera = Camera.main;
    }

    private void OnDestroy()
    {
        if (_instance != this)
        {
            _instance = this;
        }
    }

    public void StartGame()
    {
        if (onGameStart != null)
            onGameStart();
    }

    public void GamePause()
    {
        _isGamePaused = !_isGamePaused;

        if (onGamePause != null)
            onGamePause(_isGamePaused);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("FinalGame");
    }

    public void GameOver()
    {
        _isGameOver = true;

        if (onGameOver != null)
            onGameOver(_isGameOver);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public static GameManager GetInstance
    {
        get { return _instance; }
    }

    public Camera GetCamera
    {
        get { return _camera; }
    }

    public PoolManager GetBulletPoolManager
    {
        get { return _bulletPoolManager; }
    }

    public PoolManager GetSimpleEnemyPoolManager
    {
        get { return _simpleEnemyPoolManager; }
    }

    public Transform GetPlayerPosition
    {
        get { return _playerPos; }
    }

    public UIController GetUIController
    {
        get { return _uiController; }
    }

    public AudioManager GetAudioManager
    {
        get { return _audioManager; }
    }
}
