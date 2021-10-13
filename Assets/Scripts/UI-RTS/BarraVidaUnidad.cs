using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVidaUnidad : MonoBehaviour
{
    public Image barraVida;
    Unidad aliado;
    UnidadEnemiga enemigo;

    void Start()
    {
        aliado = GetComponentInParent<Unidad>();
        enemigo = GetComponentInParent<UnidadEnemiga>();
    }

    public void ActualizarVidaUnidad()
    {
        if(aliado != null)
        {

        barraVida.fillAmount = (float)aliado.vidaActual / (float)aliado.unidad.vida;

        }
        else if(enemigo != null)
        {
            barraVida.fillAmount = (float)enemigo.vidaActual / (float)enemigo.unidad.vida;
        }
    }



}
