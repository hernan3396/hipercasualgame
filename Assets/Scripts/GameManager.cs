using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    #region PoolsManagers
    [Header("Pools Managers")]
    [SerializeField] private BulletPoolManager _bulletPoolManager;
    [SerializeField] private EnemyPoolManager _enemyPoolManager;
    #endregion
    [SerializeField] private UIController _uiController;
    [SerializeField] private Transform _playerPos;
    private Camera _camera;

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

    public void ReloadGame()
    {
        SceneManager.LoadScene("FinalGame");
    }

    public static GameManager GetInstance
    {
        get { return _instance; }
    }

    public Camera GetCamera
    {
        get { return _camera; }
    }

    public BulletPoolManager GetBulletPoolManager
    {
        get { return _bulletPoolManager; }
    }

    public EnemyPoolManager GetEnemyPoolManager
    {
        get { return _enemyPoolManager; }
    }

    public Transform GetPlayerPosition
    {
        get { return _playerPos; }
    }

    public UIController GetUIController
    {
        get { return _uiController; }
    }
}
