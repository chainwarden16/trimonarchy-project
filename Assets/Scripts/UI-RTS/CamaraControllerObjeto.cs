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


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controles = new PlayerControls();
        controles.RTS.MoverCamara.performed += ctx => movX = (int)ctx.ReadValue<Vector2>().x;
        controles.RTS.MoverCamara.canceled += ctx => movX = 0;
        controles.RTS.MoverCamara.performed += ctx => movY = (int)ctx.ReadValue<Vector2>().y;
        controles.RTS.MoverCamara.canceled += ctx => movY = 0;
    }

    private void Update()
    {
        MoverCamara();
    }

    private void MoverCamara()
    {
        //gameObject.transform.Translate(new Vector3(movX * Time.deltaTime * 60 * velocidad, movY * Time.deltaTime * 60 * velocidad, 0));

        rb.velocity = new Vector2(movX*6, movY*6);
        
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
