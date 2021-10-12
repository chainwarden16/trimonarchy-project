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
    int contadorPrueba = 0;
    public GameObject casa;
    public GameObject pozo;
    public GameObject centroEntrenamiento;
    public GameObject soldadoEnemigo;
    public GameObject soldado;
    public Button botonCasa, botonPozo, botonCentro;
    public GameObject panelError, aceptarError, aceptarDerrota;
    public TextMeshProUGUI tituloError, textoError;
    public Text textoTutorial;

    void Start()
    {
        dialogo = FindObjectOfType<DialogoFunciones>();
        tileSuelo = GameObject.Find("Tilemap-Suelo").GetComponent<Tilemap>();
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
                contador++;
                Debug.Log("Deteniendo el tiempo");

                dialogo.MostrarDialogoSinEleccion();
            }

            if (contador == 5 && !CheckHaMovidoUnidad())
            {
                textoTutorial.text = "";
                textoTutorial.enabled = false;
                Debug.Log("Reanudando el tiempo (Movido Unidad)");
                Time.timeScale = 1f;
                dialogo.CerrarDialogoDondeEstaba();
            }

            if (contador == 8 && !CheckHaConstruidoEdificio())
            {
                Debug.Log("Reanudando el tiempo (Ha consturido edificio)");
                Time.timeScale = 1f;
                dialogo.CerrarDialogoDondeEstaba();
            }

            if (contador == 11 && !CheckHaConseguidoSoldados())
            {
                Debug.Log("Reanudando el tiempo (Ha conseguido soldados)");
                Time.timeScale = 1f;
                dialogo.CerrarDialogoDondeEstaba();
            }

            if (contador == 12 && !CheckHaMatadoEnemigo())
            {
                Debug.Log("Reanudando el tiempo (Ha matado enemigo)");
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
            /*Vector2 centroCasill = tileSuelo.GetCellCenterLocal(new Vector3Int(3, 3, 0));
            centroCasill = tileSuelo.GetCellCenterLocal(new Vector3Int(4, 4, 0));
            Instantiate(soldadoEnemigo, new Vector2(centroCasill.x, centroCasill.y + tileSuelo.layoutGrid.cellSize.y / 2), Quaternion.identity);*/
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
            Time.timeScale = 1f;
            SceneManager.LoadScene("MapaCiudad");
        }

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (contadorPrueba)
            {
                case 0:
                    Vector2 centroCasilla = tileSuelo.GetCellCenterLocal(new Vector3Int(3,3,0));
                    GameObject ca = Instantiate(casa, new Vector2(centroCasilla.x, centroCasilla.y + tileSuelo.layoutGrid.cellSize.y / 2), Quaternion.identity);
                    ca.GetComponent<Edificio>().haFinalizadoConstruccion = true;
                    contadorPrueba++;
                    break;
                case 1:
                    centroCasilla = tileSuelo.GetCellCenterLocal(new Vector3Int(4, 4, 0));
                    GameObject po = Instantiate(pozo, new Vector2(centroCasilla.x, centroCasilla.y + tileSuelo.layoutGrid.cellSize.y / 2), Quaternion.identity);
                    po.GetComponent<Edificio>().haFinalizadoConstruccion = true;
                    contadorPrueba++;
                    break;
                case 2:
                    centroCasilla = tileSuelo.GetCellCenterLocal(new Vector3Int(5, 5, 0));
                    GameObject ce = Instantiate(centroEntrenamiento, new Vector2(centroCasilla.x, centroCasilla.y + tileSuelo.layoutGrid.cellSize.y / 2), Quaternion.identity);
                    ce.GetComponent<Edificio>().haFinalizadoConstruccion = true;
                    centroCasilla = tileSuelo.GetCellCenterLocal(new Vector3Int(4, 4, 0));
                    Instantiate(soldado, new Vector2(centroCasilla.x, centroCasilla.y + tileSuelo.layoutGrid.cellSize.y / 2), Quaternion.identity);
                    contadorPrueba++;
                    break;
                case 3:
                    centroCasilla = tileSuelo.GetCellCenterLocal(new Vector3Int(7, 8, 0));
                    Instantiate(soldadoEnemigo, new Vector2(centroCasilla.x, centroCasilla.y + tileSuelo.layoutGrid.cellSize.y / 2), Quaternion.identity); 
                    contadorPrueba++;
                    break;
                case 4:
                    UnidadEnemiga soldadoE = FindObjectOfType<UnidadEnemiga>();
                    Destroy(soldadoE.gameObject);
                    contadorPrueba++;
                    break;
            }
        }
        */
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

        //Unidad[] unidadesEnCampo = FindObjectsOfType<Unidad>().Where( sol => sol.GetComponent<Unidad>().unidad.tipo != UnidadScriptable.TipoUnidad.Civil).ToArray();
        UnidadEnemiga[] enemigos = FindObjectsOfType<UnidadEnemiga>();
        int soldadosAliados = (int)Recursos.soldados;

        if (soldadosAliados > 0)
        {
            tieneSoldados = true;
            Vector2 centroCasilla = tileSuelo.GetCellCenterLocal(new Vector3Int(7, 10, 0));

            if (enemigos.Length == 0)
            {

                Instantiate(soldadoEnemigo, new Vector2(centroCasilla.x, centroCasilla.y + tileSuelo.layoutGrid.cellSize.y / 2), Quaternion.identity);

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
            textoError.text = "De alguna forma, has perdido en la fase de entrenamiento. Pero no te preocupes, puedes volver a intentarlo.";
        } else if (contador > 11 && Recursos.soldados <= 0 && unidadesEnCampo.Length <= 0)
        {
            Time.timeScale = 0f;
            panelError.SetActive(true);
            aceptarError.SetActive(false);
            aceptarDerrota.SetActive(true);

            tituloError.text = "Has empatado";
            textoError.text = "Has perdido a todos tus soldados y derrotaste a los oponentes. Aun así, no se considera que hayas ganado. Vuelve a intentarlo.";
        }

            return haMatado;
    }
}
