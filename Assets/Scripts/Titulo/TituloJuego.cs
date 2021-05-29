using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TituloJuego : MonoBehaviour
{
    int opcion = 0;
    bool inputAbajo;
    bool inputArriba;
    // Start is called before the first frame update
    void Start()
    {
        inputAbajo = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) ;
        inputArriba = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) ;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputAbajo && opcion > 0)
        {
            opcion++;
        }
        else if (inputArriba && opcion < 2)
        {
            opcion--;
        }
        else if (Input.GetButtonDown("Submit"))
        {
            switch (opcion)
            {
                case 0:
                    SceneManager.LoadScene(2);
                    break;
                case 1:
                    SceneManager.LoadScene(1);
                    break;
                case 2:
                    Application.Quit();
                    break;

            }
        }
    }
}
