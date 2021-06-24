using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class BuildController : MonoBehaviour
{
    [Header("Datos del edificio a construir")]
    public GameObject edificioAConstruir;
    SpriteRenderer edificioRenderer;

    [Header("Tilemap donde se edificará")]
    Tilemap tileSuelo;

    [Header("Mensaje de error")]
    GameObject cajaMensaje;
    TextMeshProUGUI textoError;

    [Header("Game Manager")]
    GameManager manager;

    [Header("Comprobador de si el controlador de unidades está desactivado durante la construcción de un edificio")]
    UnidadController unidadCon;

    private void Start()
    {
        manager = GameManager.manager;
        edificioRenderer = gameObject.GetComponent<SpriteRenderer>();
        tileSuelo = GameObject.Find("Tilemap-Suelo").GetComponent<Tilemap>();
        edificioAConstruir = null;

        cajaMensaje = GameObject.Find("Panel-Error");
        textoError = GameObject.Find("Texto-Error").GetComponent<TextMeshProUGUI>();
        cajaMensaje.SetActive(false);

        unidadCon = GameObject.FindObjectOfType<UnidadController>();
        gameObject.GetComponent<BuildController>().enabled = false;

        /*Edificio ed = edificioAConstruir.GetComponent<Edificio>();
        EdificioScriptable edSc = ed.edificioData;
        Sprite edi = edSc.terminado;*/
    }

    void Update()
    {

        if (edificioAConstruir != null)

        {
            unidadCon.unidadesSeleccionadas.Clear();
            unidadCon.enabled = false;
            edificioRenderer.sprite = edificioAConstruir.GetComponent<Edificio>().edificioData.terminado;
            edificioRenderer.color = new Color(0, 1, 0, 1);
            //se activa el componente y se pone el color, el nuevo gráfico y el tipo de edificio desde el botón que se pulse
            gameObject.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

            if (Input.GetMouseButtonDown(0)) //TODO: comprobar que está dentro de la grid del tilemap, que se tengan materiales suficientes Y que no haya algo ya construido en esa zona de la grid del GameManager
                                             //Si no se puede contruir, aparecerá en rojo; si se puede, en verde
            {
                Vector2 vec = gameObject.transform.position;

                Vector3Int tpos = tileSuelo.WorldToCell(vec);

                gameObject.transform.position = tpos;

                // Try to get a tile from cell position
                bool condicion1 = edificioAConstruir.GetComponent<Edificio>().ComprobarConstruirEdificio(); //comprueba si se puede construir el edificio con los materiales actuales
                bool condicion2 = tileSuelo.HasTile(tpos); //mira si está dentro del Tilemap donde se permite construir
                bool condicion3 = manager.ComprobarCasillaVacia(tpos.x, tpos.y); //busca si es un punto vacío, donde no haya ya un edificio o una fuente de recursos

                if (condicion2) //condicion1 && condicion2 && condicion3
                {

                    edificioAConstruir.GetComponent<Edificio>().ConstruirEdificio();
                    GameManager.manager.ActualizarContadorRecursos();
                    edificioRenderer.color = new Color(1, 1, 1, 0);
                    Vector2 centroCasilla = tileSuelo.GetCellCenterLocal(tpos);
                    GameObject nuevoEdificio = Instantiate(edificioAConstruir, new Vector2(centroCasilla.x, centroCasilla.y+tileSuelo.layoutGrid.cellSize.y/2), Quaternion.identity);
                    //nuevoEdificio.GetComponent<Edificio>().ProcesoConstruccion();
                    
                    edificioAConstruir = null;
                    

                }
                else
                {
                    cajaMensaje.SetActive(true);
                    edificioRenderer.color = new Color(1, 1, 1, 0);
                    textoError.text = "";
                    if (!condicion1)
                    {
                        textoError.text += "-No tienes materiales suficientes.\n";
                    }
                    if (!condicion2)
                    {
                        textoError.text += "-Este lugar está fuera del límite.\n";
                    }
                    if (!condicion3)
                    {
                        textoError.text += "-Ese punto ya está ocupado.";
                    }
                    edificioAConstruir = null;
                    Time.timeScale = 0;
                }
                    unidadCon.enabled = true;
                    gameObject.GetComponent<BuildController>().enabled = false;

            }


        }
        else
        {
            edificioRenderer.color = new Color(0, 1, 0, 0);
        }
    }

 
}
