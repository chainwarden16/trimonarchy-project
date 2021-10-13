using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource sourceMusica, sourceSFX;
    public AudioClip musicaTitulo;
    public AudioClip musicaDerrota;
    public AudioClip musicaTutorial;
    public AudioClip musicaCombate;
    public AudioClip musicaVictoria;
    public AudioClip musicaConstruccion;
    public AudioClip musicaIntro;

    public static AudioController _instance;

    #region AudioController singleton

    private void Awake()
    {
        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    private void Start()
    {

        sourceMusica.clip = musicaTitulo;
        int volumenMusica = PlayerPrefs.GetInt("Musica", 10);
        int volumenSFX = PlayerPrefs.GetInt("SFX", 10);
        sourceMusica.volume = volumenMusica * 0.1f;
        sourceSFX.volume = volumenSFX * 0.1f;
        sourceMusica.Play();
    }

    #region Reproduccion de sonidos

    public void PlaySong(AudioClip aud)
    {
        sourceMusica.clip = aud;
        sourceMusica.Play();
    }

    public void PlaySFX(AudioClip aud)
    {
        if (!sourceSFX.isPlaying)
        {

            sourceSFX.PlayOneShot(aud);

        }
    }

    #endregion

}
