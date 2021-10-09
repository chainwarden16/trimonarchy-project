using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

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
    UnidadController controlUnidades;
    public List<UnidadEnemiga> unidadesAsignadas = new List<UnidadEnemiga>(); //indica el número de enemigos que lo están atacando

    private void Awake()
    {
        selector = transform.Find("Selector").gameObject;
        MostrarSelectorUnidad(false);
        posicionObjetivo = gameObject.transform.position;
        vidaActual = unidad.vida;
        enfriamientoAtaqueRestante = 0f;
        controlUnidades = FindObjectOfType<UnidadController>();
    }
    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

            var agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        
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


        /*if (posicionObjetivo != gameObject.transform.position && objetivoActual == null)

        {

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, posicionObjetivo, Time.deltaTime*3);

            
            if (Mathf.Abs(Vector3.Distance(gameObject.transform.position, posicionObjetivo)) <= 1f)
            {

                posicionObjetivo = gameObject.transform.position;

            }


        }*/

        if (objetivoActual != null)
        {
            Debug.Log("Tengo un objetivo");

            if (Mathf.Abs(Vector3.Distance(gameObject.transform.position, objetivoActual.transform.position)) > unidad.rangoAtaque)
            {

                //Se deja por si es necesario añadir algo

            }

            else
            {
                //GetComponent<AIDestinationSetter>().target = null;
                ejecutandoAccion = true;
                posicionObjetivo = gameObject.transform.position;

            }
        }


    }

    public void Morir()
    {
        if (vidaActual <= 0)
        {
            foreach (UnidadEnemiga uni in unidadesAsignadas)
            {
                uni.LiberarUnidad();
            }
            Debug.Log("Me he morío");
            if (unidad.tipo != UnidadScriptable.TipoUnidad.Civil)
            {
                Recursos.soldados--;
            }
            /*if (controlUnidades.unidadesSeleccionadas[0] == this)
            {
                controlUnidades.CerrarPanel();
            }*/
            controlUnidades.unidadesSeleccionadas.Remove(this);
            Destroy(gameObject);
        }
    }

    public void AtacarUnidad()
    {
        if (objetivoActual != null && objetivoActual.GetComponent<UnidadEnemiga>() != null && objetivoActual.GetComponent<UnidadEnemiga>().unidad.bando != unidad.bando && ejecutandoAccion)
        { //si el objetivo no es nulo, tiene componente unidad Y no es el del mismo bando, atácalo
            UnidadEnemiga unidadEnemiga = objetivoActual.GetComponent<UnidadEnemiga>();

            Debug.Log(gameObject.name + " va a atacar a un enemigo que tiene todavía " + unidadEnemiga.vidaActual + " puntos de vida");

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

    public void AddUnidad(UnidadEnemiga unidad)
    {
        unidadesAsignadas.Add(unidad);
    }

    public void QuitarUnidad(UnidadEnemiga unidad)
    {
        unidadesAsignadas.Remove(unidad);
    }

    public bool GetEjecutandoAccion()
    {
        return ejecutandoAccion;
    }

}
