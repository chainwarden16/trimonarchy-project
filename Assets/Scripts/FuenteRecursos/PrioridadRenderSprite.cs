using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioridadRenderSprite : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Unidad>() != null || collision.GetComponent<UnidadEnemiga>() != null)
        {
            collision.GetComponentInChildren<SpriteRenderer>().sortingOrder = gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder - 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Unidad>() != null || collision.GetComponent<UnidadEnemiga>() != null)
        {
            collision.GetComponentInChildren<SpriteRenderer>().sortingOrder = gameObject.GetComponentInParent<SpriteRenderer>().sortingOrder + 1;
        }
    }
}
