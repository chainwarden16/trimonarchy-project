using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogoFunciones : MonoBehaviour
{
    public DialogoScriptable dialogoOriginal;
    DialogoScriptable dialogoActual;
    public Text texto;
    public Text nombrePersonaje;
    public GameObject panelDialogo;
    GameObject jugador;
    bool puedeIniciarConversacion = false;
    bool haIniciadoConversacion = false;
    public Image portrait;

    private void Start()
    {

        jugador = GameObject.FindGameObjectWithTag("Player");
        dialogoActual = dialogoOriginal;
    }

    private void Update()
    {
        if (puedeIniciarConversacion)
        {
            if (!haIniciadoConversacion)
            {

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Debug.Log("Abro dialogo");
                    AbrirDialogo();

                }

            }
            else
            {
                MostrarDialogoSinEleccion();
            }
        }
    }

    public void MostrarDialogoSinEleccion()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogoActual.posibilidadesDialogo.Count == 0)//si no hay otro diálogo, es que se debe cerrar la ventana porque la conversación ha concluido
            {
                texto.text = "";
                nombrePersonaje.text = "";
                dialogoActual = dialogoOriginal; //se reinicia la conversación desde el principio
                
                if (portrait != null)
                {

                    portrait.sprite = null;
                    portrait.color = new Color(1, 1, 1, 0);

                }

                CerrarDialogo();
            }

            else
            {

                nombrePersonaje.text = dialogoActual.nombrePersonaje;
                texto.text = dialogoActual.posibilidadesDialogo[0].texto;
                dialogoActual = dialogoActual.posibilidadesDialogo[0];

                if (portrait != null)
                {

                    if (dialogoActual.personaje != null)
                    {
                        portrait.sprite = dialogoActual.personaje;
                        portrait.color = new Color(1, 1, 1, 1);
                    }
                    else
                    {
                        portrait.sprite = null;
                        portrait.color = new Color(1, 1, 1, 0);
                    }

                }

            }

        }

    }

    public void MostrarDialogoConEleccion(int eleccion)
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (dialogoActual.posibilidadesDialogo.Count == 0)//si no hay otro diálogo, es que se debe cerrar la ventana porque la conversación ha concluido
            {
                texto.text = "";
                nombrePersonaje.text = "";
                dialogoActual = dialogoOriginal; //se reinicia la conversación desde el principio
                CerrarDialogo();
            }
            else
            {
                texto.text = dialogoActual.posibilidadesDialogo[eleccion].texto;
                dialogoActual = dialogoActual.posibilidadesDialogo[eleccion];
            }

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            puedeIniciarConversacion = true;


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            puedeIniciarConversacion = false;
        }
    }

    public void CerrarDialogo()
    {
        panelDialogo.SetActive(false);
        haIniciadoConversacion = false;

    }

    public void AbrirDialogo()
    {
        panelDialogo.SetActive(true);
        haIniciadoConversacion = true;


        texto.text = dialogoOriginal.texto;
        nombrePersonaje.text = dialogoOriginal.nombrePersonaje;
    }

    public void AbrirDialogoDondeEstaba()
    {
        panelDialogo.SetActive(true);
    }

    public void CerrarDialogoDondeEstaba()
    {
        panelDialogo.SetActive(false);
    }
}


