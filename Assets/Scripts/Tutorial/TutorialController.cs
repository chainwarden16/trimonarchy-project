using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    [Header("Elementos que controlan el diálogo del tutorial")]
    GameObject unidad;
    Tilemap tileSuelo;

    DialogoFunciones dialogo;
    [SerializeField]
    int contador = 1;

    [Header("Elementos que aparecen en el mapa")]
    public GameObject casa;
    public GameObject pozo;
    public GameObject centroEntrenamiento;
    public GameObject soldadoEnemigo;
    public GameObject soldado;
    public Button botonCasa, botonPozo, botonCentro;
    public GameObject panelError, aceptarError, aceptarDerrota;
    public TextMeshProUGUI tituloError, textoError;
    public Text textoTutorial;

    [Header("Padre de los enemigos a invocar, para poner orden en la jerarquía")]
    GameObject padreEnemigos;

    [Header("SFX")]
    AudioController audioC;
    public AudioClip confirmar;

    void Start()
    {
        audioC = FindObjectOfType<AudioController>();

        dialogo = FindObjectOfType<DialogoFunciones>();
        tileSuelo = GameObject.Find("Tilemap-Suelo").GetComponent<Tilemap>();

        padreEnemigos = GameObject.Find("--unidades--");
    }


    void Update()
    {
        if(contador == 1)
        {
            Time.timeScale = 0f;
        }

        if (Input.GetMouseButtonDown(0) && dialogo.panelDialogo.activeSelf)
        {

            if (contador < 5 || (contador >= 5 && contador < 8) || (contador >= 8 && contador < 15) || (contador > 15))
            {
                if(audioC != null)
                {
                    audioC.PlaySFX(confirmar);
                }

                contador++;

                dialogo.MostrarDialogoSinEleccion();
            }

            if (contador == 5 && !CheckHaMovidoUnidad())
            {
                textoTutorial.text = "";
                textoTutorial.enabled = false;

                Time.timeScale = 1f;
                dialogo.CerrarDialogoDondeEstaba();
            }

            if (contador == 8 && !CheckHaConstruidoEdificio())
            {

                Time.timeScale = 1f;
                dialogo.CerrarDialogoDondeEstaba();
            }

            if (contador == 11 && !CheckHaConseguidoSoldados())
            {

                Time.timeScale = 1f;
                dialogo.CerrarDialogoDondeEstaba();
            }

            if (contador == 12 && !CheckHaMatadoEnemigo())
            {

                Time.timeScale = 1f;
                dialogo.CerrarDialogoDondeEstaba();
            }


        }

        if (contador == 5 && CheckHaMovidoUnidad())
        {
            dialogo.AbrirDialogoDondeEstaba();
            Time.timeScale = 0f;
            if (Input.GetMouseButtonDown(0))
            {
                dialogo.MostrarDialogoSinEleccion();
            }

        }

        if (contador == 8 && CheckHaConstruidoEdificio())
        {
            dialogo.AbrirDialogoDondeEstaba();
            Time.timeScale = 0f;
            if (Input.GetMouseButtonDown(0))
            {
                dialogo.MostrarDialogoSinEleccion();
            }
        }

        if (contador == 11 && CheckHaConseguidoSoldados())
        {
            Time.timeScale = 0f;
            dialogo.AbrirDialogoDondeEstaba();
            
            if (Input.GetMouseButtonDown(0))
            {
                dialogo.MostrarDialogoSinEleccion();
            }
        }

        if (contador == 12 && CheckHaMatadoEnemigo())
        {
            dialogo.AbrirDialogoDondeEstaba();
            Time.timeScale = 0f;
            if (Input.GetMouseButtonDown(0))
            {
                dialogo.MostrarDialogoSinEleccion();
            }
        }

        if(contador >= 15)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MapaCiudad");
        
        }

        //Se proporciona un botón para saltarse el tutorial e ir directamente a la acción

        if (Input.GetKeyDown(KeyCode.T))
        {
            if(audioC != null)
            {
                audioC.PlaySFX(confirmar);
            }
            Time.timeScale = 1f;
            SceneManager.LoadScene("MapaCiudad");
        }

    }

    bool CheckHaMovidoUnidad()
    {
        //Tiene que construir algo

        bool haMovido = false;

        Edificio build = FindObjectOfType<Edificio>();
        if (build != null)
        {
            haMovido = FindObjectOfType<Edificio>().haFinalizadoConstruccion;
            if (haMovido)
            {
                botonCasa.interactable = false;
                botonPozo.interactable = true;
            }
        }

        return haMovido;
    }

    bool CheckHaConstruidoEdificio()
    {
        bool haMovido = false;

        Edificio[] build = FindObjectsOfType<Edificio>();
        if (build != null)
        {
            foreach (Edificio edi in build)
            {
                if (edi.edificioData.nombre == "Pozo" && edi.haFinalizadoConstruccion)
                {
                    Recursos.SetRecursos(new List<int>() { 200, 200, 200, 200, 200, 200, 200, 200 });
                    haMovido = true;
                    botonPozo.interactable = false;
                    botonCentro.interactable = true;
                }
            }
        }

        return haMovido;
    }

    bool CheckHaConseguidoSoldados()
    {
        bool tieneSoldados = false;

        UnidadEnemiga[] enemigos = FindObjectsOfType<UnidadEnemiga>();
        int soldadosAliados = (int)Recursos.soldados;

        if (soldadosAliados > 0)
        {
            tieneSoldados = true;
            Vector2 centroCasilla = tileSuelo.GetCellCenterLocal(new Vector3Int(7, 10, 0));

            if (enemigos.Length == 0)
            {

                GameObject ene = Instantiate(soldadoEnemigo, new Vector2(centroCasilla.x, centroCasilla.y + tileSuelo.layoutGrid.cellSize.y / 2), Quaternion.identity);
                GameObject ene2 = Instantiate(soldadoEnemigo, new Vector2(centroCasilla.x+1, centroCasilla.y + 1 + tileSuelo.layoutGrid.cellSize.y / 2), Quaternion.identity);

                if (padreEnemigos != null)
                {
                    ene.transform.SetParent(padreEnemigos.transform, true);
                    ene2.transform.SetParent(padreEnemigos.transform, true);
                }

            }
        }

        return tieneSoldados;
    }

    bool CheckHaMatadoEnemigo()
    {
        UnidadEnemiga[] unidadesEnCampo = FindObjectsOfType<UnidadEnemiga>();

        bool haMatado = false;

        if (contador > 11 && unidadesEnCampo.Length == 0)
        {
            haMatado = true;
        }
        else if (contador > 11 && Recursos.soldados <= 0 && unidadesEnCampo.Length > 0)
        {
            //Por alguna razón, el jugador ha perdido, así que se le notifica
            Time.timeScale = 0f;
            panelError.SetActive(true);
            aceptarError.SetActive(false);
            aceptarDerrota.SetActive(true);

            tituloError.text = "Has fallado el tutorial";
            textoError.text = "De alguna forma, has perdido en el tutorial. Pero no importa, puedes volver a intentarlo.";
        } else if (contador > 11 && Recursos.soldados <= 0 && unidadesEnCampo.Length <= 0)
        {
            Time.timeScale = 0f;
            panelError.SetActive(true);
            aceptarError.SetActive(false);
            aceptarDerrota.SetActive(true);

            tituloError.text = "Has empatado";
            textoError.text = "Aun así, no se considera que hayas ganado. Vuelve a intentarlo.";
        }

            return haMatado;
    }
}
