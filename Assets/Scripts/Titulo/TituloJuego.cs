using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TituloJuego : MonoBehaviour
{

    AudioController audioJuego;
    public AudioClip confirmar, cancelar;

    private void Start()
    {
        audioJuego = FindObjectOfType<AudioController>();
    }

    public void EmpezarPartida()
    {
        if(audioJuego != null)
        {
            audioJuego.PlaySFX(confirmar);
            audioJuego.PlaySong(audioJuego.musicaIntro);
        }
        SceneManager.LoadScene("Introduccion");
    }

    public void MenuOpciones()
    {
        if (audioJuego != null)
        {
            audioJuego.PlaySFX(confirmar);
        }
        SceneManager.LoadScene("Opciones");
    }

    public void CerrarJuego()
    {
        if (audioJuego != null)
        {
            audioJuego.PlaySFX(cancelar);
        }
        Application.Quit();
    }
}
