using UnityEngine;

public class ScoreObject : MonoBehaviour
{
    [SerializeField] private int _valor;
    [SerializeField] private int _speed;

    protected UIController _uiController;
    protected Transform _playerPos;
    protected AudioManager _aManager;

    private bool _isPaused = false;


    void Start()
    {
        _aManager = GameManager.GetInstance.GetAudioManager;
        _uiController = GameManager.GetInstance.GetUIController;
        _playerPos = GameManager.GetInstance.GetPlayerPosition;
        GameManager.GetInstance.onGamePause += OnPause;
    }


    void Update()
    {
        if (_isPaused) return;
        Moverse();
        SeguirJugador();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _uiController.AddScore(_valor);
            Destroy(gameObject);
            _aManager.ScoreSound();
        }
    }

    public void Moverse()
    {
        transform.position += transform.up * _speed * Time.deltaTime;
    }
    public void SeguirJugador()
    {

        Vector3 Direction = _playerPos.position - transform.position;
        transform.up = Direction;

    }

    private void OnPause(bool isPaused)
    {
        _isPaused = isPaused;
    }

    private void OnDestroy()
    {
        GameManager.GetInstance.onGamePause -= OnPause;
    }
}
