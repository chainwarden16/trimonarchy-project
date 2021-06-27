using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName ="FuenteRecursos", menuName ="FuenteScriptable/FuenteRecursos")]
public class FuenteRecursos : ScriptableObject
{
    [Tooltip("El orden de los costes es: oro, madera, piedra, fruta, cebada, agua, metal, carne")]
    public int indiceRecurso;
    [Tooltip("La cantidad de recurso que da cada vez que se cumple el tiempo de enfriamiento")]
    public int cantidad;
    [Tooltip("Cuando el recurso haya daño esta cantidad de recurso, se agotará se eliminará del mapa")]
    public int cantidadMaxima;
    [Tooltip("Aspecto del recurso")]
    public Sprite spriteRecurso;
    [Tooltip("Tiempo en segundos que debe pasar entre cada obtención de cantidad de recurso")]
    public float tiempoEnfriamiento;
    [Tooltip("Cantidad máxima de unidades que pueden extraer recursos de esta fuente a la vez. No sería lógico tener 80 tipos talando el mismo árbol")]
    public int limiteUnidadesAsignadas;

}
