using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingUnidad : MonoBehaviour
{
    GameManager manager;
    GameManagerTutorial managerTutorial;
    Tilemap suelo;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        managerTutorial = FindObjectOfType<GameManagerTutorial>();
        suelo = GameObject.Find("Tilemap-Suelo").GetComponent<Tilemap>();
    }

    public void BuscarCamino(Vector2 puntoInicial, Vector2 puntoFinal)
    {
        List<int> coordenadasInicio = TransformarMundoACasilla(puntoInicial);
            
        if(manager != null)
        {
            
        }
        else
        {

        }



    }

    public List<int> TransformarMundoACasilla(Vector2 posicion)
    {

        List<int> lugarEnGrid = new List<int>();

        int x = suelo.WorldToCell(new Vector3Int((int)posicion.x, (int)posicion.y, 0)).x;
        int y = suelo.WorldToCell(new Vector3Int((int)posicion.x, (int)posicion.y, 0)).y;

        lugarEnGrid.Add(x);
        lugarEnGrid.Add(y);

        return lugarEnGrid;

    }

}
