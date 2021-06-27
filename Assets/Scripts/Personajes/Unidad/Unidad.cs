using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unidad : MonoBehaviour
{
    [Header("Estadísticas de la unidad")]
    public UnidadScriptable unidad;
    public int vidaActual;
    float enfriamientoAtaqueRestante;

    [Header("Propiedades visuales")]
    SpriteRenderer renderer;
    Animator anim;

    [Header("Propiedades de selección y cumplimiento de órdenes")]
    GameObject selector; //activa el indicador de selección de unidades cuando el jugador arrastra el ratón sobre ella
    public GameObject objetivoActual; //determina si la unidad ya tiene un objetivo asignado
    public Vector3 posicionObjetivo; //indica si el personaje se desplaza a una zona en concreto
    bool ejecutandoAccion = false; //sirve para determinar si el personaje está ejecutando una acción

    public List<Unidad> unidadesAsignadas = new List<Unidad>(); //indica el número de enemigos que lo están atacando

    private void Awake()
    {
        selector = transform.Find("Selector").gameObject;
        MostrarSelectorUnidad(false);
        posicionObjetivo = gameObject.transform.position;
        vidaActual = unidad.vida;
        enfriamientoAtaqueRestante = 0f;
    }
    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        //el selector empieza apagado, pues no se tienen unidades escogidas
    }

    private void Update()
    {
        Desplazarse();
        AtacarUnidad();
        Morir();
    }

    public void MostrarSelectorUnidad(bool debeMostrarse)
    {

        selector.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 * Convert.ToInt32(debeMostrarse)); //si es verdadero, tendrá opacidad 1 (100%); si no, opacidad 0 (0%). Los booleanos valen siempre 0 o 1

    }

    public void Desplazarse()
    {
        //Las unidades se desplazan hacia el punto que el jugador indique
        //si tiene un objetivo al que dirigirse O un punto nuevo al que caminar Y a su vez no está haciendo ya algo

        Debug.Log("Debería moverme");
        if (posicionObjetivo != gameObject.transform.position)

        {
            Debug.Log("Mi posición ya no es donde estoy");
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, posicionObjetivo, 0.5f * Time.deltaTime * 60);


            if (Mathf.Abs(Vector3.Distance(gameObject.transform.position, posicionObjetivo)) <= 1f)
            {
                Debug.Log("He llegado al punto indicado");
                posicionObjetivo = gameObject.transform.position;

            }


        }

        else if (objetivoActual != null)
        {
            Debug.Log("Tengo un objetivo");
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, objetivoActual.transform.position, 0.5f * Time.deltaTime * 60);

            if (Mathf.Abs(Vector3.Distance(gameObject.transform.position, objetivoActual.transform.position)) <= 1f)
            {
                Debug.Log("He llegado a mi objetivo");
                ejecutandoAccion = true;
                posicionObjetivo = gameObject.transform.position;
                if (objetivoActual.GetComponent<FuenteRecursosOperaciones>() != null && !objetivoActual.GetComponent<FuenteRecursosOperaciones>().unidadesAsignadas.Contains(this))

                {
                    objetivoActual.GetComponent<FuenteRecursosOperaciones>().AsignarUnidad(this);
                
                }else if(objetivoActual.GetComponent<Unidad>() != null && !objetivoActual.GetComponent<Unidad>().unidadesAsignadas.Contains(this))
                {
                    objetivoActual.GetComponent<Unidad>().AddUnidad(this);

                }
            }
        }


    }

    public void Morir()
    {
        if (vidaActual <= 0)
        {
            foreach(Unidad uni in unidadesAsignadas)
            {
                uni.LiberarUnidad();
            }
            Debug.Log("Me he morío");
            Destroy(gameObject);
        }
    }

    public void AtacarUnidad()
    {
        if (objetivoActual != null && objetivoActual.GetComponent<Unidad>() != null && objetivoActual.GetComponent<Unidad>().unidad.bando != unidad.bando && ejecutandoAccion)
        { //si el objetivo no es nulo, tiene componente unidad Y no es el del mismo bando, atácalo
            Unidad unidadEnemiga = objetivoActual.GetComponent<Unidad>();

            Debug.Log(gameObject.name + " va a atacar a un enemigo que tiene todavía " + unidadEnemiga.vidaActual+" puntos de vida");

            if (enfriamientoAtaqueRestante <= 0) //claro que para eso debe haber pasado el tiempo de enfriamiento
            {
                if (unidadEnemiga.vidaActual - unidad.fuerza < 0)
                {
                    unidadEnemiga.vidaActual = 0;
                }
                else
                {
                    unidadEnemiga.vidaActual -= unidad.fuerza;
                    enfriamientoAtaqueRestante = unidad.tiempoEnfriamientoAtaque;
                }
            }
            else
            {
                enfriamientoAtaqueRestante -= Time.deltaTime;
            }

        }
    }

    public void LiberarUnidad()
    {

        ejecutandoAccion = false;
        objetivoActual = null;
    }

    public void AddUnidad(Unidad unidad)
    {
        unidadesAsignadas.Add(unidad);
    }

    public void QuitarUnidad(Unidad unidad)
    {
        unidadesAsignadas.Remove(unidad);
    }

}
