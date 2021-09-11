using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FuenteRecursosOperaciones : MonoBehaviour
{
    public FuenteRecursos fuente;
    float tiempoEnfriamientoActual = 0;
    int cantidadExtraida = 0;
    public List<Unidad> unidadesAsignadas = new List<Unidad>();
    Tilemap tileSuelo;
    SpriteRenderer rendererRecurso;

    private void Start()
    {
        tileSuelo = GameObject.Find("Tilemap-Suelo").GetComponent<Tilemap>();
        rendererRecurso = gameObject.GetComponent<SpriteRenderer>();
        rendererRecurso.sprite = fuente.spriteRecurso;
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void Update()
    {
        ProporcionarRecurso();
    }

    public void ProporcionarRecurso()
    {

        if (unidadesAsignadas.Count > 0) //tiene que haber alguien trabajando en esta fuente
        {
            bool trabajando = false;
            foreach(Unidad uni in unidadesAsignadas) //si al menos una de las unidades está ya ejecutando la acción (es decir, ha llegado al objetivo), entonces empieza a proporcionar recursos
            {
                if (uni.GetEjecutandoAccion())
                {
                    trabajando = true;
                }
            }

            if (tiempoEnfriamientoActual >= fuente.tiempoEnfriamiento && trabajando)
            { //tiene que haber pasado el tiempo de enfriamiento para proporcionar el recurso
                Debug.Log("Ahora voy a dar recursos");
                int cantidadAObtener = fuente.cantidad * unidadesAsignadas.Count;
                Recursos.SumarRecurso(fuente.indiceRecurso, cantidadAObtener); //cuantos más trabajen en este recurso, más rápido extraerán material
                cantidadExtraida += cantidadAObtener;
                GameManager.manager.ActualizarContadorRecursos();
                tiempoEnfriamientoActual = 0f;
                if(cantidadExtraida >= fuente.cantidadMaxima)
                {
                    //TODO: liberar unidades que están trabajando en esta fuente y hacer que pasen a idle
                    foreach (Unidad un in unidadesAsignadas) 
                    {
                        un.LiberarUnidad();
                    }
                    Vector3Int tpos = tileSuelo.WorldToCell(gameObject.transform.position);
                    GameManager.manager.RellenarCasillaGrid(tpos.x, tpos.y, 0);
                    Debug.Log("Me he agotado, me borro");
                    Destroy(gameObject);
                }

            }
            else
            {
                tiempoEnfriamientoActual += Time.deltaTime;
            }
        }
    }

    public void AsignarUnidad(Unidad unidad)
    {
        unidadesAsignadas.Add(unidad);
    }
    public void QuitarUnidad(Unidad unidad)
    {
        unidadesAsignadas.Remove(unidad);
    }

}
