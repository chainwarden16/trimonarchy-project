using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Edificio : MonoBehaviour
{

    #region Variables

    [Header("Datos del edificio")]
    public EdificioScriptable edificioData;
    SpriteRenderer spriteEdificio;
    public bool haFinalizadoConstruccion = false;
    public GameObject ciudadano;
    public GameObject mago;
    public GameObject guerrero;
    float duracionRestante;

    [Header("Contador de tiempo para dar recurso")]
    float progresoRecurso = 4f; //cada 30 segundos dará uno o más materiales definidos en su Scriptable Object


    [Header("Civiles trabajando en la construcción")]
    public List<Unidad> unidadesAsignadas = new List<Unidad>(); //indica el número de civiles que lo están construyendo

    #endregion

    private void Start()
    {
        spriteEdificio = gameObject.GetComponent<SpriteRenderer>();

        edificioData.estado = EdificioScriptable.EstadoEdificio.EnConstruccion;
        spriteEdificio.sprite = edificioData.enConstruccion;
        spriteEdificio.color = new Color(1, 1, 1, 1);

        duracionRestante = edificioData.tiempoConstruccion;

    }

    private void Update()
    {
        if (!haFinalizadoConstruccion)
        {
            ProcesoConstruccion();
            
        }
        else
        {
            AddRecursos();
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
            for(int i = 0; i < edificioData.beneficio[0]; i++)
            {
                Instantiate(ciudadano, new Vector2(0, i), Quaternion.identity);
            }
        }
        else
        {
            Recursos.soldados += edificioData.beneficio[1];

            if (edificioData.indiceEdificio == 7) //Campo de entrenamiento
            {
                for (int i = 0; i < edificioData.beneficio[1]; i++)
                {
                    Instantiate(guerrero, new Vector2(1, i+1), Quaternion.identity);
                }
            }
            else
            {
                for (int i = 0; i < edificioData.beneficio[1]; i++) //Escuela de magia
                {
                    Instantiate(mago, new Vector2(2, i+6), Quaternion.identity);
                }
            }
        }

    }

    public void ConstruirEdificio()
    {
        for (int i = 0; i < edificioData.coste.Count; i++)
            Recursos.RestarRecurso(i, edificioData.coste[i]);

        GameManager.manager.ActualizarContadorRecursos(); //se actualiza la UI con la reducción de recursos

    }

    public void ProcesoConstruccion()
    {
        if (unidadesAsignadas.Count > 0 && !haFinalizadoConstruccion)
        {
            Debug.Log("Ya tengo una unidad y me pongo a contar");
            Debug.Log("Iniciando temporizador. Tengo a " + unidadesAsignadas.Count + " civiles asignados");

            if (duracionRestante > 0f)
            {
                duracionRestante -= Time.deltaTime * unidadesAsignadas.Count;

                Debug.Log("El tiempo restante es: " + duracionRestante);

            }
            else
            {

                haFinalizadoConstruccion = true;

                edificioData.estado = EdificioScriptable.EstadoEdificio.Terminado;
                spriteEdificio.sprite = edificioData.terminado;
                SumarBeneficio();
                //Actualizar contador de civiles y soldados
                GameManager.manager.ActualizarContadorRecursos();

                foreach (Unidad uni in unidadesAsignadas)
                {
                    uni.LiberarUnidad();
                }
            }

        }


    }

    private void AddRecursos()
    {

        if (progresoRecurso <= 0f)
        {

            for (int i = 0; i < edificioData.materiales.Count; i++)
            {
                Recursos.recursos[i] += edificioData.materiales[i];
            }
            progresoRecurso = 4f;
            GameManager.manager.ActualizarContadorRecursos();

        }
        else
        {
            progresoRecurso -= Time.deltaTime;
        }


    }

    public void AsignarUnidad(Unidad unidad)
    {
        unidadesAsignadas.Add(unidad);
    }

    public void QuitarUnidad(Unidad unidad)
    {
        unidadesAsignadas.Remove(unidad);
    }



    #endregion
}
