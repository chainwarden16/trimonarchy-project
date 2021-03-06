using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Unidad", menuName ="UnidadScriptable/Unidad")]
public class UnidadScriptable : ScriptableObject
{
    public int vida;
    public int fuerza;
    public float rangoAtaque;
    public TipoUnidad tipo;
    public Bando bando;
    public float tiempoEnfriamientoAtaque;
    public int limiteUnidadesAsignadas;

    public enum TipoUnidad
    {
        Civil,
        Guerrero,
        Mago
    }

    public enum Bando
    {
        Jugador,
        IA
    }
}
