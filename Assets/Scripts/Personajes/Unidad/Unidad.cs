using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unidad : MonoBehaviour
{
    [Header("Estadísticas de la unidad")]
    public UnidadScriptable unidad;

    [Header("Propiedades visuales")]
    SpriteRenderer renderer;
    Animator anim;

    [Header("Propiedades de selección y cumplimiento de órdenes")]
    GameObject selector; //activa el indicador de selección de unidades cuando el jugador arrastra el ratón sobre ella
    public GameObject objetivoActual; //determina si la unidad ya tiene un objetivo asignado
    public Vector3 posicionObjetivo; //indica si el personaje se desplaza a una zona en concreto
    bool ejecutandoAccion = false; //sirve para determinar si el personaje está ejecutando una acción

    private void Awake()
    {
        selector = transform.Find("Selector").gameObject;
        MostrarSelectorUnidad(false);
        posicionObjetivo = gameObject.transform.position;
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
                
                }
            }
        }


    }

    public void LiberarUnidad()
    {

        ejecutandoAccion = false;
        objetivoActual = null;
    }

}
