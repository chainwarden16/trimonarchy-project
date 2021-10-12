using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GameObject edificioAConstruir;
    BuildController buildC;

    private void Start()
    {
        buildC = FindObjectOfType<BuildController>();
        buildC.edificioAConstruir = edificioAConstruir;
    }

    public void PrepararEdificio()
    {

        buildC.edificioAConstruir = edificioAConstruir;
        buildC.enabled = true;
    }

    public void CerrarMensajeError()
    {
        GameObject.Find("Panel-Error").SetActive(false);
        Time.timeScale = 1;
    }

    //Este m�todo es para el bot�n "Aceptar" del tutorial en el caso de perder a todos los soldados aliados
    public void CerrarMensajeGameOver()
    {
        GameObject.Find("Panel-Error").SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("FinPartida");
    }

}
