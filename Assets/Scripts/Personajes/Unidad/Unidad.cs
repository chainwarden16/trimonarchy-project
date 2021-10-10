using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using System.Linq;

public class Unidad : MonoBehaviour
{
    [Header("Estadísticas de la unidad")]
    public UnidadScriptable unidad;
    public int vidaActual;
    float enfriamientoAtaqueRestante;
    int tipoAccion = 0;
    bool estaMuerto = false;

    [Header("Propiedades visuales")]
    SpriteRenderer renderer;
    Animator anim;
    NavMeshAgent agente;

    [Header("Propiedades de selección y cumplimiento de órdenes")]
    GameObject selector; //activa el indicador de selección de unidades cuando el jugador arrastra el ratón sobre ella
    public GameObject objetivoActual; //determina si la unidad ya tiene un objetivo asignado
    public Vector3 posicionObjetivo; //indica si el personaje se desplaza a una zona en concreto
    bool ejecutandoAccion = false; //sirve para determinar si el personaje está ejecutando una acción
    UnidadController controlUnidades;
    public List<UnidadEnemiga> unidadesAsignadas = new List<UnidadEnemiga>(); //indica el número de enemigos que lo están atacando

    private void Awake()
    {
        selector = transform.Find("Selector").gameObject;
        MostrarSelectorUnidad(false);
        posicionObjetivo = gameObject.transform.position;
        vidaActual = unidad.vida;
        enfriamientoAtaqueRestante = 0f;
        controlUnidades = FindObjectOfType<UnidadController>();
        agente = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //el selector empieza apagado, pues no se tienen unidades escogidas
    }

    private void Update()
    {
        Morir();
        if (!estaMuerto)

        {
            Desplazarse();
            AtacarUnidad();
            ActualizarAnimacion();

        }
    }

