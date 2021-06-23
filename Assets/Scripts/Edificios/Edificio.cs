using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Edificio : MonoBehaviour
{
    public EdificioScriptable edificioData;
    SpriteRenderer spriteEdificio;
    bool haFinalizadoConstruccion = false;
    float inicioCuenta;
    float duracionRestante;
    float progresoTemporizador;
    public SpriteRenderer temporizadorRelleno;
    public SpriteRenderer temporizadorFondo;

    private void Start()
    {
        spriteEdificio = gameObject.GetComponent<SpriteRenderer>();

        edificioData.estado = EdificioScriptable.EstadoEdificio.EnConstruccion;
        spriteEdificio.sprite = edificioData.enConstruccion;
        spriteEdificio.color = new Color(1, 0, 1, 1);
        //TODO: añadir un temporizador sobre el objeto que muestre cuánto queda para estar construido

        //El temporizador estará colocado sobre el modelo, dentro del prefab, y será invisible una vez termine de construirse
        
        inicioCuenta = Time.time;
        duracionRestante = edificioData.tiempoConstruccion;

    }

    private void Update()
    {
        if (!haFinalizadoConstruccion)
        {
            ProcesoConstruccion();
        }
    }

    #region Funciones

    public bool ComprobarConstruirEdificio()
    {
        bool sePuedeConstruir = true;
        for (int i = 0; i < edificioData.coste.Count; i++)
        {
            if (!Recursos.ComprobarCantidadRecurso(i, edificioData.coste[i]))
            {
                sePuedeConstruir = false;
                break;
            }
        }

        return sePuedeConstruir;
    }

    public void SumarBeneficio()
    {
        if (edificioData.beneficio[0] != 0)
        {
            Recursos.habitantes += edificioData.beneficio[0];
        }
        else
        {

            Recursos.soldados += edificioData.beneficio[1];
        }
    }

    public void ConstruirEdificio()
    {
        for (int i = 0; i < edificioData.coste.Count; i++)
            Recursos.RestarRecurso(i, edificioData.coste[i]);

        //TODO: Añadir particulas de polvo al construir el edificio

    }

    public void ProcesoConstruccion()
    {
        Debug.Log("Iniciando temporizador");

        if (Time.time - inicioCuenta < edificioData.tiempoConstruccion)
        {
            duracionRestante -= Time.deltaTime;
            progresoTemporizador = duracionRestante / edificioData.tiempoConstruccion;
            Debug.Log("El tiempo restante es: " + duracionRestante);
            
        }
        else
        {
            spriteEdificio.color = new Color(1, 1, 1, 1);
            
            haFinalizadoConstruccion = true;

            edificioData.estado = EdificioScriptable.EstadoEdificio.Terminado;
            spriteEdificio.sprite = edificioData.terminado;
            SumarBeneficio();
            //Actualizar contador de civiles y soldados

        }

    }

    #endregion
}
