using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCursor : MonoBehaviour
{
    GameManager managerJuego;
    float inputX;
    float inputY;

    private void Awake()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        managerJuego = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (inputX == -1 && gameObject.transform.position.x > 0 || inputX == 1 && gameObject.transform.position.x < managerJuego.anchoGrid)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + inputX, gameObject.transform.position.y, gameObject.transform.position.z);

        }else if(inputY == -1 && gameObject.transform.position.y > 0 || inputY == 1 && gameObject.transform.position.y < managerJuego.largoGrid)
        {

            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+inputY, gameObject.transform.position.z);
        }

        //managerJuego
    }
}
