using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Edificio", menuName ="EdificioScriptable/Edificio")]
public class EdificioScriptable : ScriptableObject
{
    #region Enums

    public enum EstadoEdificio
    {
        EnConstruccion,
        Terminado
    }

    #endregion

    #region Variables públicas


    [Header("--Datos referidos al edificio--")]
    [Tooltip("El orden de los costes es: oro, madera, piedra, fruta, cebada, agua, metal, carne")]
    public List<int> coste;
    [Tooltip("Nombre del edificio")]
    public string nombre;
    [Tooltip("Beneficio que ofrece (tipo y cantidad). El primer índice representa a los civiles; el segundo, a los soldados")]
    public List<int> beneficio;
    [Tooltip("Cómo de construido está el edificio. El coste se tomará en el primer estado y el beneficio se concederá en el último")]
    public EstadoEdificio estado;
    [Tooltip("Ancho y largo en metros del edificio")]
    public int ancho;
    public int largo;
    [Header("--Control de sprites y apariencia--")]
    [Tooltip("Sprite que se muestra ahora teniendo en cuenta el estado de construccion del edificio")]
    public SpriteRenderer spriteActual;
    [Tooltip("Tiempo en segundos que tardará en estar listo")]
    public float tiempoConstruccion;
    public Sprite enConstruccion;
    public Sprite terminado;


    #endregion


}
