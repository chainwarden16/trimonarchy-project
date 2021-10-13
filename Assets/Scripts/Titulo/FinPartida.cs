using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinPartida : MonoBehaviour
{
    public AudioClip confirmar, cancelar;
    AudioController audioC;

    private void Start()
    {
        audioC = FindObjectOfType<AudioController>();
        if (audioC != null)
        {
            if (SceneManager.GetActiveScene().name == "FinPartida")
            {
                audioC.PlaySong(audioC.musicaDerrota);

            }
            else if(SceneManager.GetActiveScene().name == "Victoria")
            {
                audioC.PlaySong(audioC.musicaVictoria);
            }
        }
    }

    public void VolverAlTitulo()
    {
        if (audioC != null)
        {
            audioC.PlaySong(audioC.musicaTitulo);
            audioC.PlaySFX(confirmar);
        }
        SceneManager.LoadScene("Titulo");
    }

    public void CerrarJuego()
    {
        if (audioC != null)
        {
            audioC.PlaySFX(cancelar);
        }
        Application.Quit();
    }
}
