using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GameObject edificioAConstruir;
    BuildController buildC;

    [Header("SFX")]
    AudioController audioC;
    public AudioClip confirmar;
    public AudioClip cancelar;

    private void Start()
    {
        audioC = FindObjectOfType<AudioController>();

        buildC = FindObjectOfType<BuildController>();
        buildC.edificioAConstruir = edificioAConstruir;
    }

    public void PrepararEdificio()
    {
        if (audioC != null)
        {
            audioC.PlaySFX(confirmar);
        }

        buildC.edificioAConstruir = edificioAConstruir;
        buildC.enabled = true;
    }

    public void CerrarMensajeError()
    {
        if (audioC != null)
        {
            audioC.PlaySFX(cancelar);   
        }

        GameObject.Find("Panel-Error").SetActive(false);
        Time.timeScale = 1;
    }

    //Este método es para el botón "Aceptar" del tutorial en el caso de perder a todos los soldados aliados
    public void CerrarMensajeGameOver()
    {
        if (audioC != null)
        {
            audioC.PlaySFX(cancelar);
        }
        GameObject.Find("Panel-Error").SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("FinPartida");
    }

}
