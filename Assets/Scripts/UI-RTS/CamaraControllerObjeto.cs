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
        float velX, velY;
        /*
        //gameObject.transform.Translate(new Vector3(movX * Time.deltaTime * 60 * velocidad, movY * Time.deltaTime * 60 * velocidad, 0));

        rb.velocity = new Vector2(movX * 6, movY * 6);

        //si se deja de presionar una tecla para mover la cámara, el objeto que desplaza la cámara volverá al centro de la pantalla para que no haya
        //un retardo a la hora de desplazarse de derecha a izquierda
        if (rb.velocity == Vector2.zero)
        {
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth,Camera.main.pixelHeight));
        }*/

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
        /*else //se evita que se mueva para que no se salga del cuadro
        {

            velX = 0f;
        }*/

        //aquí igual, pero con derecha e izquierda

        if (gameObject.transform.position.y <= 26f && movY > 0) 
        {
            gameObject.transform.Translate(new Vector2(0, Time.deltaTime * 20f));
        }

        else if (gameObject.transform.position.y >= 2f && movY < 0)
        {
            gameObject.transform.Translate(new Vector2(0, -Time.deltaTime * 20f));
        }

        /*else 
        {
            velY = 0f;
        }*/

        //la cámara se desplaza

        //rb.velocity = new Vector2(velX, velY);


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
