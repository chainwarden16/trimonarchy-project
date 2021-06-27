using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class UnidadController : MonoBehaviour
{

    PlayerControls controles;
    Vector2 inicioArrastreRaton = Vector2.zero;
    Vector2 finArrastreRaton = Vector2.zero;
    BuildController buildCon;
    GameObject areaSeleccion;
    Color colArea;
    bool estaPresionandoBotonIzquierdo = false;
    Tilemap tileSuelo;

    public List<Unidad> unidadesSeleccionadas = new List<Unidad>();

    private void Awake()
    {
        controles = new PlayerControls();

        //suscripción a los controles correspondientes

        controles.RTS.SeleccionarItem.performed += ctx => InicioSeleccionarUnidades();
        controles.RTS.SeleccionarItem.canceled += ctx => FinSeleccionarUnidades();
        controles.RTS.BorrarEdificioCancelar.performed += ctx => DarOrdenesUnidades();

        buildCon = GameObject.FindObjectOfType<BuildController>();
        areaSeleccion = GameObject.Find("--area seleccion--");
        colArea = areaSeleccion.GetComponentInChildren<SpriteRenderer>().color;
        areaSeleccion.GetComponentInChildren<SpriteRenderer>().color = new Color(colArea.r, colArea.g, colArea.b, 0); //se deja la imagen con opacidad 0 para que no se vea
        tileSuelo = GameObject.Find("Tilemap-Suelo").GetComponent<Tilemap>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EscalarAreaSeleccion();
    }

    private void InicioSeleccionarUnidades()
    {
        inicioArrastreRaton = Camera.main.ScreenToWorldPoint(controles.RTS.PosicionCursor.ReadValue<Vector2>());

        areaSeleccion.GetComponentInChildren<SpriteRenderer>().color = new Color(colArea.r, colArea.g, colArea.b, 0.6f); //se deja la imagen con opacidad 1 para que se vea lo que se está seleccionando

        estaPresionandoBotonIzquierdo = true;

    }

    private void FinSeleccionarUnidades()
    {

        finArrastreRaton = Camera.main.ScreenToWorldPoint(controles.RTS.PosicionCursor.ReadValue<Vector2>());
        estaPresionandoBotonIzquierdo = false;

        areaSeleccion.GetComponentInChildren<SpriteRenderer>().color = new Color(colArea.r, colArea.g, colArea.b, 0); //se deja de nuevo la imagen con opacidad 0 para que no se vea
        if (!buildCon.enabled)
        {
            foreach (Unidad uni in unidadesSeleccionadas)
            {
                uni.MostrarSelectorUnidad(false);
            }
            unidadesSeleccionadas.Clear();
            //se añaden las unidades que sean aliados
            List<Collider2D> colliderUnidades = Physics2D.OverlapAreaAll(inicioArrastreRaton, finArrastreRaton).Where(col => col.gameObject.GetComponent<Unidad>() != null).ToList();
            foreach (Collider2D colli in colliderUnidades)
            {
                unidadesSeleccionadas.Add(colli.gameObject.GetComponent<Unidad>());
                colli.gameObject.GetComponent<Unidad>().MostrarSelectorUnidad(true);
            }


        }

    }

    private void EscalarAreaSeleccion()
    {
        if (estaPresionandoBotonIzquierdo)
        {

            //esta función hace que el área de selección de unidades cambie de tamaño a medida que muevas el ratón mientras tengas el botón izquierdo pulsado
            Vector2 posicionActual = Camera.main.ScreenToWorldPoint(controles.RTS.PosicionCursor.ReadValue<Vector2>());

            /*El jugador puede arrastrar el ratón de dos maneras en cada eje maneras: de derecha a izquierda o de izquierda a derecha (X) y de arriba abajo o de abajo arriba (Y). Esto resultará en los vértices superior izquierdo e inferior derecho tomando una de varias posibles posiciones entre ambos.
             * La posicion actual del cursor puede ser mayor, igual o menor que la de inicio, tanto en un eje como en otro. Por eso, para hacer que refleje ese cambio es necesario que las coordenadas de uno de los puntos sean lo más pequeñas que sea posible y las del otro, lo más grandes
             * Esto formará un rectángulo a partir de dos esquinas opuestas
             */

            Vector2 puntoMenor = new Vector2(Mathf.Min(inicioArrastreRaton.x, posicionActual.x), Mathf.Min(inicioArrastreRaton.y, posicionActual.y));

            Vector2 puntoMayor = new Vector2(Mathf.Max(inicioArrastreRaton.x, posicionActual.x), Mathf.Max(inicioArrastreRaton.y, posicionActual.y));

            areaSeleccion.transform.position = puntoMenor; //actúa como pivote cuando se arrastre el ratón. inicioArrastreRaton no sirve porque está fijo
            areaSeleccion.transform.localScale = puntoMayor - puntoMenor; //la resta de dos vectores opuestos determina a cuánto están uno de otro y escalar X e Y acorde. Z es cero en este caso por ser un juego 2D


        }
    }

    private void DarOrdenesUnidades()
    {

        /* Ya se tienen unidades seleccionadas y no se está construyendo ningún edificio.
         * 
         * Ahora se mira sobre qué está el cursor al momento de levantar el botón. Hay varios casos: 
        -si es un recurso, que sólo los civiles vayan allá
        -si es un enemigo,que vayan sólo los guerreros y los magos
        -si no hay nada, simplemente se desplazan

        En cualquier caso, deben colocarse en torno al punto indicado sin solaparse entre sí Y esquivar los obstáculos que se encuentren por el camino
        */

        if (unidadesSeleccionadas.Count > 0) //debes pulsar el botón derecho del ratón y tener al menos 1 unidad seleccionada

        {

            Vector2 posicionActual = Camera.main.ScreenToWorldPoint(controles.RTS.PosicionCursor.ReadValue<Vector2>());
            Vector3Int tpos = tileSuelo.WorldToCell(posicionActual);

            if (tileSuelo.HasTile(tpos))

            {
                Debug.Log("Alola");
                List<Collider2D> colliders = Physics2D.OverlapPointAll(posicionActual).Where(collid => collid.gameObject.GetComponent<FuenteRecursosOperaciones>() != null).ToList();

                int valorCelda = GameManager.manager.gridCiudad[tpos.x, tpos.y];
                List<Unidad> aux = new List<Unidad>(unidadesSeleccionadas);
                int unidadesYaEnCamino = 0;
                foreach (Unidad unidad in aux)
                {
                    if (unidad.objetivoActual != null)
                    {
                        if (unidad.objetivoActual.GetComponent<FuenteRecursosOperaciones>() != null)
                        {
                            unidad.objetivoActual.GetComponent<FuenteRecursosOperaciones>().QuitarUnidad(unidad);
                        }
                        else if (false)
                        { //sustituir por componente del enemigo

                        }
                        unidad.LiberarUnidad();
                    }
                    //usar las mismas condicionales que a la hora de crear la casa para comprobar que están dentro de la cuadrícula, y luego hacer que se muevan y hagan las acciones mencionadas antes segñun el contenido de la casilla
                    switch (valorCelda)
                    {
                        case 0: //Terreno vacío
                            Debug.Log("Tereno vacío");
                            unidad.posicionObjetivo = posicionActual;
                            break;
                        case 1: //Recurso

                            if (colliders.Count > 0)
                            //si hay algún recurso en este punto, se tomará el primero y se asignará una serie de unidades para trabajar en ella. 
                            //Estas unidades serán apartadas de la lista de unidades seleccionadas y, en caso de haber más de las que acepta el recurso, el resto deberán seguir estando seleccionadas
                            {
                                int unidadesAsignadasRecurso = colliders[0].gameObject.GetComponent<FuenteRecursosOperaciones>().unidadesAsignadas.Count;

                                int unidadesMaximasRecurso = colliders[0].gameObject.GetComponent<FuenteRecursosOperaciones>().fuente.limiteUnidadesAsignadas;

                                int numeroUnidadesSeleccionadas = unidadesSeleccionadas.Count;

                                Debug.Log("La suma total de unidades asignadas a este recurso es:" + (unidadesAsignadasRecurso + numeroUnidadesSeleccionadas));

                                if (unidad.unidad.tipo == UnidadScriptable.TipoUnidad.Civil) //separo este if en dos porque se puede elegir soldados también, pero estos no pueden recolectar. Es prioritario ver que
                                                                                             //son civiles y luego contar cuántos civiles tienes
                                {

                                    if (unidadesAsignadasRecurso + numeroUnidadesSeleccionadas <= unidadesMaximasRecurso)
                                    {

                                        unidad.objetivoActual = colliders[0].gameObject;
                                        unidadesSeleccionadas.Remove(unidad);
                                        unidad.MostrarSelectorUnidad(false);
                                        unidadesYaEnCamino++;

                                    }
                                    else
                                    {
                                        //hay que mirar cuántas unidades tienen asignado este recurso y asignar las que falten (si es que hay sitio)
                                        int diferencia = unidadesMaximasRecurso - unidadesAsignadasRecurso;
                                        Debug.Log("La diferencia entre el maximo de unidades posible y las asignadas es: " + diferencia);
                                        if (diferencia > unidadesYaEnCamino)
                                        {
                                            unidad.objetivoActual = colliders[0].gameObject;
                                            unidadesSeleccionadas.Remove(unidad);
                                            unidad.MostrarSelectorUnidad(false);
                                            unidadesYaEnCamino++;
                                        }
                                        /*for(int i=numeroUnidadesSeleccionadas-1;unidadesSeleccionadas.Count==diferencia; i--)
                                        {
                                            unidadesSeleccionadas.Remove(unidadesSeleccionadas[i]);
                                            unidad.MostrarSelectorUnidad(false);
                                        }

                                        Debug.Log("El tamaño de la lista de unidades seleccionadas es de " + unidadesSeleccionadas.Count);

                                        if(unidadesSeleccionadas.Count > 0)
                                        {
                                            List<Unidad> aux2 = new List<Unidad>(unidadesSeleccionadas);
                                            foreach (Unidad un in aux2)
                                            {
                                                unidad.objetivoActual = colliders[0].gameObject;
                                                unidadesSeleccionadas.Remove(unidad);
                                                unidad.MostrarSelectorUnidad(false);
                                            }
                                        }*/

                                    }



                                }



                            }
                            break;
                        case 2: //Edificio por construir
                            break;
                        default: // en los demás casos, se mira si hay un enemigo en la zona cercana al ratón
                            FinSeleccionarUnidades();
                            break;
                    }
                }

            }
            else
            {
                FinSeleccionarUnidades();
            }


        }



    }

    void OnEnable()
    {
        controles.RTS.Enable();
    }
    void OnDisable()
    {
        controles.RTS.Disable();
    }
}
