using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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
        EnConstruccion,
        Terminado
    }

    #endregion

    #region Variables p�blicas


    [Header("--Datos referidos al edificio--")]
    [Tooltip("El orden de los costes es: oro, madera, piedra, fruta, cebada, agua, metal, carne")]
    public List<int> coste;
    [Tooltip("Nombre del edificio")]
    public string nombre;
    [Tooltip("Beneficio que ofrece (tipo y cantidad)")]
    [SerializeField]
    public List<int> beneficio; //�ndice 0 = civiles, �ndice 1 = soldados
    [Tooltip("C�mo de construido est� el edificio. El coste se tomar� en el primer estado y el beneficio se conceder� en el �ltimo")]
    public EstadoEdificio estado;
    [Header("--Control de sprites y apariencia--")]
    [Tooltip("Sprite que se muestra ahora teniendo en cuenta el estado de construccion del edificio")]
    public SpriteRenderer spriteActual;
    [Tooltip("Tiempo en segundos que tardar� en estar listo")]
    public float tiempoConstruccion;
    public Sprite enConstruccion;
    public Sprite terminado;
    public Image temporizadorFondo;
    public Image temporizadorRelleno;

    [Header("--S�lo existente durante testeo de prototipos--")]
    public Material materialTerminado;
    public Material materialEnConstruccion;

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
        if(beneficio[0] !=0)
        {
            Recursos.habitantes += beneficio[0];
        }
        else
        {

            Recursos.soldados += beneficio[1];
        }
    }

    public void ConstruirEdificio()
    {
        for (int i = 0; i < coste.Count; i++)
            Recursos.RestarRecurso(i, coste[i]);

        //TODO: A�adir particulas de polvo al construir el edificio

    }

    public IEnumerator ProcesoConstruccion()
    {

        estado = EstadoEdificio.EnConstruccion;
        //spriteActual.sprite = enConstruccion;

        //TODO: a�adir un temporizador sobre el objeto que muestre cu�nto queda para estar construido

        yield return new WaitForSeconds(tiempoConstruccion);


        estado = EstadoEdificio.EnConstruccion;
        //spriteActual.sprite = enConstruccion;
        gameObject.GetComponent<MeshRenderer>().material = materialTerminado;

        SumarBeneficio();

        yield return null;
    }

    public IEnumerator Temporizador()
    {
        //El temporizador estar� colocado sobre el modelo, dentro del prefab, y ser� invisible una vez termine de construirse
        temporizadorRelleno.color = new Color(1, 1, 1, 1);
        temporizadorFondo.color = new Color(1, 1, 1, 1);
        float inicioCuenta = Time.time;
        float duracionRestante = tiempoConstruccion;
        float progresoTemporizador;

        while(Time.time - inicioCuenta < tiempoConstruccion)
        {
            duracionRestante -= Time.deltaTime;
            progresoTemporizador = duracionRestante / tiempoConstruccion;
            temporizadorRelleno.fillAmount = progresoTemporizador;
            yield return null;
        }
        temporizadorRelleno.color = new Color(1, 1, 1, 0); //se vuelve invisible una vez acaba de contar el tiempo
        temporizadorFondo.color = new Color(1, 1, 1, 0); //se vuelve invisible una vez acaba de contar el tiempo
    }

    public void ReiniciarContador()
    {
        temporizadorRelleno.fillAmount = 0f;
    }

    #endregion
}
