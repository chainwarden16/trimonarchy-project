using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

}
