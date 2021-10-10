using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class UnidadEnemiga : MonoBehaviour
{
    [Header("Estadísticas de la unidad")]
    public UnidadScriptable unidad;
    public int vidaActual;
    float enfriamientoAtaqueRestante;
    bool estaMuerto = false;

    [Header("Propiedades visuales")]
    SpriteRenderer renderer;
    Animator anim;
    NavMeshAgent agente;

    [Header("Propiedades de selección y cumplimiento de órdenes")]

    public GameObject objetivoActual; //determina si la unidad ya tiene un objetivo asignado
    public Vector3 posicionObjetivo; //indica si el personaje se desplaza a una zona en concreto
    bool ejecutandoAccion = false; //sirve para determinar si el personaje está ejecutando una acción

    public List<Unidad> unidadesAsignadas = new List<Unidad>(); //indica el número de enemigos que lo están atacando

    private void Awake()
    {
        posicionObjetivo = gameObject.transform.position;
        vidaActual = unidad.vida;
        enfriamientoAtaqueRestante = 0f;
    }
    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        agente = GetComponent<NavMeshAgent>();

        agente.updateRotation = false;
        agente.updateUpAxis = false;
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

    public void Desplazarse()
    {
        //Los enemigos buscan a un enemigo

        if (objetivoActual == null)
        {
            //se busca a una unidad del jugador que sea un soldado. Los civiles no importan, pues se perderá si matan a todos los soldados
            List<Unidad> soldadosRestantes = FindObjectsOfType<Unidad>().Where(un => un.unidad.tipo != UnidadScriptable.TipoUnidad.Civil).ToList();

            if (soldadosRestantes.Count != 0)
            {
                for (int i = 0; i < soldadosRestantes.Count; i++)
                {
                    if (soldadosRestantes[i].unidadesAsignadas.Count < soldadosRestantes[i].unidad.limiteUnidadesAsignadas)
                    {
                        objetivoActual = soldadosRestantes[i].gameObject;
                        objetivoActual.GetComponent<Unidad>().AddUnidad(this);
                        agente.SetDestination(objetivoActual.transform.position);
                        break;
                    }
                }
            }
        }

        else
        {

            if ((Mathf.Abs(Vector3.Distance(gameObject.transform.position, objetivoActual.transform.position)) >= unidad.rangoAtaque))
            {
                ejecutandoAccion = false;
                agente.SetDestination(objetivoActual.transform.position);
            }
            else
            {

                if (!ejecutandoAccion)
                {

                    Debug.Log("Tengo un objetivo");

                    if (objetivoActual.GetComponent<Unidad>() != null && objetivoActual.GetComponent<Unidad>().unidadesAsignadas.Contains(this)) //&& objetivoActual.GetComponent<Unidad>().unidadesAsignadas.Count < objetivoActual.GetComponent<Unidad>().unidad.limiteUnidadesAsignadas
                    {

                        ejecutandoAccion = true;
                    }

                }

                else if (objetivoActual.GetComponent<Unidad>().unidadesAsignadas.Count >= objetivoActual.GetComponent<Unidad>().unidad.limiteUnidadesAsignadas && !objetivoActual.GetComponent<Unidad>().unidadesAsignadas.Contains(this))
                {
                    Debug.Log("Dejo de atacar con " + this.name);
                    LiberarUnidad();
                }

                else if (ejecutandoAccion && objetivoActual.GetComponent<Unidad>().unidadesAsignadas.Contains(this))
                {
                    AtacarUnidad();

                }

            }


        }


    }

    public void Morir()
    {

        if (vidaActual <= 0)
        {
            estaMuerto = true;
            foreach (Unidad uni in unidadesAsignadas)
            {
                uni.LiberarUnidad();
            }
            Debug.Log("Me he morío (enemigo)");
            GameManager.manager.unidadesEnemigas.Remove(this);
            Destroy(gameObject);
        }
    }

    public void AtacarUnidad()
    {
        if (objetivoActual != null && objetivoActual.GetComponent<Unidad>() != null && objetivoActual.GetComponent<Unidad>().unidad.bando != unidad.bando && ejecutandoAccion)
        { //si el objetivo no es nulo, tiene componente unidad Y no es el del mismo bando, atácalo
            Unidad unidadJugador = objetivoActual.GetComponent<Unidad>();

            Debug.Log(gameObject.name + " va a atacar a un aliado que tiene todavía " + unidadJugador.vidaActual + " puntos de vida");

            if (enfriamientoAtaqueRestante <= 0) //claro que para eso debe haber pasado el tiempo de enfriamiento
            {
                if (unidadJugador.vidaActual - unidad.fuerza < 0)
                {
                    unidadJugador.vidaActual = 0;
                }
                else
                {
                    unidadJugador.vidaActual -= unidad.fuerza;
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

        ejecutandoAccion = false;
        objetivoActual = null;
    }

    private void ActualizarAnimacion()
    {

        if (ejecutandoAccion)
        {
            //si es un civil, está trabajando en un recurso o construyendo una casa


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


    public void AddUnidad(Unidad unidad)
    {
        unidadesAsignadas.Add(unidad);
    }

    public void QuitarUnidad(Unidad unidad)
    {
        unidadesAsignadas.Remove(unidad);
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

}
