using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuenteRecursos : MonoBehaviour
{
    [Tooltip("El orden de los costes es: oro, madera, piedra, fruta, cebada, agua, metal, carne")]
    public int indiceRecurso;
    public int cantidad;
    public SpriteRenderer spriteRendererEdificio;
    public TextMeshProUGUI textoSumaRecurso;

    public void ProporcionarRecurso()
    {
        for(int i = 0; i < Recursos.recursos.Count; i++)
        {
            Recursos.SumarRecurso(i,cantidad);
        }
    }
}
