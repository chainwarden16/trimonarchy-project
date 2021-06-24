using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidad : MonoBehaviour
{
    public UnidadScriptable unidad;
    public GameObject marcadorSeleccion;
    SpriteRenderer renderer;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        //el selector empieza apagado, pues no se tienen unidades escogidas
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
