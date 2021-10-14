using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GameObject edificioAConstruir;
    public GameObject panelAdelantarTiempo;
    BuildController buildC;
    GameManager manager;


    [Header("SFX")]
    AudioController audioC;
    public AudioClip confirmar;
    public AudioClip cancelar;

    private void Start()
    {
        audioC = FindObjectOfType<AudioController>();

        buildC = FindObjectOfType<BuildController>();
        buildC.edificioAConstruir = edificioAConstruir;

        manager = FindObjectOfType<GameManager>();
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

    public void AbrirMensajeAdelantarTiempo()
    {
        panelAdelantarTiempo.SetActive(true);
    }

    public void CerrarMensajeAdelantarTiempo()
    {
        panelAdelantarTiempo.SetActive(false);
        Time.timeScale = 1f;
    }

    public void AdelantarTiempo()
    {
        CerrarMensajeAdelantarTiempo();
        manager.SetTiempoRestante(0f);
        GameObject.Find("Boton-Saltar").GetComponent<Button>().interactable = false;
    }

}
