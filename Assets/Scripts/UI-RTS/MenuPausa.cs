using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{

    #region Variables
    [Header("UI")]
    public GameObject uiMenuOpciones;
    public GameObject uiMenuPausa;

    [Header("Comprobador menú activo")]
    public bool estaPausado = false;

    [Header("Control del cursor")]

    [Header("Audio")]
    //AudioController contAudio;
    public AudioClip seleccionar;

    MenuOpcionesGameplay gameplay;

    #endregion

    void Start()
    {
        gameplay = FindObjectOfType<MenuOpcionesGameplay>();

        uiMenuPausa.SetActive(false);
        uiMenuOpciones.SetActive(false);
        gameplay.enabled = false;

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

    public void AbrirMenuOpciones()
    {

        uiMenuOpciones.SetActive(true);
        //uiMenuPausa.SetActive(false);
        gameplay.enabled = true;
        this.enabled = false;

    }

    public void CerrarMenuPausa()
    {
        estaPausado = false;
        Time.timeScale = 1;
        uiMenuPausa.SetActive(false);
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
