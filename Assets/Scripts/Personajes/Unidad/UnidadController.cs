using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.AI;

public class UnidadController : MonoBehaviour
{
    #region variables

    PlayerControls controles;
    [Header("Posiciones del �rea bajo el rat�n")]
    Vector2 inicioArrastreRaton = Vector2.zero;
    Vector2 finArrastreRaton = Vector2.zero;
    GameObject areaSeleccion;
    Color colArea;

    [Header("Controlador de construcci�n de edificios")]
    BuildController buildCon;

    [Header("Determina si se ha levantado el bot�n del rat�n para realizar la selecci�n")]
    bool estaPresionandoBotonIzquierdo = false;
    Tilemap tileSuelo;
    int unidadesYaEnCamino = 0;

    [Header("Panel de unidad seleccionada")]
    public GameObject panelUnidad;
    public Text vidaMaxima;
    public Text vidaActual;
    public Image iconoPersonaje;
    public Image barraVida;
    public Text tipoUnidad;

    public GameObject seguirRaton;
    public List<Unidad> unidadesSeleccionadas = new List<Unidad>();

    [Header("SFX")]
    AudioController audioC;
    public AudioClip enviarAConstruir;

    #endregion

    private void Awake()
    {
        controles = new PlayerControls();

        //suscripci�n a los controles correspondientes

        controles.RTS.SeleccionarItem.performed += ctx => InicioSeleccionarUnidades();
        controles.RTS.SeleccionarItem.canceled += ctx => FinSeleccionarUnidades();
        controles.RTS.BorrarEdificioCancelar.performed += ctx => DarOrdenesUnidades();

        buildCon = GameObject.FindObjectOfType<BuildController>();
        areaSeleccion = GameObject.Find("--area seleccion--");
        colArea = areaSeleccion.GetComponentInChildren<SpriteRenderer>().color;
        areaSeleccion.GetComponentInChildren<SpriteRenderer>().color = new Color(colArea.r, colArea.g, colArea.b, 0); //se deja la imagen con opacidad 0 para que no se vea
        tileSuelo = GameObject.Find("Tilemap-Suelo").GetComponent<Tilemap>();
    }

    #region Start y Update


    void Start()
    {
        audioC = FindObjectOfType<AudioController>();
        panelUnidad.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        EscalarAreaSeleccion();
    }

    #endregion

    #region Control de seleccion de unidades

    private void InicioSeleccionarUnidades()
    {
        if (Camera.main != null)
        {

            inicioArrastreRaton = Camera.main.ScreenToWorldPoint(controles.RTS.PosicionCursor.ReadValue<Vector2>());

            areaSeleccion.GetComponentInChildren<SpriteRenderer>().color = new Color(colArea.r, colArea.g, colArea.b, 0.6f); //se deja la imagen con opacidad 1 para que se vea lo que se est� seleccionando

            estaPresionandoBotonIzquierdo = true;

        }

    }

