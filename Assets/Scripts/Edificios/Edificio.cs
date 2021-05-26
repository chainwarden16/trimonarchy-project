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

    #region Variables públicas
    public List<int> coste;
    public string nombre;
    public Tuple<Beneficio, int> beneficio;
    public EstadoEdificio estado;
    public SpriteRenderer sprite;
    public float tiempoConstruccion;

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

        //TODO: Añadir cambio de sprite

        //TODO: Añadir una Corrutina que determine el tiempo pasado para pasar a otro estado. Si es demasiado difícil, hacer que se construyan los edificios automáticamente
    }

    public IEnumerator ProcesoConstruccion()
    {
        yield return null;
    }

    #endregion
}
