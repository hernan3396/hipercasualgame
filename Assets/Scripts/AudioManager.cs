using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    
    [Header("Music")]
    [SerializeField] private AudioSource _musicAS;
    [SerializeField] private AudioClip _musicClip;

    [Header("Enemy")]
    [SerializeField] private AudioSource[] _enemyHitAS;
    [SerializeField] private AudioSource[] _enemyDeathAS;

    [Header("Player")]
    [SerializeField] private AudioSource[] _shootAS;
    [SerializeField] private AudioClip _shootClip;
    [SerializeField] private AudioSource[] _scoreAS;
    [SerializeField] private AudioClip _scoreClip;

















    // Start is called before the first frame update
    void Start()
    {
        IniciarMusica();
        IniciarPuntos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreSound()
    {
        if(!_scoreAS[0].isPlaying)
        {
            _scoreAS[0].Play();
        }
        else if (!_scoreAS[1].isPlaying)
        {
            _scoreAS[1].Play();
        }
        else
        {
            _scoreAS[0].Play();
        }
    }

    public void IniciarPuntos()
    {
        _scoreAS[0].clip = _scoreClip;
        _scoreAS[1].clip = _scoreClip;
    }
    public void IniciarMusica()
    {
        _musicAS.clip = _musicClip;
        _musicAS.Play();
    }
}
