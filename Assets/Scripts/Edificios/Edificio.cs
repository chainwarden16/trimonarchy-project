using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Edificio : MonoBehaviour
{
    #region Enums
    public enum Beneficio
    {
        Habitantes,
        Soldados
    }

    public enum EstadoEdificio
    {
        SinIniciar,
        EnConstruccion,
        Terminado
    }

    #endregion

    #region Variables p�blicas

    [Header("--Datos referidos al edificio--")]
    [Tooltip("El orden de los costes es: oro, madera, piedra, fruta, cebada, agua, metal")]
    public List<int> coste;
    [Tooltip("Nombre del edificio")]
    public string nombre;
    [Tooltip("Beneficio que ofrece (tipo y cantidad)")]
    public Tuple<Beneficio, int> beneficio;
    [Tooltip("C�mo de construido est� el edificio. El coste se tomar� en el primer estado y el beneficio se conceder� en el �ltimo")]
    public EstadoEdificio estado;
    [Header("--Control de sprites y apariencia--")]
    [Tooltip("Sprite que se muestra ahora teniendo en cuenta el estado de construccion del edificio")]
    public SpriteRenderer spriteActual;
    [Tooltip("Tiempo en segundos que tardar� en estar listo")]
    public float tiempoConstruccion;
    public Sprite noIniciado;
    public Sprite enConstruccion;
    public Sprite terminado;

    #endregion

    #region Funciones

    public bool ComprobarConstruirEdificio()
    {
        bool sePuedeConstruir = true;
        for(int i = 0; i < coste.Count;i++)
        {
            if (!Recursos.ComprobarCantidadRecurso(i, coste[i]))
            {
                sePuedeConstruir = false;
                break;
            }
        }

        return sePuedeConstruir;
    }

    public void SumarBeneficio()
    {
        if(beneficio.Item1 is Beneficio.Habitantes)
        {
            Recursos.habitantes += beneficio.Item2;
        }
        else
        {
            
            Recursos.soldados += beneficio.Item2;
        }
    }

    public void ConstruirEdificio()
    {
        estado = EstadoEdificio.SinIniciar;
        for (int i = 0; i < coste.Count; i++)
            Recursos.RestarRecurso(i, coste[i]);

        //TODO: A�adir cambio de sprite

        //TODO: A�adir una Corrutina que determine el tiempo pasado para pasar a otro estado. Si es demasiado dif�cil, hacer que se construyan los edificios autom�ticamente
    }

    public IEnumerator ProcesoConstruccion()
    {



        yield return null;
    }

    #endregion
}