    private void FinSeleccionarUnidades()
    {
        if (Camera.main != null)
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
                //se a�aden las unidades que sean aliados
                List<Collider2D> colliderUnidades = Physics2D.OverlapAreaAll(inicioArrastreRaton, finArrastreRaton).Where(col => col.gameObject.GetComponent<Unidad>() != null
                && col.gameObject.GetComponent<Unidad>().unidad.bando == UnidadScriptable.Bando.Jugador).ToList();
                foreach (Collider2D colli in colliderUnidades)
                {
                    unidadesSeleccionadas.Add(colli.gameObject.GetComponent<Unidad>());
                    colli.gameObject.GetComponent<Unidad>().MostrarSelectorUnidad(true);
                }

                areaSeleccion.transform.localScale = new Vector3(1f, 1f);

            }

        }

    }

    #endregion

    #region Escalado de area de seleccion

    private void EscalarAreaSeleccion()
    {
        if (estaPresionandoBotonIzquierdo)
        {
            if (Camera.main != null)
            {

                //esta funci�n hace que el �rea de selecci�n de unidades cambie de tama�o a medida que muevas el rat�n mientras tengas el bot�n izquierdo pulsado
                Vector2 posicionActual = Camera.main.ScreenToWorldPoint(controles.RTS.PosicionCursor.ReadValue<Vector2>());

                /*El jugador puede arrastrar el rat�n de dos maneras en cada eje maneras: de derecha a izquierda o de izquierda a derecha (X) y de arriba abajo o de abajo arriba (Y). Esto resultar� en los v�rtices superior izquierdo e inferior derecho tomando una de varias posibles posiciones entre ambos.
                 * La posicion actual del cursor puede ser mayor, igual o menor que la de inicio, tanto en un eje como en otro. Por eso, para hacer que refleje ese cambio es necesario que las coordenadas de uno de los puntos sean lo m�s peque�as que sea posible y las del otro, lo m�s grandes
                 * Esto formar� un rect�ngulo a partir de dos esquinas opuestas
                 */

                Vector2 puntoMenor = new Vector2(Mathf.Min(inicioArrastreRaton.x, posicionActual.x), Mathf.Min(inicioArrastreRaton.y, posicionActual.y));

                Vector2 puntoMayor = new Vector2(Mathf.Max(inicioArrastreRaton.x, posicionActual.x), Mathf.Max(inicioArrastreRaton.y, posicionActual.y));

                areaSeleccion.transform.position = puntoMenor; //act�a como pivote cuando se arrastre el rat�n. inicioArrastreRaton no sirve porque est� fijo
                areaSeleccion.transform.localScale = puntoMayor - puntoMenor; //la resta de dos vectores opuestos determina a cu�nto est�n uno de otro y escalar X e Y acorde. Z es cero en este caso por ser un juego 2D

            }

        }
    }

    #endregion

    #region M�todos para desplazar y dar �rdenes a las unidades

    private void DarOrdenesUnidades()
    {

        /* Ya se tienen unidades seleccionadas y no se est� construyendo ning�n edificio.
         * 
         * Ahora se mira sobre qu� est� el cursor al momento de levantar el bot�n. Hay varios casos: 
        -si es un recurso, que s�lo los civiles vayan all�
        -si es un enemigo,que vayan s�lo los guerreros y los magos
        -si no hay nada, simplemente se desplazan

        En cualquier caso, deben colocarse en torno al punto indicado sin solaparse entre s� Y esquivar los obst�culos que se encuentren por el camino
        */

        if (unidadesSeleccionadas.Count > 0) //debes pulsar el bot�n derecho del rat�n y tener al menos 1 unidad seleccionada

        {

            Vector2 posicionActual = Camera.main.ScreenToWorldPoint(controles.RTS.PosicionCursor.ReadValue<Vector2>());

            Vector3Int tpos = tileSuelo.WorldToCell(posicionActual);


            if (tileSuelo.HasTile(tpos))

            {

                List<Collider2D> collidersRecursos = Physics2D.OverlapPointAll(posicionActual).Where(collid => collid.gameObject.GetComponent<FuenteRecursosOperaciones>() != null).ToList();

                List<Collider2D> collidersEnemigos = Physics2D.OverlapPointAll(posicionActual).Where(collid => collid.gameObject.GetComponent<UnidadEnemiga>() != null).ToList();

                List<Collider2D> collidersEdificiosCons = Physics2D.OverlapPointAll(posicionActual).Where(collid => collid.gameObject.GetComponent<Edificio>() != null).ToList();

                int valorCelda;

                if (GameManager.manager != null)
                {

                    valorCelda = GameManager.manager.gridCiudad[tpos.x, tpos.y];
                }
                else
                {
                    valorCelda = FindObjectOfType<GameManagerTutorial>().gridCiudad[tpos.x, tpos.y];
                }


                List<Unidad> aux = new List<Unidad>(unidadesSeleccionadas);


                foreach (Unidad unidad in aux)
                {



                    if (unidad.objetivoActual != null)
                    {
                        if (unidad.objetivoActual.GetComponent<FuenteRecursosOperaciones>() != null)
                        {
                            unidad.objetivoActual.GetComponent<FuenteRecursosOperaciones>().QuitarUnidad(unidad);
                        }
                        else if (unidad.objetivoActual.GetComponent<UnidadEnemiga>() != null)
                        {
                            unidad.objetivoActual.GetComponent<UnidadEnemiga>().QuitarUnidad(unidad);
                        }
                        else if (unidad.objetivoActual.GetComponent<Edificio>() != null)
                        {
                            unidad.objetivoActual.GetComponent<Edificio>().QuitarUnidad(unidad);
                        }
                        unidad.LiberarUnidad();
                    }
                    //usar las mismas condicionales que a la hora de crear la casa para comprobar que est�n dentro de la cuadr�cula, y luego hacer que se muevan y hagan las acciones mencionadas antes seg�un el contenido de la casilla
                    switch (valorCelda)
                    {
                        case 0: //Terreno vac�o


                            if (collidersEnemigos.Count == 0)
                            {
                                unidad.posicionObjetivo = posicionActual;
                                //seguirRaton.transform.position = posicionActual;
                                if (unidad.unidad.tipo == UnidadScriptable.TipoUnidad.Civil)
                                {
                                    unidad.GetComponent<NavMeshAgent>().stoppingDistance = 0.5f;
                                }
                                unidad.GetComponent<NavMeshAgent>().SetDestination(posicionActual);
                            }
                            else
                            {
                                EnviarUnidadesSegunNumero(collidersEnemigos, unidad, 1);
                            }
                            break;
                        case 1: //Madera

                            if (collidersRecursos.Count > 0)
                            //si hay alg�n recurso en este punto, se tomar� el primero y se asignar� una serie de unidades para trabajar en ella. 
                            //Estas unidades ser�n apartadas de la lista de unidades seleccionadas y, en caso de haber m�s de las que acepta el recurso, el resto deber�n seguir estando seleccionadas
                            {

                                EnviarUnidadesSegunNumero(collidersRecursos, unidad, 0);

                            }
                            break;
                        case 2: //Piedra
                            if (collidersRecursos.Count > 0)
                            //si hay alg�n recurso en este punto, se tomar� el primero y se asignar� una serie de unidades para trabajar en ella. 
                            //Estas unidades ser�n apartadas de la lista de unidades seleccionadas y, en caso de haber m�s de las que acepta el recurso, el resto deber�n seguir estando seleccionadas
                            {

                                EnviarUnidadesSegunNumero(collidersRecursos, unidad, 0);

                            }

                            break;
                        case 3: //Edificio por construir (si var�a, c�mbialo tambi�n en BuildController)
                            if (collidersEdificiosCons.Count > 0)
                            {

                                EnviarUnidadesSegunNumero(collidersEdificiosCons, unidad, 2);
                                if(audioC != null)
                                {
                                    audioC.PlaySFX(enviarAConstruir);
                                }
                            }
                            break;
                        default: // en los dem�s casos, se mira si hay un enemigo en la zona cercana al rat�n
                            FinSeleccionarUnidades();
                            break;
                    }
                }

            }
            else
            {
                FinSeleccionarUnidades();
            }

            unidadesYaEnCamino = 0;

        }


    }


    private void EnviarUnidadesSegunNumero(List<Collider2D> colliders, Unidad unidad, int tipoCollider)
    {
        int unidadesAsignadasRecurso;
        int unidadesMaximasRecurso;
        int numeroUnidadesSeleccionadas = unidadesSeleccionadas.Count;

        switch (tipoCollider)
        {
            case 0: //recurso
                unidadesAsignadasRecurso = colliders[0].gameObject.GetComponent<FuenteRecursosOperaciones>().unidadesAsignadas.Count;

                unidadesMaximasRecurso = colliders[0].gameObject.GetComponent<FuenteRecursosOperaciones>().fuente.limiteUnidadesAsignadas;

                if (unidad.unidad.tipo == UnidadScriptable.TipoUnidad.Civil) //separo este if en dos porque se puede elegir soldados tambi�n, pero estos no pueden recolectar. Es prioritario ver que
                                                                             //son civiles y luego contar cu�ntos civiles tienes
                {

                    //hay que mirar cu�ntas unidades tienen asignado este recurso y asignar las que falten (si es que hay sitio)
                    int diferencia = unidadesMaximasRecurso - unidadesAsignadasRecurso;

                    if (diferencia > 0)
                    {

                        unidad.objetivoActual = colliders[0].gameObject;

                        if (unidad.objetivoActual.GetComponent<FuenteRecursosOperaciones>().fuente.indiceRecurso == 1) //madera
                        {
                            unidad.SetTipoAccion(1);
                            unidad.SetAnimAtaque(0f, 0f, false);

                        }
                        else
                        {
                            unidad.SetTipoAccion(2);
                            unidad.SetAnimAtaque(0f, 0f, false);
                        }

                        unidad.GetComponent<NavMeshAgent>().stoppingDistance = 0;
                        unidad.GetComponent<NavMeshAgent>().SetDestination(colliders[0].gameObject.transform.position);
                        unidad.objetivoActual.GetComponent<FuenteRecursosOperaciones>().AsignarUnidad(unidad);
                        unidadesSeleccionadas.Remove(unidad);
                        unidad.MostrarSelectorUnidad(false);

                    }

                    //}

                }

                break;
            case 1: //enemigo
                unidadesAsignadasRecurso = colliders[0].gameObject.GetComponent<UnidadEnemiga>().unidadesAsignadas.Count;

                unidadesMaximasRecurso = colliders[0].gameObject.GetComponent<UnidadEnemiga>().unidad.limiteUnidadesAsignadas;




                if (unidad.unidad.tipo != UnidadScriptable.TipoUnidad.Civil) //separo este if en dos porque se puede elegir soldados tambi�n, pero estos no pueden recolectar. Es prioritario ver que
                                                                             //son civiles y luego contar cu�ntos civiles tienes
                {
                  
                    //hay que mirar cu�ntas unidades tienen asignado este recurso y asignar las que falten (si es que hay sitio)
                    int diferencia = unidadesMaximasRecurso - unidadesAsignadasRecurso;

                    if (diferencia > 0)
                    {
                        unidad.objetivoActual = colliders[0].gameObject;

                        unidad.GetComponent<NavMeshAgent>().SetDestination(colliders[0].gameObject.transform.position);
                        unidad.objetivoActual.GetComponent<UnidadEnemiga>().AddUnidad(unidad);
                        unidadesSeleccionadas.Remove(unidad);
                        unidad.MostrarSelectorUnidad(false);
                        unidadesYaEnCamino++;
                    }

                }

                //}
                break;


            case 2: //edificio en construccion
                unidadesAsignadasRecurso = colliders[0].gameObject.GetComponent<Edificio>().unidadesAsignadas.Count;

                unidadesMaximasRecurso = colliders[0].gameObject.GetComponent<Edificio>().edificioData.limiteCiviles;

                if (unidad.unidad.tipo == UnidadScriptable.TipoUnidad.Civil) //separo este if en dos porque se puede elegir soldados tambi�n, pero estos no pueden recolectar. Es prioritario ver que
                                                                             //son civiles y luego contar cu�ntos civiles tienes
                {

                    //hay que mirar cu�ntas unidades tienen asignado este recurso y asignar las que falten (si es que hay sitio)
                    int diferencia = unidadesMaximasRecurso - unidadesAsignadasRecurso;

                    if (diferencia > 0)
                    {
                        unidad.objetivoActual = colliders[0].gameObject;
                       
                        unidad.SetTipoAccion(3);
                        unidad.GetComponent<NavMeshAgent>().stoppingDistance = 1f;
                        unidad.GetComponent<NavMeshAgent>().SetDestination(colliders[0].gameObject.transform.position);
                        unidad.objetivoActual.GetComponent<Edificio>().AsignarUnidad(unidad);
                        unidadesSeleccionadas.Remove(unidad);
                        unidad.MostrarSelectorUnidad(false);
                        unidadesYaEnCamino++;
                    }

                }

                break;

        }


    }

    #endregion

    #region Funciones del nuevo sistema de input

    void OnEnable()
    {
        controles.RTS.Enable();
    }
    void OnDisable()
    {
        controles.RTS.Disable();
    }

    #endregion
}
