using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    #region Variables

    [Header("Dimensiones de la cuadrícula, de la que el cursor no puede salir")]
    public int anchoGrid;
    public int largoGrid;
    public int[,] gridCiudad; //se debe comprobar en la clase del cursor que no se salga de estas medidas

    [Header("Control del estado del juego")]
    float tiempoInicial = 1200; //en segundos
    float tiempoRestante = 1200; //en segundos
    float tiempoDerrotarEnemigos = 0f;
    int numeroSoldadosNecesario = 24; // 20 //los magos también entran aquí
    public List<TextMeshProUGUI> textoContador;
    public TextMeshProUGUI contadorCiviles;
    public TextMeshProUGUI contadorSoldados;
    public TextMeshProUGUI contadorTiempoRestante;
    public GameObject panelFinPartida;
    public TextMeshProUGUI tituloFinPartida;
    public TextMeshProUGUI textoFinPartida;

    public Button botonConstruirCentro;
    public Button botonConstruirEscuela;
    public GameObject panelConstruccion;

    bool seHanCreadoEnemigos = false;
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
    GameObject padreRecursos;
    GameObject padreEnemigos;

    [Header("Enemigos presentes")]
    public List<UnidadEnemiga> unidadesEnemigas = new List<UnidadEnemiga>();
    int numeroEnemigosACrear = 24; //16
    public GameObject mago;
    public GameObject guerrero;

    [Header("Música y SFX")]
    AudioController audioC;

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
        List<FuenteRecursosOperaciones> arboles = new List<FuenteRecursosOperaciones>();
        List<FuenteRecursosOperaciones> rocas = new List<FuenteRecursosOperaciones>();

        audioC = FindObjectOfType<AudioController>();

        suelo = GameObject.Find("Tilemap-Suelo").GetComponent<Tilemap>();
        obstaculos = GameObject.Find("Tilemap-Obstaculos").GetComponent<Tilemap>();
        Recursos.SetRecursos(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 });
        Recursos.SetHabitantes(1);
        Recursos.SetSoldados(0); //0

        gridCiudad = new int[anchoGrid, largoGrid];

        padreEnemigos = GameObject.Find("--unidades--");
        padreRecursos = GameObject.Find("--fuente recursos--");

        //Se puebla el mapa con rocas y árboles de forma procedural

        for (int i = anchoGrid - 1; i >= 0; i--)
        {
            for (int j = largoGrid - 1; j >= 0; j--)
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
                                arboles.Add(recurso.GetComponent<FuenteRecursosOperaciones>());

                                //Se coloca en el objeto padre correspondiente en la escena, por mantener orden
                                if (padreRecursos != null)
                                {
                                    recurso.transform.SetParent(padreRecursos.transform, true);
                                }

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
                                rocas.Add(recurso.GetComponent<FuenteRecursosOperaciones>());

                                //Se coloca en el objeto padre correspondiente en la escena, por mantener orden
                                if (padreRecursos != null)
                                {
                                    recurso.transform.SetParent(padreRecursos.transform, true);
                                }

                                gridCiudad[i, j] = contenido;
                                obstaculos.SetTile(obstaculos.WorldToCell(centroCasillaObstaculo), obstaculoInvisible);

                                recurso.GetComponent<SpriteRenderer>().sortingOrder = anchoGrid - j;
                            }

                            break;
                    }

                }


            }
        }

        //se procede a contar cuántas piedras y cuántos árboles se han generado. Si son menos de 45 árboles y menos de 42 rocas, se deberá crear más hasta llegar a esa cantidad
        //de no hacerse, podría no haber suficiente material para crear 5 copias de cada edificio

        if (arboles.Count < 45 || rocas.Count < 42)
        {
            int numeroArboles = arboles.Count;
            int numeroRocas = rocas.Count;
            Debug.Log("Hay " + numeroArboles + "árboles y " + numeroRocas);

            for (int i = 0; i < largoGrid; i++)
            {
                for (int j = 0; j < anchoGrid; j++)
                {
                    if (numeroArboles < 45 && gridCiudad[i, j] == 0)
                    {
                        Vector2 centroCasilla = suelo.GetCellCenterWorld(new Vector3Int(i, j, 0));
                        Vector2 centroCasillaObstaculo = obstaculos.GetCellCenterWorld(new Vector3Int(i, j, 0));

                        GameObject recurso = Instantiate(recurso1, new Vector2(centroCasilla.x, centroCasilla.y - 0.15f), Quaternion.identity);

                        //Se coloca en el objeto padre correspondiente en la escena, por mantener orden
                        if (padreRecursos != null)
                        {
                            recurso.transform.SetParent(padreRecursos.transform, true);
                        }

                        gridCiudad[i, j] = 1;
                        obstaculos.SetTile(obstaculos.WorldToCell(centroCasillaObstaculo), obstaculoInvisible);

                        recurso.GetComponent<SpriteRenderer>().sortingOrder = anchoGrid - j;
                        numeroArboles++;
                    }

                    else if (numeroRocas < 42 && gridCiudad[i, j] == 0)
                    {
                        Vector2 centroCasilla = suelo.GetCellCenterWorld(new Vector3Int(i, j, 0));
                        Vector2 centroCasillaObstaculo = obstaculos.GetCellCenterWorld(new Vector3Int(i, j, 0));

                        GameObject recurso = Instantiate(recurso2, new Vector2(centroCasilla.x, centroCasilla.y - 0.15f), Quaternion.identity);

                        //Se coloca en el objeto padre correspondiente en la escena, por mantener orden
                        if (padreRecursos != null)
                        {
                            recurso.transform.SetParent(padreRecursos.transform, true);
                        }

                        gridCiudad[i, j] = 2;
                        obstaculos.SetTile(obstaculos.WorldToCell(centroCasillaObstaculo), obstaculoInvisible);

                        recurso.GetComponent<SpriteRenderer>().sortingOrder = anchoGrid - j;
                        numeroRocas++;
                    }
                    else if (numeroArboles >= 45 && numeroRocas >= 42)
                    {
                        break;
                    }
                }
            }

        }

        ActualizarContadorRecursos();
        FindObjectOfType<NavMeshSurface2d>().BuildNavMesh();

        if (audioC != null)
        {
            audioC.PlaySong(audioC.musicaConstruccion);
        }

    }


    void Update()
    {
        DeterminarCondicionVictoriaDerrota();
        /*BotonDerrotaTemporal();
        BotonVictoriaTemporal();
        MuchosRecursos();
        SaltarMinutos();*/
    }

    #endregion


    #region Metodos para probar escenarios rapido

    public void MuchosRecursos()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Recursos.SetRecursos(new List<int>() { 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000 });
        }
    }

    public void SaltarMinutos()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            tiempoRestante -= 120f;
        }
    }

    public void BotonVictoriaTemporal()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            int rand = UnityEngine.Random.Range(0, 61);
            int tiempoRand = UnityEngine.Random.Range(3, 100);

            float minutos = Mathf.Floor(tiempoRand / 60);
            float segundos = Mathf.RoundToInt(tiempoRand % 60);

            if (segundos == 60)
            {
                segundos = 0;
                minutos = Mathf.Floor(tiempoRand / 60 + 1);
            }

            string tiempoPrueba = minutos + ":" + segundos.ToString("00");

            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("SoldadosVivos", rand);

            PlayerPrefs.SetString("TiempoRecord", tiempoPrueba);

            SceneManager.LoadScene("Victoria");
        }
    }

    public void BotonDerrotaTemporal()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("FinPartida");
        }
    }

    #endregion



    #region Condicion de Victoria y de Derrota


    private void DeterminarCondicionVictoriaDerrota()
    {

        if (contadorTiempoRestante != null)
        {

            if (tiempoRestante <= 0)
            {
                contadorTiempoRestante.text = "0:00";
                if (Recursos.soldados < numeroSoldadosNecesario && !seHanCreadoEnemigos) //
                {

                    InvocarPanelFinPartida(0);
                    //Aparece el  mensaje de Game Over para hacerle clic. Eso será lo que te lleve a la escena de Fin de Partida

                }
                else
                {

                    //Si tienes soldados suficientes, se crean varios enemigos una vez
                    if (!seHanCreadoEnemigos)
                    {
                        int contadorSoldados = 0;
                        int k = 7;
                        int j = 10;

                        Unidad[] unidadesAliadas = FindObjectsOfType<Unidad>();

                        List<Vector3> posiciones = new List<Vector3>();

                        foreach (Unidad un in unidadesAliadas)
                        {

                            Vector3 transTile = suelo.GetCellCenterWorld(new Vector3Int((int)un.transform.position.x, (int)un.transform.position.y, 0));

                            posiciones.Add(transTile);

                        }

                        while (contadorSoldados < numeroEnemigosACrear)
                        {
                            Vector3 lugarSpawn = suelo.GetCellCenterWorld(new Vector3Int(k, j, 0));

                            int rand = Random.Range(0, 2);

                            if (rand == 0)
                            {

                                if (gridCiudad[k, j] == 0 && !posiciones.Contains(lugarSpawn))
                                {
                                    GameObject enemigo = Instantiate(mago, lugarSpawn, Quaternion.identity);
                                    unidadesEnemigas.Add(enemigo.GetComponent<UnidadEnemiga>());

                                    //Se coloca en el objeto padre correspondiente en la escena, por mantener orden
                                    if (padreEnemigos != null)
                                    {
                                        enemigo.transform.SetParent(padreRecursos.transform, true);
                                    }

                                    contadorSoldados++;
                                    if (contadorSoldados == numeroEnemigosACrear)
                                    {
                                        break;

                                    }

                                }

                            }
                            else
                            {
                                if (gridCiudad[k, j] == 0 && !posiciones.Contains(lugarSpawn))
                                {
                                    GameObject enemigo = Instantiate(guerrero, lugarSpawn, Quaternion.identity);
                                    unidadesEnemigas.Add(enemigo.GetComponent<UnidadEnemiga>());

                                    //Se coloca en el objeto padre correspondiente en la escena, por mantener orden
                                    if (padreEnemigos != null)
                                    {
                                        enemigo.transform.SetParent(padreRecursos.transform, true);
                                    }

                                    contadorSoldados++;
                                    if (contadorSoldados == numeroEnemigosACrear)
                                    {
                                        break;

                                    }

                                }

                            }

                            k++;
                            if (k >= 20)
                            {
                                j++;
                                k = 7;
                            }
                        }
                        seHanCreadoEnemigos = true;

                        //Se bloquean los botones para construir nuevas escuelas de magia y campos de entrenamiento

                        if (audioC != null)
                        {
                            audioC.PlaySong(audioC.musicaCombate);
                        }

                        botonConstruirCentro.interactable = false;
                        botonConstruirEscuela.interactable = false;

                        //se evita que se pueda construir cualquier cosa una vez empieza el combate que no se haya seleccionado o cimentado ya

                        panelConstruccion.SetActive(false);


                    }

                    tiempoDerrotarEnemigos += Time.deltaTime;

                    float minutes = Mathf.Floor(tiempoDerrotarEnemigos / 60);
                    float seconds = Mathf.RoundToInt(tiempoDerrotarEnemigos % 60);

                    if (seconds == 60)
                    {
                        seconds = 0;
                        minutes = Mathf.Floor(tiempoDerrotarEnemigos / 60 + 1);
                    }

                    contadorTiempoRestante.text = minutes + ":" + seconds.ToString("00");

                    if (Recursos.soldados <= 0)
                    {
                        InvocarPanelFinPartida(1);
                        //Aparece el  mensaje de Game Over para hacerle clic. Eso será lo que te lleve a la escena de Fin de Partida

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
                tiempoRestante -= Time.deltaTime;

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
                //float tiempoGastado = tiempoInicial - tiempoRestante;

                //Si el tiempo gastado es menor que el guardado, se ha roto el récord personal, por lo que debe registrarse

                if (PlayerPrefs.GetFloat("TiempoGastado", 2000f) > tiempoDerrotarEnemigos)
                {

                    PlayerPrefs.SetFloat("TiempoGastado", tiempoDerrotarEnemigos);
                    float minutes = Mathf.Floor(tiempoDerrotarEnemigos / 60);
                    float seconds = Mathf.RoundToInt(tiempoDerrotarEnemigos % 60);

                    if (seconds == 60)
                    {
                        seconds = 0;
                        minutes = Mathf.Floor(tiempoDerrotarEnemigos / 60 + 1);
                    }

                    string tiempoAGuardar = minutes.ToString() + ":" + seconds.ToString("00");

                    PlayerPrefs.SetString("TiempoRecord", tiempoAGuardar);

                    PlayerPrefs.SetInt("SoldadosVivos", (int)Recursos.soldados);

                }

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

    public bool GetSeHanCreadoEnemigos()
    {
        return seHanCreadoEnemigos;
    }

    #endregion

}
