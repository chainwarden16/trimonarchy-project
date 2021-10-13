using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public CinemachineVirtualCamera camara;
    public GameObject sirvienta;
    public GameObject panelDialogo;

    [Header("Música y SFX")]
    AudioController audioC;
    public AudioClip confirmar;

    int contador = 1;
    DialogoFunciones dialogo;

    private void Start()
    {
        audioC = FindObjectOfType<AudioController>();
        dialogo = FindObjectOfType<DialogoFunciones>();
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0) && contador <= 7)
        {
            if (audioC != null)
            {
                audioC.PlaySFX(confirmar);
            }
            dialogo.MostrarDialogoSinEleccion();
            contador++;
        }


        if (contador == 4)
        {
            camara.Follow = sirvienta.transform;
        }
        else if (contador == 8)
        {
            dialogo.CerrarDialogo();
            SceneManager.LoadScene("Tutorial");
        }
    }
}
