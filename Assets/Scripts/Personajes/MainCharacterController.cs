using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxSpeed = 4;
    
    private float moviX;
    private float moviY;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        moviX = Input.GetAxisRaw("Horizontal");
        moviY = Input.GetAxisRaw("Vertical");
        MoverPersonaje();

    }


    void MoverPersonaje()
    {

        if (moviX != 0 || moviY != 0)
        {

            //rb.velocity = new Vector3(-3, 0, 0);
            rb.AddForce(new Vector2(8 * moviX, 8 * moviY), ForceMode2D.Impulse);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }

        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            //arreglar el salto del personaje: si salta mientras se mueve, el salto será más bajo
            rb.velocity = rb.velocity.normalized * maxSpeed;

        }

    }

    
}
