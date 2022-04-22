using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    [Header("Score")]
    [SerializeField] private int _pScore = 0;
    [SerializeField] private TextMeshProUGUI _scoreText;


    [Header("Life")]
    [SerializeField] private GameObject[] _ImagenVida;

    [Header("GameOver")]
    [SerializeField] private GameObject _panelGameOver;
    [SerializeField] private TextMeshProUGUI _scoreGOText;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + _pScore;
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     // _scoreText.text = "Score: " + _pScore;
    //     if (_pLife > 2)
    //     {
    //         _ImagenVida[0].SetActive(true);
    //         _ImagenVida[1].SetActive(true);
    //         _ImagenVida[2].SetActive(true);
    //     }
    //     else if (_pLife > 1)
    //     {
    //         _ImagenVida[0].SetActive(true);
    //         _ImagenVida[1].SetActive(true);
    //         _ImagenVida[2].SetActive(false);
    //     }
    //     else if (_pLife > 0)
    //     {
    //         _ImagenVida[0].SetActive(true);
    //         _ImagenVida[1].SetActive(false);
    //         _ImagenVida[2].SetActive(false);
    //     }
    //     else
    //     {
    //         /*
    //         _panelGameOver.SetActive(true);
    //         _scoreGOText.text = "Tu puntaje es de: " + _pScore;
    //         */
    //     }

    // }

    public void UpdateLifes(int pLife)
    {
        if (pLife > 2)
        {
            _ImagenVida[0].SetActive(true);
            _ImagenVida[1].SetActive(true);
            _ImagenVida[2].SetActive(true);
        }
        else if (pLife > 1)
        {
            _ImagenVida[0].SetActive(true);
            _ImagenVida[1].SetActive(true);
            _ImagenVida[2].SetActive(false);
        }
        else if (pLife > 0)
        {
            _ImagenVida[0].SetActive(true);
            _ImagenVida[1].SetActive(false);
            _ImagenVida[2].SetActive(false);
        }
    }

    public void AddScore(int addScore)
    {
        _pScore += addScore;
        _scoreText.text = "Score: " + _pScore;
    }

    public void DeathScreen()
    {
        _panelGameOver.SetActive(true);
        _scoreGOText.text = "Tu puntaje es de: " + _pScore;
    }
}
