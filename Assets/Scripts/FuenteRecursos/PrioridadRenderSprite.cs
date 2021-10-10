using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;

public class PrioridadRenderSprite : MonoBehaviour
{

    Tilemap suelo;
    GameManager manager;
    GameManagerTutorial tuto;
    NavMeshAgent agente;
    float y;

    void Start()
    {
        suelo = GameObject.Find("Tilemap-Suelo").GetComponent<Tilemap>();

        manager = FindObjectOfType<GameManager>();
        tuto = FindObjectOfType<GameManagerTutorial>();
        agente = GetComponent<NavMeshAgent>();

    }

    void LateUpdate()
    {

        if (agente != null)
        {
            
            if (suelo.HasTile(new Vector3Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y, 0)))
            {
               
               
               y = suelo.WorldToCell(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0)).y - gameObject.transform.position.y - 10f;
               

            }
            else
            {
               y = 0;
            }

            if (manager != null)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = manager.largoGrid - (int)y;

            }
            else if (tuto != null)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = tuto.largoGrid - (int)y;
            }

        }

    }

    /*
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Unidad>() != null || collider.gameObject.GetComponent<UnidadEnemiga>() != null)
        {
            if (collider.gameObject.transform.position.y <= gameObject.transform.position.y)
            {
                collider.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;

            }
            else
            {
                collider.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
            }
        }
    }


    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Unidad>() != null || collider.gameObject.GetComponent<UnidadEnemiga>() != null)
        {

            Debug.Log(collider.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder);

            if (collider.gameObject.transform.position.y <= gameObject.transform.position.y)
            {
                collider.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;

            }

        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Unidad>() != null || collider.gameObject.GetComponent<UnidadEnemiga>() != null)
        {
            Debug.Log(collider.gameObject.name);
            Debug.Log(gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder);

            if (collider.gameObject.transform.position.y <= gameObject.transform.position.y)
            {
                collider.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;

            }
            else
            {
                collider.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
            }
        }
    }
    */
}
