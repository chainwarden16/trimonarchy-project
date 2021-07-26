using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    #region Variables

    [Header("Dimensiones de la cuadrícula, de la que el cursor no puede salir")]
    public int anchoGrid;
    public int largoGrid;
    public int[,] gridCiudad; //se debe comprobar en la clase del cursor que no se salga de estas medidas

    [Header("Control del estado del juego")]
    float tiempoRestante = 360; //en segundos
    int numeroSoldadosNecesario = 1; //los magos también entran aquí
    public List<TextMeshProUGUI> textoContador;
    public TextMeshProUGUI contadorCiviles;
    public TextMeshProUGUI contadorSoldados;
    public TextMeshProUGUI contadorTiempoRestante;
    public GameObject panelFinPartida;
    public TextMeshProUGUI tituloFinPartida;
    public TextMeshProUGUI textoFinPartida;
    bool seHanCreadoEnemigos = false;
    bool haGanado = false;

    [Header("Manipulación de elementos del mapa")]
    GameObject textoMenuAcciones;
    int accionElegida;
    public List<GameObject> tiposEdificio;
    Tilemap suelo;
    public GameObject recurso1, recurso2;
    public float probabilidadGeneracionRecurso;

    [Header("Enemigos presentes")]
    public List<UnidadEnemiga> unidadesEnemigas = new List<UnidadEnemiga>();
    int numeroEnemigosACrear = 6;
    public GameObject mago;
    public GameObject guerrero;

    #endregion

    #region Singleton Game Manager

    [Header("Game manager")]
    public static GameManager manager; //sólo debe haber uno en el juego activo en cada momento

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(manager);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region Start y Update
    void Start()
    {
        suelo = suelo = GameObject.Find("Tilemap-Suelo").GetComponent<Tilemap>();
        panelFinPartida.SetActive(false);

        gridCiudad = new int[anchoGrid, largoGrid];
        for (int i = anchoGrid - 1; i >= 0; i--)
        {
            for (int j = largoGrid - 1; j >= 0; j--) //TODO: se generan algunos elementos aleatorios que son recursos naturales (madera y piedra) por el mapa
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

                            Vector2 centroCasilla = suelo.GetCellCenterLocal(new Vector3Int(i, j, (int)suelo.transform.position.z));
                            Instantiate(recurso1, new Vector2(centroCasilla.x, centroCasilla.y), Quaternion.identity);
                            gridCiudad[i, j] = contenido;
                        }
                        else
                        {
                            gridCiudad[i, j] = 0;
                        }

                        break;
                    case 2: //piedra
                        if (generacion <= probabilidadGeneracionRecurso)

                        {
                            Vector2 centroCasilla = suelo.GetCellCenterLocal(new Vector3Int(i, j, (int)suelo.transform.position.z));
                            Instantiate(recurso2, new Vector2(centroCasilla.x, centroCasilla.y), Quaternion.identity);
                            gridCiudad[i, j] = contenido;
                        }

                        break;
                }

            }
        }

        ActualizarContadorRecursos();
    }


    // Update is called once per frame
    void Update()
    {
        DeterminarCondicionVictoriaDerrota();
        //PruebaABorrarLuego();
    }

    #endregion


    #region Condicion de Victoria y de Derrota

    private void PruebaABorrarLuego()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Recursos.soldados--;
            Debug.Log(Recursos.soldados);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            unidadesEnemigas.Remove(unidadesEnemigas[0]);
            Debug.Log(unidadesEnemigas.Count);
        }
    }


    private void DeterminarCondicionVictoriaDerrota()
    {
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0)
        {
            contadorTiempoRestante.text = "0:00";
            if (Recursos.soldados < numeroSoldadosNecesario && !seHanCreadoEnemigos)
            {

                InvocarPanelFinPartida(0);
                //Aparece el  mensaje de Game Over para hacerle clic. Eso será lo que te lleve a la escena de Fin de Partida
                Debug.Log("Has perdido, jajaja");
            }
            else
            {
                //Si tienes soldados suficientes, se crean varios enemigos una vez
                if (!seHanCreadoEnemigos)
                {
                    //se crean enemigos en distintas posiciones
                    for (int i = 0; i < numeroEnemigosACrear; i++)
                    {
                        int rand = Random.Range(0, 2);

                        if (rand == 0)
                        {

                            GameObject unidad = Instantiate(mago, new Vector2(i, i), Quaternion.identity);
                            unidadesEnemigas.Add(unidad.GetComponent<UnidadEnemiga>());

                        }
                        else
                        {
                            GameObject unidad = Instantiate(guerrero, new Vector2(i, i), Quaternion.identity);
                            unidadesEnemigas.Add(unidad.GetComponent<UnidadEnemiga>());
                        }

                    }
                    seHanCreadoEnemigos = true;
                }

                if (Recursos.soldados <= 0)
                {
                    InvocarPanelFinPartida(1);
                    //Aparece el  mensaje de Game Over para hacerle clic. Eso será lo que te lleve a la escena de Fin de Partida
                    Debug.Log("Has perdido porque no tienes soldados vivos, jajaja");
                }
                else if (unidadesEnemigas.Count == 0 && seHanCreadoEnemigos)
                {
                    //¡Victoria! Los enemigos han muerto y tú todavía tienes soldados en pie
                    haGanado = true;
                    InvocarPanelFinPartida(2);
                }


            }


        }
        else
        {
            float minutes = Mathf.Floor(tiempoRestante / 60);
            float seconds = Mathf.RoundToInt(tiempoRestante % 60);

            if (seconds == 60)
            {
                seconds = 0;
                minutes = Mathf.Floor(tiempoRestante / 60 + 1);
            }

            contadorTiempoRestante.text = minutes + ":" + seconds.ToString("00");

        }
    }

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

    private void InvocarPanelFinPartida(int numero)
    {
        Time.timeScale = 0;
        switch (numero)
        {
            case 0:

                break;
            case 1:
                tituloFinPartida.text = "Derrota";
                textoFinPartida.text = "Tus soldados han muerto y no queda nadie que defienda la ciudad.";
                break;
            case 2:
                tituloFinPartida.text = "¡Victoria!";
                textoFinPartida.text = "Tus soldados han resistido el ataque y el reino enemigo se ha rendido. ¡Has ganado!";
                break;
        }
        if (panelFinPartida != null)
        {

            panelFinPartida.SetActive(true);
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
