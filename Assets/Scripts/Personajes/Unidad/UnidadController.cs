using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnidadController : MonoBehaviour
{

    PlayerControls controles;
    Vector2 inicioArrastreRaton = Vector2.zero;
    Vector2 finArrastreRaton = Vector2.zero;
    BuildController buildCon;

    public List<Unidad> unidadesSeleccionadas = new List<Unidad>();

    private void Awake()
    {
        controles = new PlayerControls();

        //suscripción a los controles correspondientes

        controles.RTS.SeleccionarItem.performed += ctx => InicioSeleccionarUnidades();
        controles.RTS.SeleccionarItem.canceled += ctx => FinSeleccionarUnidades();
        controles.RTS.BorrarEdificioCancelar.performed += ctx => DeseleccionarUnidades();
        buildCon = GameObject.FindObjectOfType<BuildController>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InicioSeleccionarUnidades()
    {
        inicioArrastreRaton = Camera.main.ScreenToWorldPoint(controles.RTS.PosicionCursor.ReadValue<Vector2>());
        Debug.Log(inicioArrastreRaton);
    }

    private void FinSeleccionarUnidades()
    {

        finArrastreRaton = Camera.main.ScreenToWorldPoint(controles.RTS.PosicionCursor.ReadValue<Vector2>());
        if (!buildCon.enabled)
        {

            if (unidadesSeleccionadas.Count == 0)
            {
                //se añaden las unidades que sean aliados
                List<Collider2D> colliderUnidades = Physics2D.OverlapAreaAll(inicioArrastreRaton, finArrastreRaton).Where(col => col.gameObject.GetComponent<Unidad>()!=null).ToList(); //
                foreach(Collider2D colli in colliderUnidades)
                {
                    unidadesSeleccionadas.Add(colli.gameObject.GetComponent<Unidad>());
                }
                Debug.Log(unidadesSeleccionadas.Count);
            }
            else
            {
                /* Ya se tienen unidades seleccionadas y no se está construyendo ningún edificio.
                 * 
                 * Ahora se mira sobre qué está el cursor al momento de levantar el botón. Hay varios casos: 
                -si es un recurso, que sólo los civiles vayan allá
                -si es un enemigo,que vayan sólo los guerreros y los magos
                -si no hay nada, simplemente se desplazan

                En cualquier caso, deben colocarse en torno al punto indicado sin solaparse entre sí Y esquivar los obstáculos que se encuentren por el camino
                */

                
            }
        }

        Debug.Log(finArrastreRaton);

    }

    private void DeseleccionarUnidades()
    {
        Debug.Log("he deseleccionado unidades");
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
