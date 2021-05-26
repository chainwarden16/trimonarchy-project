using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    public Rigidbody rb;
    public float fuerzaSalto = 10f;
    public float gravity = 9.81f;
    public float maxSpeed = 4;
    

    private bool enSuelo = true;
    private float moviX;

    private void Update()
    {

        moviX = Input.GetAxis("Horizontal");
        MoverPersonaje();
        SaltoPersonaje();

    }


    void MoverPersonaje()
    {

        if (moviX != 0)
        {

            //rb.velocity = new Vector3(-3, 0, 0);
            rb.AddForce(8 * moviX, 0, 0, ForceMode.Force);
        }

        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            //arreglar el salto del personaje: si salta mientras se mueve, el salto será más bajo
            rb.velocity = rb.velocity.normalized * maxSpeed;

        }

    }

    void SaltoPersonaje()
    {
        if (enSuelo == true && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(0, fuerzaSalto, 0, ForceMode.Impulse);
            enSuelo = false;
            Debug.Log("He saltado, el valor de enSuelo es " + enSuelo + " y la velocidad del objeto es: (" + rb.velocity.x + ", " + rb.velocity.y + ", "+ rb.velocity.z + ")");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            enSuelo = true;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }
}
