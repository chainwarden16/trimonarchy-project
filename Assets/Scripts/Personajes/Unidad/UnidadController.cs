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
    GameObject areaSeleccion;
    Color colArea;
    bool estaPresionandoBotonIzquierdo = false;

    public List<Unidad> unidadesSeleccionadas = new List<Unidad>();

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

        areaSeleccion.GetComponentInChildren<SpriteRenderer>().color = new Color(colArea.r, colArea.g, colArea.b, 0.6f); //se deja la imagen con opacidad 1 para que se vea lo que se est� seleccionando

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
            //se a�aden las unidades que sean aliados
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
        Vector2 posicionActual = Camera.main.ScreenToWorldPoint(controles.RTS.PosicionCursor.ReadValue<Vector2>());

        foreach(Unidad unidad in unidadesSeleccionadas)
        {
            //usar las mismas condicionales que a la hora de crear la casa para comprobar que est�n dentro de la cuadr�cula, y luego hacer que se muevan y hagan las acciones mencionadas antes seg�un el contenido de la casilla
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
