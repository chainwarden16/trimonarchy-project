using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOpcionesGameplay : MonoBehaviour
{
    #region Variables
    public GameObject flechaMusicaIqz;
    public GameObject flechaMusicaDer;
    public GameObject flechaSFXIqz;
    public GameObject flechaSFXDer;
    public GameObject cursor;
    public GameObject contenedorMenu;
    public Text musicaNum;
    public Text sfxNum;

    MenuPausa menuPausa;

    [Header("Audio")]
    public AudioClip clipPrueba;
    GameObject objetoSonidos;
    AudioSource sourceMusica, sourceSFX;
    public AudioClip moverCursorSFX;
    public AudioClip seleccionar;

    [SerializeField]
    int lugarCursor = 0;
    int musica = 0;
    int sfx = 0;
    int moverCursor = 43;

    #endregion

    void Start()
    {
        //Los números no se actualizan. Haz que se pongan correctamente cada vez que se abra el menú
        musicaNum.text = PlayerPrefs.GetInt("Musica", 10).ToString();
        sfxNum.text = PlayerPrefs.GetInt("SFX", 10).ToString();
        musica = PlayerPrefs.GetInt("Musica", 10);
        sfx = PlayerPrefs.GetInt("SFX", 10);
        menuPausa = GetComponent<MenuPausa>();

        objetoSonidos = GameObject.Find("--Musica--");
        sourceMusica = objetoSonidos.GetComponent<AudioController>().sourceMusica;
        sourceSFX = objetoSonidos.GetComponent<AudioController>().sourceSFX;

        ComprobarFlechasMenu();

    }


    void Update()
    {
        //TODO: poner un audioclip con la musica al volumen cuando se ajuste al pulsar una tecla
        if (cursor != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && lugarCursor > 0)
            {
                lugarCursor--;
                objetoSonidos.GetComponent<AudioController>().PlaySFX(moverCursorSFX);
                if (lugarCursor == 1)
                {
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-344, 115, 0);
                }
                else
                {

                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(cursor.GetComponent<RectTransform>().anchoredPosition.x, cursor.GetComponent<RectTransform>().anchoredPosition.y + moverCursor, 0);
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && lugarCursor < 3)
            {
                objetoSonidos.GetComponent<AudioController>().PlaySFX(moverCursorSFX);
                lugarCursor++;
                if (lugarCursor == 2)
                {
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-198, -75, 0);
                }
                else
                {
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(cursor.GetComponent<RectTransform>().anchoredPosition.x, cursor.GetComponent<RectTransform>().anchoredPosition.y - moverCursor, 0);
                }
            }
        }

        switch (lugarCursor)
        {
            case 0:

                if (Input.GetKeyDown(KeyCode.LeftArrow) && musica > 0)
                {
                    musica--;
                    sourceMusica.volume -= 0.1f;
                }

                if (Input.GetKeyDown(KeyCode.RightArrow) && musica < 10)
                {
                    musica++;
                    sourceMusica.volume += 0.1f;
                }

                ComprobarFlechasMenu();

                musicaNum.text = musica.ToString();

                break;

            case 1:

                if (Input.GetKeyDown(KeyCode.LeftArrow) && sfx > 0)
                {
                    sfx--;
                    sourceSFX.volume -= 0.1f;
                    sourceSFX.PlayOneShot(clipPrueba);
                }

                if (Input.GetKeyDown(KeyCode.RightArrow) && sfx < 10)
                {
                    sfx++;
                    sourceSFX.volume += 0.1f;
                    sourceSFX.PlayOneShot(clipPrueba);
                }

                ComprobarFlechasMenu();

                sfxNum.text = sfx.ToString();
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    objetoSonidos.GetComponent<AudioController>().PlaySFX(seleccionar);
                    PlayerPrefs.SetInt("Musica", musica);
                    PlayerPrefs.SetInt("SFX", sfx);
                    menuPausa.estaViendoOpciones = false;
                    contenedorMenu.SetActive(false);
                    lugarCursor = 0;
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-344, 158, 0);
                    this.enabled = false;
                }
                break;

            case 3:
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    objetoSonidos.GetComponent<AudioController>().PlaySFX(seleccionar);
                    menuPausa.estaViendoOpciones = false;
                    contenedorMenu.SetActive(false);
                    lugarCursor = 0;
                    cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-344, 158, 0);

                    musicaNum.text = PlayerPrefs.GetInt("Musica", 10).ToString();
                    sfxNum.text = PlayerPrefs.GetInt("SFX", 10).ToString();
                    musica = PlayerPrefs.GetInt("Musica", 10);
                    sfx = PlayerPrefs.GetInt("SFX", 10);
                    this.enabled = false;
                }
                break;

        }
    }

    /// <summary>
    /// Esta función hace visible o invisible las flechas de selección de volumen de los elementos
    /// </summary>
    void ComprobarFlechasMenu()
    {
        if (flechaMusicaIqz != null && flechaMusicaDer != null)
        {

            if (musica == 0)
            {
                flechaMusicaIqz.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            else
            {
                flechaMusicaIqz.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }

            if (musica == 10)
            {
                flechaMusicaDer.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            else
            {
                flechaMusicaDer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }


        }

        if (flechaMusicaIqz != null && flechaMusicaDer != null)
        {

            if (sfx == 0)
            {
                flechaSFXIqz.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            else
            {
                flechaSFXIqz.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }

            if (sfx == 10)
            {
                flechaSFXDer.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            else
            {
                flechaSFXDer.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }

        }

    }
}
