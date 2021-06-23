using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    GameManager managerJuego;


    private void Awake()
    {

        managerJuego = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        


    }


}
