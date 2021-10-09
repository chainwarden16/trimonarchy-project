using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

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


    void Start()
    {
        dialogo = FindObjectOfType<DialogoFunciones>();
        tileSuelo = GameObject.Find("Tilemap-Suelo").GetComponent<Tilemap>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialogo.panelDialogo.activeSelf)
        {

            if (contador < 5 || (contador >= 5 && contador < 8) || (contador >= 8 && contador < 15) || (contador > 15))
            {
                contador++;
                dialogo.MostrarDialogoSinEleccion();
            }

            if (contador == 5 && !CheckHaMovidoUnidad())
            {
                dialogo.CerrarDialogoDondeEstaba();
            }

            if (contador == 8 && !CheckHaConstruidoEdificio())
            {
                dialogo.CerrarDialogoDondeEstaba();
            }

            if (contador == 11 && !CheckHaConseguidoSoldados())
            {
                dialogo.CerrarDialogoDondeEstaba();
            }

            if (contador == 12 && !CheckHaMatadoEnemigo())
            {
                dialogo.CerrarDialogoDondeEstaba();
            }


        }

        if (contador == 5 && CheckHaMovidoUnidad())
        {
            dialogo.AbrirDialogoDondeEstaba();

            if (Input.GetMouseButtonDown(0))
            {
                dialogo.MostrarDialogoSinEleccion();
            }

        }

        if (contador == 8 && CheckHaConstruidoEdificio())
        {
            dialogo.AbrirDialogoDondeEstaba();

            if (Input.GetMouseButtonDown(0))
            {
                dialogo.MostrarDialogoSinEleccion();
            }
        }

        if (contador == 11 && CheckHaConseguidoSoldados())
        {

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

            if (Input.GetMouseButtonDown(0))
            {
                dialogo.MostrarDialogoSinEleccion();
            }
        }

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
                    centroCasilla = tileSuelo.GetCellCenterLocal(new Vector3Int(4, 4, 0));
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

    }

    bool CheckHaMovidoUnidad()
    {
        //Tiene que construir algo

        bool haMovido = false;

        Edificio build = FindObjectOfType<Edificio>();
        if (build != null)
        {
            haMovido = FindObjectOfType<Edificio>().haFinalizadoConstruccion;

        }

        return haMovido;
    }

    bool CheckHaConstruidoEdificio()
    {
        bool haMovido = false;

        Edificio[] build = FindObjectsOfType<Edificio>();
        if (build != null)
        {
            foreach(Edificio edi in build)
            {
                if(edi.edificioData.nombre == "Pozo" && edi.haFinalizadoConstruccion)
                {
                    haMovido = true;
                }
            }
        }

        return haMovido;
    }

    bool CheckHaConseguidoSoldados()
    {
        bool tieneSoldados = false;

        Unidad[] unidadesEnCampo = FindObjectsOfType<Unidad>().Where( sol => sol.GetComponent<Unidad>().unidad.tipo != UnidadScriptable.TipoUnidad.Civil).ToArray();

        if(unidadesEnCampo.Length != 0)
        {
            tieneSoldados = true;
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

        return haMatado;
    }
}
