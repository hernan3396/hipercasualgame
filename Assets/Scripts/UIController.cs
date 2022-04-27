using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{

    [Header("Score")]
    [SerializeField] private int _pScore = 0;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [Header("Life")]
    [SerializeField] private GameObject[] _ImagenVida;

    [Header("GameStart")]
    [SerializeField] private GameObject _titleMenu;

    [Header("GamePause")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pauseBtn;

    [Header("GameOver")]
    [SerializeField] private GameObject _panelGameOver;
    [SerializeField] private TextMeshProUGUI _scoreGOText;
    void Start()
    {
        _scoreText.text = "Score: " + _pScore;

        GameManager.GetInstance.onGameStart += StartGame;
        GameManager.GetInstance.onGamePause += PauseGame;
        GameManager.GetInstance.onGameOver += DeathScreen;
    }

    public void UpdateLifes(int pLife)
    {
        if (pLife == 2)
        {
            _ImagenVida[0].SetActive(true);
            _ImagenVida[1].SetActive(true);
            _ImagenVida[2].SetActive(false);
        }
        else if (pLife == 1)
        {
            _ImagenVida[0].SetActive(true);
            _ImagenVida[1].SetActive(false);
            _ImagenVida[2].SetActive(false);
        }
        else if (pLife >= 0)
        {
            _ImagenVida[0].SetActive(false);
            _ImagenVida[1].SetActive(false);
            _ImagenVida[2].SetActive(false);
        }
    }

    public void AddScore(int addScore)
    {
        _pScore += addScore;
        _scoreText.text = "Score: " + _pScore;
    }

    public void StartGame()
    {
        _titleMenu.SetActive(false);
        _pauseBtn.SetActive(true);
        _scoreText.gameObject.SetActive(true);
    }

    public void PauseGame(bool value)
    {
        _pauseMenu.SetActive(value);
        _pauseBtn.SetActive(!value);
    }

    public void DeathScreen(bool value)
    {
        _panelGameOver.SetActive(value);
        _scoreGOText.text = "Tu puntaje es de: " + _pScore;
    }
}
