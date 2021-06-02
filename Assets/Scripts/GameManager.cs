using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Dimensiones de la cuadrícula, de la que el cursor no puede salir")]
    public int anchoGrid;
    public int largoGrid;
    private int[][] gridCiudad; //se debe comprobar en la clase del cursor que no se salga de estas medidas

    [Header("Control del estado del juego")]
    public GameObject contadorRecursos;
    float tiempoRestante;
    float numeroSoldadosNecesario;
    List<GameObject> textoContador = new List<GameObject>() { };

    [Header("Manipulación de elementos del mapa")]
    GameObject textoMenuAcciones;
    int accionElegida;
    public List<Edificio> tiposEdificio;
    public GameObject cursorMapa;

    // Start is called before the first frame update
    void Start()
    {

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
            SceneManager.LoadScene(6);
        }
    }

    private void InicializarContadorRecursos()
    {
        float anchoPantalla = Screen.width / 8;
        for (int i = 0; i < Recursos.recursos.Count; i++)
        {
            GameObject textoContador = Instantiate(contadorRecursos, new Vector3(anchoPantalla + anchoPantalla * i, Screen.height / 10, 0), Quaternion.identity);
            textoContador.GetComponent<TextMeshProUGUI>().text = Recursos.recursos[i].ToString();
            textoContador.GetComponent<Image>().sprite = Recursos.iconosRecursos[i];
        }
    }

    private void ActualizarContadorRecursos()
    {
        for (int i = 0; i < Recursos.recursos.Count; i++)
        {
            textoContador[i].GetComponent<TextMeshProUGUI>().text = Recursos.recursos[i].ToString();
        }

    }

    private bool ComprobarCasillaVacia(int fila, int columna)
    {
        bool estaVacia = true;

        if (gridCiudad[fila][columna] != 0)
        {
            estaVacia = false;
        }

        return estaVacia;
    }

    private void AbrirMenuAcciones(int x, int y)
    {
        //TODO: Colocar un texto con opciones según el valor de la celda en la cuadrícula.

        if (Input.GetButtonDown("Submit"))
        {
            if (accionElegida == 0)
            {

                int valorCelda = gridCiudad[x][y];

                switch (valorCelda)
                {
                    case 0: //vacío
                        textoMenuAcciones.GetComponent<TextMeshProUGUI>().text = "Construir";
                        accionElegida = 1;
                        break;
                    case 1: //una mina de recursos
                        textoMenuAcciones.GetComponent<TextMeshProUGUI>().text = "Recolectar";
                        accionElegida = 2;
                        break;
                    case 2: //un edificio de entrenamiento de soldados
                        accionElegida = 3;
                        textoMenuAcciones.GetComponent<TextMeshProUGUI>().text = "Entrenar";
                        break;
                }

            }
            else
            {
                switch (accionElegida)
                {
                    case 1:
                        if (Input.GetKeyDown(KeyCode.Alpha1))
                        {
                            bool sePuedeConstruir = tiposEdificio[0].ComprobarConstruirEdificio();
                            if (sePuedeConstruir)
                            {
                                tiposEdificio[0].ConstruirEdificio();
                                Instantiate(tiposEdificio[0], cursorMapa.transform.position, Quaternion.identity);
                                tiposEdificio[0].ProcesoConstruccion();
                                for (int i = 0; i < tiposEdificio[0].ancho; i++)
                                {
                                    for (int j = 0; j < tiposEdificio[0].largo; j++)
                                    {
                                        gridCiudad[x+i][y+j] = 3;
                                    }
                                }

                            }
                            else
                            {
                                //TODO: poner mensaje que diga que no se puede construir 
                            }

                            accionElegida = 0;

                        }


                        break;
                    case 2: // se recolectan recursos
                        break;
                    case 3:
                        break;
                }
            }

        }

    }

}