    public void MostrarSelectorUnidad(bool debeMostrarse)
    {
        selector.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 * Convert.ToInt32(debeMostrarse)); //si es verdadero, tendrá opacidad 1 (100%); si no, opacidad 0 (0%). Los booleanos valen siempre 0 o 1
    }

    #region Acciones principales

    public void Desplazarse()
    {
        //Las unidades se desplazan hacia el punto que el jugador indique
        //si tiene un objetivo al que dirigirse O un punto nuevo al que caminar Y a su vez no está haciendo ya algo


        /*if (posicionObjetivo != gameObject.transform.position && objetivoActual == null)

        {

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, posicionObjetivo, Time.deltaTime*3);

            
            if (Mathf.Abs(Vector3.Distance(gameObject.transform.position, posicionObjetivo)) <= 1f)
            {

                posicionObjetivo = gameObject.transform.position;

            }


        }*/

        if (objetivoActual != null)
        {
            Debug.Log("Tengo un objetivo");

            if (Mathf.Abs(Vector3.Distance(gameObject.transform.position, objetivoActual.transform.position)) > unidad.rangoAtaque)
            {
                ejecutandoAccion = false;
                //Se deja por si es necesario añadir algo

            }

            else
            {
                //GetComponent<AIDestinationSetter>().target = null;
                ejecutandoAccion = true;
                posicionObjetivo = gameObject.transform.position;

            }
        }

        //si es un soldado y no está atacando a nadie cercano, ve a por un objetivo

        else if (objetivoActual == null && unidad.tipo != UnidadScriptable.TipoUnidad.Civil)
        {
            List<UnidadEnemiga> soldadosRestantes = FindObjectsOfType<UnidadEnemiga>().Where(un => un.unidad.tipo != UnidadScriptable.TipoUnidad.Civil).ToList();

            if (soldadosRestantes.Count != 0)
            {
                for (int i = 0; i < soldadosRestantes.Count; i++)
                {
                    if (soldadosRestantes[i].unidadesAsignadas.Count < soldadosRestantes[i].unidad.limiteUnidadesAsignadas && Mathf.Abs(Vector2.Distance(soldadosRestantes[i].transform.position, gameObject.transform.position)) < 4f)
                    {
                        objetivoActual = soldadosRestantes[i].gameObject;
                        objetivoActual.GetComponent<UnidadEnemiga>().AddUnidad(this);
                        agente.SetDestination(objetivoActual.transform.position);
                        break;
                    }
                }
            }
        }


    }

    public void Morir()
    {
        if (vidaActual <= 0)
        {
            estaMuerto = true;
            foreach (UnidadEnemiga uni in unidadesAsignadas)
            {
                uni.LiberarUnidad();
            }
            Debug.Log("Me he morío");
            if (unidad.tipo != UnidadScriptable.TipoUnidad.Civil)
            {
                Recursos.soldados--;
            }
            /*if (controlUnidades.unidadesSeleccionadas[0] == this)
            {
                controlUnidades.CerrarPanel();
            }*/
            controlUnidades.unidadesSeleccionadas.Remove(this);
            Destroy(gameObject);
        }
    }

    public void AtacarUnidad()
    {
        if (objetivoActual != null && objetivoActual.GetComponent<UnidadEnemiga>() != null && objetivoActual.GetComponent<UnidadEnemiga>().unidad.bando != unidad.bando && ejecutandoAccion)
        { //si el objetivo no es nulo, tiene componente unidad Y no es el del mismo bando, atácalo
            UnidadEnemiga unidadEnemiga = objetivoActual.GetComponent<UnidadEnemiga>();

            Debug.Log(gameObject.name + " va a atacar a un enemigo que tiene todavía " + unidadEnemiga.vidaActual + " puntos de vida");

            if (enfriamientoAtaqueRestante <= 0) //claro que para eso debe haber pasado el tiempo de enfriamiento
            {
                if (unidadEnemiga.vidaActual - unidad.fuerza < 0)
                {
                    unidadEnemiga.vidaActual = 0;
                }
                else
                {
                    unidadEnemiga.vidaActual -= unidad.fuerza;
                    enfriamientoAtaqueRestante = unidad.tiempoEnfriamientoAtaque;
                }
            }
            else
            {
                enfriamientoAtaqueRestante -= Time.deltaTime;
            }

        }
    }

    public void LiberarUnidad()
    {
        if (anim != null)
        {

            if (unidad.tipo == UnidadScriptable.TipoUnidad.Civil)
            {
                SetTipoAccion(0);
            }
            SetAnimCaminar(0f, 0f, false);
            SetAnimAtaque(0f, 0f, false);


        }
        ejecutandoAccion = false;
        objetivoActual = null;
    }

    private void ActualizarAnimacion()
    {

        if (ejecutandoAccion)
        {
            //si es un civil, está trabajando en un recurso o construyendo una casa
            if (unidad.tipo == UnidadScriptable.TipoUnidad.Civil)
            {
                SetAnimCaminar(0f, 0f, false);
                //SetAnimConstruirRecoger();
                float diferenciaX = agente.destination.x - gameObject.transform.position.x;
                float diferenciaY = agente.destination.y - gameObject.transform.position.y;
                bool esXmayor = Mathf.Abs(agente.destination.x - gameObject.transform.position.x) > Mathf.Abs(agente.destination.y - gameObject.transform.position.y);

                if (esXmayor)
                {
                    if (diferenciaX < 0) //está a su izquierda
                    {
                        SetAnimAtaque(-1f, 0, true);

                    }
                    else //está a su derecha
                    {
                        SetAnimAtaque(1f, 0, true);
                    }
                }
                else
                {
                    //Debug.Log(diferenciaY);
                    Debug.Log("La posición de la piedra es: " + agente.destination.y);
                    Debug.Log("La posición del objeto es: " + gameObject.transform.position.y);
                    Debug.Log("La posición del agente es: " + agente.transform.position.y);

                    if (diferenciaY <= 0f) //está debajo
                    {
                        if (tipoAccion != 2)
                        {

                            SetAnimAtaque(0, 1f, true);
                            Debug.Log("Se encuentra debajo y se pone a " + anim.GetFloat("atacarZ"));

                        }
                        else
                        {
                            SetAnimAtaque(0, -1f, true);
                            Debug.Log("Se encuentra debajo y se pone a " + anim.GetFloat("atacarZ"));
                        }
                    }
                    else //está encima
                    {
                        if (tipoAccion != 2)
                        {

                            SetAnimAtaque(0, -1f, true);
                            Debug.Log("Se encuentra debajo y se pone a " + anim.GetFloat("atacarZ"));

                        }
                        else
                        {
                            SetAnimAtaque(0, 1f, true);
                            Debug.Log("Se encuentra debajo y se pone a " + anim.GetFloat("atacarZ"));
                        }
                    }
                }

            }
            //si no, es un soldado (del tipo que sea) y está atacando
            else
            {

                SetAnimCaminar(0f, 0f, false);

                bool esXmayor = Mathf.Abs(agente.destination.x - gameObject.transform.position.x) > Mathf.Abs(agente.destination.y - gameObject.transform.position.y);
                float diferenciaX = agente.destination.x - gameObject.transform.position.x;
                float diferenciaY = agente.destination.y - gameObject.transform.position.y;

                if (esXmayor)
                {
                    if (diferenciaX < 0) //está a su izquierda
                    {
                        SetAnimAtaque(-1f, 0, true);

                    }
                    else //está a su derecha
                    {
                        SetAnimAtaque(1f, 0, true);
                    }
                }
                else
                {
                    if (diferenciaY <= 0) //está debajo
                    {
                        SetAnimAtaque(0, 1f, true);
                    }
                    else //está encima
                    {
                        SetAnimAtaque(0, -1f, true);
                    }
                }
            }
        }
        //si se está moviendo sin hacer una acción, entonces debe verse cómo camina
        else if (agente.velocity != Vector3.zero)
        {

            /*if(agente.velocity.x < 0 && Mathf.Abs(agente.velocity.x) > Mathf.Abs(agente.velocity.y))
            {
                anim.SetFloat("movZ", 0f);
                anim.SetFloat("movX", -1f);
                anim.SetFloat("atacarZ", 0f);
                anim.SetFloat("atacarX", 0f);
            }*/

            SetAnimAtaque(0f, 0f, false);
            SetAnimCaminar(agente.velocity.x, agente.velocity.y, true);
            //SetTipoAccion(0);

        }
        //Está quieto, sin realizar acciones ni caminar
        else
        {
            //SetTipoAccion(0);
            SetAnimAtaque(0f, 0f, false);
            SetAnimCaminar(0f, 0f, false);
            anim.SetFloat("mirarZ", 0f);
            anim.SetFloat("mirarX", 0f);
        }

    }

    #endregion

    #region Agregar o quitar unidad

    public void AddUnidad(UnidadEnemiga unidad)
    {
        unidadesAsignadas.Add(unidad);
    }

    public void QuitarUnidad(UnidadEnemiga unidad)
    {
        unidadesAsignadas.Remove(unidad);
    }

    #endregion

    #region getters y setters

    public bool GetEjecutandoAccion()
    {
        return ejecutandoAccion;
    }

    public void SetAnimAtaque(float numeroX, float numeroZ, bool debeActivarse)
    {
        anim.SetBool("atacando", debeActivarse);
        anim.SetFloat("atacarX", numeroX);
        anim.SetFloat("atacarZ", numeroZ);

    }

    public void SetAnimCaminar(float numeroX, float numeroZ, bool debeActivarse)
    {
        anim.SetBool("caminando", debeActivarse);
        anim.SetFloat("movX", numeroX);
        anim.SetFloat("movZ", numeroZ);

    }

    private void SetAnimConstruirRecoger()
    {
        anim.SetInteger("tipoAccion", tipoAccion);
    }

    public void SetTipoAccion(int tipo)
    {
        tipoAccion = tipo;
        anim.SetInteger("tipoAccion", tipoAccion);
    }


    #endregion

}
