using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogoFunciones : MonoBehaviour
{
    public DialogoScriptable dialogoOriginal;
    DialogoScriptable dialogoActual;
    public TextMeshProUGUI texto;
    public TextMeshProUGUI nombrePersonaje;
    public GameObject panelDialogo;
    GameObject jugador;
    bool puedeIniciarConversacion = false;
    bool haIniciadoConversacion = false;

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
        if (Input.GetKeyDown(KeyCode.Z))
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

                nombrePersonaje.text = dialogoActual.nombrePersonaje;
                texto.text = dialogoActual.posibilidadesDialogo[0].texto;
                dialogoActual = dialogoActual.posibilidadesDialogo[0];



            }

        }

    }

    public void MostrarDialogoConEleccion(int eleccion)
    {
        if (Input.GetKeyDown(KeyCode.Z))
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

    void CerrarDialogo()
    {
        panelDialogo.SetActive(false);
        haIniciadoConversacion = false;
        if (jugador.GetComponent<MainCharacterController>() == null)
            jugador.AddComponent<MainCharacterController>();
    }

    void AbrirDialogo()
    {
        panelDialogo.SetActive(true);
        haIniciadoConversacion = true;
        jugador.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (jugador.GetComponent<MainCharacterController>() != null)
            Destroy(jugador.GetComponent<MainCharacterController>());
        texto.text = dialogoOriginal.texto;
        nombrePersonaje.text = dialogoOriginal.nombrePersonaje;
    }
}


