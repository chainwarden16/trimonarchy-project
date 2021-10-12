using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Recursos : MonoBehaviour
{
    
    public static List<int> recursos = new List<int>() { 0, 5, 10, 0, 0, 0, 0, 0 }; //representan, en orden: oro, madera, piedra, fruta, cebada, agua, metal, carne
    public static float habitantes = 1;
    public static float soldados = 0; //cambiar a 0
    public static List<Sprite> iconosRecursos;

    private const float ratioHabitantesSoldados = 0.3f; 

    /// <summary>
    /// Se añade una cantidad dada como parámetro del recurso indicado
    /// </summary>
    /// <param name="indice">El índice del recurso en la lista</param>
    /// <param name="cantidad">La cantidad a añadir. No puede ser negativo</param>
    public static void SumarRecurso(int indice, int cantidad)
    {
        recursos[indice] += cantidad;
    }

    /// <summary>
    /// Gasta una cantidad de recursos proporcionada como parámetro. No puede ser menor que 0
    /// </summary>
    /// <param name="indice">El índice del recurso dentro de la lista</param>
    /// <param name="cantidad">la cantidad a gastar</param>
    public static void RestarRecurso(int indice, int cantidad)
    {

            recursos[indice] -= cantidad;


        //NOTA: Mostrar mensaje indicando que no se ha llevado a cabo la operación en otro lugar con texto por pantalla usando TextMeshPro, como en el script donde se realiza la construccion del edificio

        
    }

    /// <summary>
    /// Comprueba si existe suficiente cantidad de un recurso dado para construir un edificio
    /// </summary>
    /// <param name="indice">ïndice del recurso en la lista</param>
    /// <param name="cantidad">Cantidad de ese recurso requerido</param>
    /// <returns>Un booleano que determina si se tiene suficiente cantidad de ese recurso</returns>
    public static bool ComprobarCantidadRecurso(int indice, int cantidad)
    {
        bool sePuedeRealizarOperacion = false;
        if (recursos[indice] - cantidad >= 0)
        {
            sePuedeRealizarOperacion = true;
        }

        return sePuedeRealizarOperacion;
    }

    /// <summary>
    /// Esta función evita que haya más de un cierto porcentaje de piblación que sea soldado. No tiene mucho sentido que todos los habitantes se dediquen a la guerra; tiene que haber otras personas dedicadas a otros oficios.
    /// No recibe ningún parámetro y devuelve un booleano que determina el ratio actual de soldados y civiles. Si es menor que el indicado por la constante ratioHabitantesSoldados, entonces se podrá entrenar a más
    /// </summary>
    /// <returns></returns>
    public static bool ComprobarRatioHabitantesSoldados()
    {
        bool haySuficientesHabitantes = false;

        if(soldados/habitantes < ratioHabitantesSoldados)
        {
            haySuficientesHabitantes = true;
        }

        return haySuficientesHabitantes;
    }

    public static void SetRecursos(List<int> recur)
    {
        recursos = recur;
    }

    public static void SetSoldados(int sold)
    {
        soldados = sold;
    }

    public static void SetHabitantes(int habi)
    {
        habitantes = habi;
    }


}
