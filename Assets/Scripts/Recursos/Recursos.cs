using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Recursos : MonoBehaviour
{
    
    public static List<int> recursos = new List<int>() { 0, 0, 0, 0, 0, 0, 0 }; //representan, en orden: oro, madera, piedra, fruta, cebada, agua, metal
    public static int habitantes = 0;
    public static int soldados = 0;

    public static void SumarRecurso(int indice, int cantidad)
    {
        recursos[indice] += cantidad;
    }

    public static void RestarRecurso(int indice, int cantidad)
    {

            recursos[indice] -= cantidad;


        //NOTA: Mostrar mensaje indicando que no se ha llevado a cabo la operación en otro lugar con texto por pantalla usando TextMeshPro, como en el script donde se realiza la construccion del edificio

        
    }

    public static bool ComprobarCantidadRecurso(int indice, int cantidad)
    {
        bool sePuedeRealizarOperacion = false;
        if (recursos[indice] - cantidad >= 0)
        {
            sePuedeRealizarOperacion = true;
        }

        return sePuedeRealizarOperacion;
    }


}
