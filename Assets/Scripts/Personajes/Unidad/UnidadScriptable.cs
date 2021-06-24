using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Unidad", menuName ="UnidadScriptable/Unidad")]
public class UnidadScriptable : ScriptableObject
{
    public int vida;
    public int fuerza;
    public int rangoAtaque;
    public TipoUnidad tipo;
    public Bando bando;

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
