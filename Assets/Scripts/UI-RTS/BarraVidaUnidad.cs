using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVidaUnidad : MonoBehaviour
{
    public Image barraVida;
    Unidad aliado;

    void Start()
    {
        aliado = GetComponentInParent<Unidad>();
    }

    void Update()
    {
        if (aliado != null)
        {
            //ACTUALIZAR SOLO CUANDO RECIBA DAÑO O SE CURE
            barraVida.fillAmount = (float)aliado.vidaActual / (float)aliado.unidad.vida;

        }
    }

}
