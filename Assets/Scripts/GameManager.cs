using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [Header("Dimensiones de la cuadrícula, de la que el cursor no puede salir")]
    public int anchoGrid;
    public int largoGrid;
    public int[,] gridCiudad; //se debe comprobar en la clase del cursor que no se salga de estas medidas

    [Header("Control del estado del juego")]
    float tiempoRestante = 60; //en segundos
    int numeroSoldadosNecesario = 20; //los magos también entran aquí
    public List<TextMeshProUGUI> textoContador;

    [Header("Manipulación de elementos del mapa")]
    GameObject textoMenuAcciones;
    int accionElegida;
    public List<GameObject> tiposEdificio;
    Tilemap suelo;
    public GameObject recurso1, recurso2;
    public float probabilidadGeneracionRecurso;


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

    // Start is called before the first frame update
    void Start()
    {
        suelo = suelo = GameObject.Find("Tilemap-Suelo").GetComponent<Tilemap>();

        gridCiudad = new int[anchoGrid, largoGrid];
        for (int i = anchoGrid - 1; i >= 0; i--)
        {
            for (int j = largoGrid - 1; j >= 0; j--) //TODO: se generan algunos elementos aleatorios que son recursos naturales (madera y piedra) por el mapa
            {

                int contenido = Random.Range(0, 2);
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

                        //TODO: lo mismo que con la madera, pero con recurso 2

                        break;
                }

            }
        }

        ActualizarContadorRecursos();
    }

    // Update is called once per frame
    void Update()
    {
        DeterminarCondicionDerrota();
    }

    private void DeterminarCondicionDerrota()
    {
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0 && Recursos.soldados < numeroSoldadosNecesario)
        {
            //SceneManager.LoadScene(6);
        }
    }

    public void ActualizarContadorRecursos()
    {
        for (int i = 0; i < Recursos.recursos.Count; i++)
        {
            textoContador[i].GetComponent<TextMeshProUGUI>().text = Recursos.recursos[i].ToString();
        }

    }

    public bool ComprobarCasillaVacia(int fila, int columna)
    {
        bool estaVacia = true;
        if (anchoGrid >= fila && largoGrid >= columna && fila >= 0 && columna >= 0)
        {
            if (gridCiudad[fila, columna] != 0)
            {
                estaVacia = false;
            }
        }


        return estaVacia;
    }

    private void AbrirMenuAcciones(int x, int y)
    {


    }

}
