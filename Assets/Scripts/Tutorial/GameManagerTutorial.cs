using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class GameManagerTutorial : MonoBehaviour
{

    #region Variables

    [Header("Dimensiones de la cuadrícula, de la que el cursor no puede salir")]
    public int anchoGrid;
    public int largoGrid;
    public int[,] gridCiudad; //se debe comprobar en la clase del cursor que no se salga de estas medidas


    [Header("Control del estado del juego")]

    public List<TextMeshProUGUI> textoContador;
    public TextMeshProUGUI contadorCiviles;
    public TextMeshProUGUI contadorSoldados;
    public TextMeshProUGUI contadorTiempoRestante;
    bool haGanado = false;

    [Header("Manipulación de elementos del mapa")]
    GameObject textoMenuAcciones;
    int accionElegida;
    public List<GameObject> tiposEdificio;
    Tilemap suelo;
    Tilemap obstaculos;
    public Tile obstaculoInvisible;
    public GameObject recurso1, recurso2;
    public float probabilidadGeneracionRecurso;



    [Header("Enemigos presentes")]
    public List<UnidadEnemiga> unidadesEnemigas = new List<UnidadEnemiga>();

    public GameObject mago;
    public GameObject guerrero;

    #endregion

    #region Start y Update
    void Start()
    {
        suelo = GameObject.Find("Tilemap-Suelo").GetComponent<Tilemap>();
        obstaculos = GameObject.Find("Tilemap-Obstaculos").GetComponent<Tilemap>();

        gridCiudad = new int[anchoGrid, largoGrid];
        for (int i = anchoGrid - 1; i >= 0; i--)
        {
            for (int j = largoGrid - 1; j >= 0; j--) //TODO: se generan algunos elementos aleatorios que son recursos naturales (madera y piedra) por el mapa
            {

                if ((i < 6 || i > 11 || j < 6 || j > 13)) //no crea nada en la zona de spawn de unidades nuevas
                
                {

                    int contenido = Random.Range(0, 3);
                    float generacion = Random.Range(0f, 1.1f);

                    switch (contenido)
                    {
                        case 0: //nada
                            gridCiudad[i, j] = contenido;
                            break;
                        case 1: //madera

                            if (generacion <= probabilidadGeneracionRecurso)

                            {

                                Vector2 centroCasilla = suelo.GetCellCenterWorld(new Vector3Int(i, j, 0));
                                Vector2 centroCasillaObstaculo = obstaculos.GetCellCenterWorld(new Vector3Int(i, j, 0));

                                GameObject recurso = Instantiate(recurso1, new Vector2(centroCasilla.x, centroCasilla.y - 0.15f), Quaternion.identity);

                                gridCiudad[i, j] = contenido;
                                obstaculos.SetTile(obstaculos.WorldToCell(centroCasillaObstaculo), obstaculoInvisible);

                                recurso.GetComponent<SpriteRenderer>().sortingOrder = anchoGrid - j;
                            }
                            else
                            {
                                gridCiudad[i, j] = 0;
                            }

                            break;
                        case 2: //piedra
                            if (generacion <= probabilidadGeneracionRecurso)

                            {
                                Vector2 centroCasilla = suelo.GetCellCenterWorld(new Vector3Int(i, j, (int)suelo.transform.position.z));
                                Vector2 centroCasillaObstaculo = obstaculos.GetCellCenterWorld(new Vector3Int(i, j, 0));
                                
                                GameObject recurso = Instantiate(recurso2, new Vector2(centroCasilla.x, centroCasilla.y), Quaternion.identity);

                                gridCiudad[i, j] = contenido;
                                obstaculos.SetTile(obstaculos.WorldToCell(centroCasillaObstaculo), obstaculoInvisible);

                                recurso.GetComponent<SpriteRenderer>().sortingOrder = anchoGrid - j;
                            }

                            break;
                    }

                }


            }
        }

        ActualizarContadorRecursos();
        FindObjectOfType<NavMeshSurface2d>().BuildNavMesh();
    }


    // Update is called once per frame
    void Update()
    {
        
        //PruebaABorrarLuego();
    }

    #endregion


    #region Condicion de Victoria y de Derrota

    public void CargarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    public void CargarEscenaFinPartida()
    {

        if (haGanado)
        {
            SceneManager.LoadScene("Victoria"); //Pantalla de victoria
        }
        else
        {

            SceneManager.LoadScene("FinPartida"); //Game Over

        }

    }


    #endregion

    #region Actualizacion de UI

    public void ActualizarContadorRecursos()
    {
        for (int i = 0; i < Recursos.recursos.Count; i++)
        {
            textoContador[i].GetComponent<TextMeshProUGUI>().text = Recursos.recursos[i].ToString();
        }

        contadorCiviles.text = Recursos.habitantes.ToString();
        contadorSoldados.text = Recursos.soldados.ToString();

    }

    #endregion

    #region Comprobacion de elementos del mapa

    public bool ComprobarCasillaVacia(int fila, int columna)
    {
        bool estaVacia = true;
        if (fila < anchoGrid && columna < largoGrid && fila >= 0 && columna >= 0)
        {
            if (gridCiudad[fila, columna] != 0)
            {
                estaVacia = false;
            }
        }


        return estaVacia;
    }

    public void RellenarCasillaGrid(int x, int y, int numero)

    {
        if (x < anchoGrid && y < largoGrid && x >= 0 && y >= 0)
        {
            gridCiudad[x, y] = numero;
        }
    }

    #endregion

}
