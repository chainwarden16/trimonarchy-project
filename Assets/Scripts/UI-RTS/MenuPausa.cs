using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{

    #region Variables
    [Header("UI")]
    public GameObject uiMenuHab;
    public GameObject uiMenuOpciones;
    public GameObject uiMenuPausa;
    public GameObject cursorPausa;

    [Header("Comprobador menú activo")]
    public bool estaViendoHabilidades;
    public bool estaViendoOpciones;
    public bool estaPausado;

    [Header("Control del cursor")]

    [Header("Audio")]
    //AudioController contAudio;
    public AudioClip moverCursor;
    public AudioClip seleccionar;

    #endregion

    void Start()
    {

        uiMenuHab.SetActive(false);
        uiMenuPausa.SetActive(false);
        uiMenuOpciones.SetActive(false);

        //contAudio = FindObjectOfType<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.Escape)) //pausa el juego
            {

                estaPausado = !estaPausado;
                if (estaPausado)
                {
                    
                    Time.timeScale = 0;
                    uiMenuPausa.SetActive(true);
                }
                else //se devuelve todo al estado original
                {
                    
                    Time.timeScale = 1;
                    uiMenuPausa.SetActive(false);

                }
            }
        
    }
}
