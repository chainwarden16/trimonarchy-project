using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuOpcionesGameplay : MonoBehaviour
{
    #region Variables
    public GameObject flechaMusicaIqz;
    public GameObject flechaMusicaDer;
    public GameObject flechaSFXIqz;
    public GameObject flechaSFXDer;
    public GameObject contenedorMenu;
    public Text musicaNum;
    public Text sfxNum;

    [Header("Audio")]
    public AudioClip clipPrueba;
    GameObject objetoSonidos;
    AudioSource sourceMusica, sourceSFX;
    public AudioClip seleccionar;

    int musica = 0;
    int sfx = 0;

    [Header("Elementos del menú de pausa")]
    MenuPausa pausa;
    public GameObject panelOpciones;


    #endregion

    void Start()
    {
        //Los números no se actualizan. Haz que se pongan correctamente cada vez que se abra el menú
        musicaNum.text = PlayerPrefs.GetInt("Musica", 10).ToString();
        sfxNum.text = PlayerPrefs.GetInt("SFX", 10).ToString();
        musica = PlayerPrefs.GetInt("Musica", 10);
        sfx = PlayerPrefs.GetInt("SFX", 10);
        objetoSonidos = GameObject.Find("--Musica--");
        sourceMusica = objetoSonidos.GetComponent<AudioController>().sourceMusica;
        sourceSFX = objetoSonidos.GetComponent<AudioController>().sourceSFX;

        pausa = FindObjectOfType<MenuPausa>();

        ComprobarFlechasMenu();

    }

    public void SubirVolumenMusica()
    {
        if (musica < 10)
        {
            musica++;
            sourceMusica.volume += 0.1f;
        }

        ComprobarFlechasMenu();

        musicaNum.text = musica.ToString();
    }

    public void BajarVolumenMusica()
    {
        if (musica > 0)
        {
            musica--;
            sourceMusica.volume -= 0.1f;
        }

        ComprobarFlechasMenu();

        musicaNum.text = musica.ToString();
    }

    public void SubirVolumenSFX()
    {
        if (sfx < 10)
        {

            sfx++;
            sourceSFX.volume += 0.1f;
            sourceSFX.PlayOneShot(seleccionar);

        }

        ComprobarFlechasMenu();

        sfxNum.text = sfx.ToString();
    }

    public void BajarVolumenSFX()
    {
        if (sfx > 0)
        {

            sfx--;
            sourceSFX.volume -= 0.1f;
            sourceSFX.PlayOneShot(seleccionar);

        }

        ComprobarFlechasMenu();

        sfxNum.text = sfx.ToString();
    }


    public void RegresarAPausa()
    {
        objetoSonidos.GetComponent<AudioController>().PlaySFX(seleccionar);
        PlayerPrefs.SetInt("Musica", musica);
        PlayerPrefs.SetInt("SFX", sfx);
        panelOpciones.SetActive(false);
        pausa.enabled = true;
        this.enabled = false;
    }

    /// <summary>
    /// Esta función hace visible o invisible las flechas de selección de volumen de los elementos
    /// </summary>
    void ComprobarFlechasMenu()
    {
        if (flechaMusicaIqz != null && flechaMusicaDer != null)
        {
            //Musica

            if (musica == 0)
            {
                flechaMusicaIqz.SetActive(false);
            }
            else
            {
                flechaMusicaIqz.SetActive(true);
            }

            if (musica == 10)
            {
                flechaMusicaDer.SetActive(false);
            }
            else
            {
                flechaMusicaDer.SetActive(true);
            }

            //SFX

            if (sfx == 0)
            {
                flechaSFXIqz.SetActive(false);
            }
            else
            {
                flechaSFXIqz.SetActive(true);
            }

            if (sfx == 10)
            {
                flechaSFXDer.SetActive(false);
            }
            else
            {
                flechaSFXDer.SetActive(true);
            }

        }

    }

}
