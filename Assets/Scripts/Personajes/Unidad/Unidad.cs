using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unidad : MonoBehaviour
{
    public UnidadScriptable unidad;
    
    SpriteRenderer renderer;
    Animator anim;
    GameObject selector;
    // Start is called before the first frame update
    private void Awake()
    {
        selector = transform.Find("Selector").gameObject;
        MostrarSelectorUnidad(false);
    }
    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        //el selector empieza apagado, pues no se tienen unidades escogidas
    }

    public void MostrarSelectorUnidad(bool debeMostrarse)
    {

            selector.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1* Convert.ToInt32(debeMostrarse)); //si es verdadero, tendrá opacidad 1 (100%); si no, opacidad 0 (0%). Los booleanos valen siempre 0 o 1

    }

    public void Desplazarse()
    {

    }

}
