using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuenteRecursosOperaciones : MonoBehaviour
{
    public FuenteRecursos fuente;
    float tiempoEnfriamientoActual;
    int cantidadExtraida = 0;

    List<Unidad> unidadesAsignadas;

    public void ProporcionarRecurso()
    {
        if (unidadesAsignadas.Count > 0) //tiene que haber alguien trabajando en esta fuente
        {
            if (tiempoEnfriamientoActual >= fuente.tiempoEnfriamiento)
            { //tiene que haber pasado el tiempo de enfriamiento para proporcionar el recurso

                Recursos.SumarRecurso(fuente.indiceRecurso, fuente.cantidad);
                tiempoEnfriamientoActual = 0f;
                if(cantidadExtraida >= fuente.cantidadMaxima)
                {
                    //TODO: liberar unidades que están trabajando en esta fuente y hacer que pasen a idle
                    Destroy(gameObject);
                }

            }
            else
            {
                tiempoEnfriamientoActual += Time.deltaTime;
            }
        }
    }
}
