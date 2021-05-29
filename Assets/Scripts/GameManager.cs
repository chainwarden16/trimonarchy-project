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

        if(gridCiudad[fila][columna] != 0)
        {
            estaVacia = false;
        }

        return estaVacia;
    }


}
