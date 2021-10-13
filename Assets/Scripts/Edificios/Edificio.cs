using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class Edificio : MonoBehaviour
{

    #region Variables

    [Header("Datos del edificio")]
    public EdificioScriptable edificioData;
    SpriteRenderer spriteEdificio;
    public bool haFinalizadoConstruccion = false;
    public GameObject ciudadano;
    public GameObject mago;
    public GameObject guerrero;
    float duracionRestante;

    [Header("Manipulación del mapa al construir")]
    public Tile obstaculoInvisible; //se añade un tile especial al tilemap de obstáculos para editar la NavMesh
    Tilemap tileSuelo;
    Tilemap tileObstaculos;


    [Header("Contador de tiempo para dar recurso")]
    float progresoRecurso; //cada 30 segundos dará uno o más materiales definidos en su Scriptable Object


    [Header("Civiles trabajando en la construcción")]
    public List<Unidad> unidadesAsignadas = new List<Unidad>(); //indica el número de civiles que lo están construyendo

    [Header("Objetos padre donde colocar las unidades, para poner orden en la jerarquía")]
    GameObject padreUnidades;

    #endregion

    private void Start()
    {
        spriteEdificio = gameObject.GetComponent<SpriteRenderer>();

        edificioData.estado = EdificioScriptable.EstadoEdificio.EnConstruccion;
        spriteEdificio.sprite = edificioData.enConstruccion;
        spriteEdificio.color = new Color(1, 1, 1, 1);
        tileSuelo = GameObject.Find("Tilemap-Suelo").GetComponent<Tilemap>();
        tileObstaculos = GameObject.Find("Tilemap-Obstaculos").GetComponent<Tilemap>();
        duracionRestante = edificioData.tiempoConstruccion;

        progresoRecurso = edificioData.tiempoDarRecurso;
        padreUnidades = GameObject.Find("--unidades--");

    }

    private void Update()
    {
        if (!haFinalizadoConstruccion)
        {
            ProcesoConstruccion();

        }
        else
        {
            AddRecursos();
        }
    }

    #region Funciones

    public bool ComprobarConstruirEdificio()
    {
        bool sePuedeConstruir = true;
        for (int i = 0; i < edificioData.coste.Count; i++)
        {
            if (!Recursos.ComprobarCantidadRecurso(i, edificioData.coste[i]))
            {
                sePuedeConstruir = false;
                break;
            }
        }

        return sePuedeConstruir;
    }

    public void SumarBeneficio()
    {

        GameManagerTutorial tuto = FindObjectOfType<GameManagerTutorial>();

        if (edificioData.beneficio[0] != 0)
        {
            int contadorCiudadanos = 0;
            int k = 7;
            int j = 8;

            Recursos.habitantes += edificioData.beneficio[0];
            //for (int i = 0; i < edificioData.beneficio[0]; i++)
            //{
            Unidad[] unidadesAliadas = FindObjectsOfType<Unidad>();

            List<Vector3> posiciones = new List<Vector3>();

            foreach (Unidad un in unidadesAliadas)
            {

                Vector3 transTile = tileSuelo.GetCellCenterWorld(new Vector3Int((int)un.transform.position.x, (int)un.transform.position.y, (int)0f));

                posiciones.Add(transTile);

            }

            while (contadorCiudadanos < 5)
            {
                Vector3 lugarSpawn = tileSuelo.GetCellCenterWorld(new Vector3Int(k, j, 0));

                if (GameManager.manager != null && GameManager.manager.gridCiudad[k, j] == 0 && !posiciones.Contains(lugarSpawn))
                {
                    GameObject civil = Instantiate(ciudadano, lugarSpawn, Quaternion.identity);
                    if (padreUnidades != null)
                    {

                        civil.transform.SetParent(padreUnidades.transform, true);

                    }
                    contadorCiudadanos++;
                    if (contadorCiudadanos == edificioData.beneficio[0])
                    {
                        break;

                    }

                }
                else if (tuto != null && tuto.gridCiudad[k, j] == 0 && !posiciones.Contains(lugarSpawn))
                {
                    GameObject civil = Instantiate(ciudadano, lugarSpawn, Quaternion.identity);

                    if (padreUnidades != null)
                    {

                        civil.transform.SetParent(padreUnidades.transform, true);

                    }

                    contadorCiudadanos++;
                    if (contadorCiudadanos == edificioData.beneficio[0])
                    {
                        break;

                    }

                }
                k++;
                if (k >= 20)
                {
                    j++;
                    k = 7;
                }
            }


        }
        else
        {
            Recursos.soldados += edificioData.beneficio[1];
            int contadorSoldados = 0;
            int k = 7;
            int j = 9;

            if (edificioData.indiceEdificio == 7) //Campo de entrenamiento
            {

                Unidad[] unidadesAliadas = FindObjectsOfType<Unidad>();

                List<Vector3> posiciones = new List<Vector3>();

                foreach (Unidad un in unidadesAliadas)
                {

                    Vector3 transTile = tileSuelo.GetCellCenterWorld(new Vector3Int((int)un.transform.position.x, (int)un.transform.position.y, (int)0f));

                    posiciones.Add(transTile);

                }

                while (contadorSoldados < 4)
                {
                    Vector3 lugarSpawn = tileSuelo.GetCellCenterWorld(new Vector3Int(k, j, 0));

                    if (GameManager.manager != null && GameManager.manager.gridCiudad[k, j] == 0 && !posiciones.Contains(lugarSpawn))
                    {
                        GameObject unid = Instantiate(guerrero, lugarSpawn, Quaternion.identity);
                        if (padreUnidades != null)
                        {

                            unid.transform.SetParent(padreUnidades.transform, true);

                        }
                        contadorSoldados++;
                        if (contadorSoldados == edificioData.beneficio[1])
                        {
                            break;

                        }

                    }
                    else if (tuto != null && tuto.gridCiudad[k, j] == 0 && !posiciones.Contains(lugarSpawn))
                    {
                        GameObject unid = Instantiate(guerrero, lugarSpawn, Quaternion.identity);
                        if (padreUnidades != null)
                        {

                            unid.transform.SetParent(padreUnidades.transform, true);

                        }
                        contadorSoldados++;
                        if (contadorSoldados == edificioData.beneficio[1])
                        {
                            break;

                        }

                    }
                    k++;
                    if (k >= 20)
                    {
                        j++;
                        k = 7;
                    }
                }

            }
            else
            {
                for (int i = 0; i < edificioData.beneficio[1]; i++) //Escuela de magia
                {
                    Unidad[] unidadesAliadas = FindObjectsOfType<Unidad>();

                    List<Vector3> posiciones = new List<Vector3>();

                    foreach (Unidad un in unidadesAliadas)
                    {

                        Vector3 transTile = tileSuelo.GetCellCenterWorld(new Vector3Int((int)un.transform.position.x, (int)un.transform.position.y, (int)0f));

                        posiciones.Add(transTile);

                    }

                    while (contadorSoldados < 4)
                    {
                        Vector3 lugarSpawn = tileSuelo.GetCellCenterWorld(new Vector3Int(k, j, 0));

                        if (GameManager.manager != null && GameManager.manager.gridCiudad[k, j] == 0 && !posiciones.Contains(lugarSpawn))
                        {
                            GameObject unid = Instantiate(mago, lugarSpawn, Quaternion.identity);
                            if (padreUnidades != null)
                            {

                                unid.transform.SetParent(padreUnidades.transform, true);

                            }
                            contadorSoldados++;
                            if (contadorSoldados == edificioData.beneficio[1])
                            {
                                break;

                            }

                        }
                        else if (tuto != null && tuto.gridCiudad[k, j] == 0 && !posiciones.Contains(lugarSpawn))
                        {
                            GameObject unid = Instantiate(mago, lugarSpawn, Quaternion.identity);
                            if (padreUnidades != null)
                            {

                                unid.transform.SetParent(padreUnidades.transform, true);

                            }
                            contadorSoldados++;
                            if (contadorSoldados == edificioData.beneficio[1])
                            {
                                break;

                            }

                        }
                        k++;
                        if (k >= 20)
                        {
                            j++;
                            k = 7;
                        }
                    }

                }
            }
        }

    }

    public void ConstruirEdificio()
    {
        for (int i = 0; i < edificioData.coste.Count; i++)
            Recursos.RestarRecurso(i, edificioData.coste[i]);
        if (GameManager.manager != null)
        {
            GameManager.manager.ActualizarContadorRecursos(); //se actualiza la UI con la reducción de recursos

        }
        else
        {
            FindObjectOfType<GameManagerTutorial>().ActualizarContadorRecursos(); //se actualiza la UI con la reducción de recursos
        }
    }

    public void ProcesoConstruccion()
    {

        bool trabajando = false;
        foreach (Unidad uni in unidadesAsignadas) //si al menos una de las unidades está ya ejecutando la acción (es decir, ha llegado al objetivo), entonces empieza a proporcionar recursos
        {
            if (uni.GetEjecutandoAccion())
            {
                trabajando = true;
            }
        }

        if (unidadesAsignadas.Count > 0 && !haFinalizadoConstruccion && trabajando)
        {


            if (duracionRestante > 0f)
            {
                duracionRestante -= Time.deltaTime * unidadesAsignadas.Count;

            }
            else
            {

                haFinalizadoConstruccion = true;

                edificioData.estado = EdificioScriptable.EstadoEdificio.Terminado;
                spriteEdificio.sprite = edificioData.terminado;
                SumarBeneficio();
                //Actualizar contador de civiles y soldados

                if (GameManager.manager != null)
                {
                    GameManager.manager.ActualizarContadorRecursos(); //se actualiza la UI con la reducción de recursos

                }
                else
                {
                    FindObjectOfType<GameManagerTutorial>().ActualizarContadorRecursos(); //se actualiza la UI con la reducción de recursos
                }

                foreach (Unidad uni in unidadesAsignadas)
                {
                    uni.LiberarUnidad();
                }
                gameObject.layer = 10;
                Vector2 centroCasillaObstaculo = gameObject.transform.position;
                tileObstaculos.SetTile(tileObstaculos.WorldToCell(new Vector2(centroCasillaObstaculo.x, centroCasillaObstaculo.y - 0.5f)), obstaculoInvisible);

                FindObjectOfType<NavMeshSurface2d>().BuildNavMesh();
            }

        }


    }

    private void AddRecursos()
    {

        if (progresoRecurso <= 0f)
        {

            for (int i = 0; i < edificioData.materiales.Count; i++)
            {
                Recursos.recursos[i] += edificioData.materiales[i];
            }
            progresoRecurso = edificioData.tiempoDarRecurso;
            if (GameManager.manager != null)
            {
                GameManager.manager.ActualizarContadorRecursos(); //se actualiza la UI con la reducción de recursos

            }
            else
            {
                FindObjectOfType<GameManagerTutorial>().ActualizarContadorRecursos(); //se actualiza la UI con la reducción de recursos
            }

        }
        else
        {
            progresoRecurso -= Time.deltaTime;
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



    #endregion
}
