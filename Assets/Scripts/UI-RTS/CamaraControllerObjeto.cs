using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CamaraControllerObjeto : MonoBehaviour
{
    public Tilemap tile;


    PlayerControls controles;
    int movX;
    int movY;
    Rigidbody2D rb;
    Camera camara;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        controles = new PlayerControls();
        controles.RTS.MoverCamara.performed += ctx => movX = (int)ctx.ReadValue<Vector2>().x;
        controles.RTS.MoverCamara.canceled += ctx => movX = 0;
        controles.RTS.MoverCamara.performed += ctx => movY = (int)ctx.ReadValue<Vector2>().y;
        controles.RTS.MoverCamara.canceled += ctx => movY = 0;

        camara = Camera.main;
    }

    private void Update()
    {
        MoverCamara();
    }

    private void MoverCamara()
    {

        //si no está demasiado alto, puede seguir subiendo la cámara

        if(gameObject.transform.position.x <= 21f && movX > 0)
        {
            gameObject.transform.Translate(new Vector2(Time.deltaTime * 20f, 0));
        }
        //si no está demasiado bajo, puede seguir bajando
        else if(gameObject.transform.position.x >= -21f && movX < 0)
        {
           gameObject.transform.Translate(new Vector2(-Time.deltaTime * 20f, 0));
        }


        //aquí igual, pero con derecha e izquierda

        if (gameObject.transform.position.y <= 26f && movY > 0) 
        {
            gameObject.transform.Translate(new Vector2(0, Time.deltaTime * 20f));
        }

        else if (gameObject.transform.position.y >= 2f && movY < 0)
        {
            gameObject.transform.Translate(new Vector2(0, -Time.deltaTime * 20f));
        }

    }

    void OnEnable()
    {
        controles.RTS.Enable();
    }
    void OnDisable()
    {
        controles.RTS.Disable();
    }
}
