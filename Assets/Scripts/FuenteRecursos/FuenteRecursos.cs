using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuenteRecursos : MonoBehaviour
{
    public List<int> recursosProporcionados = new List<int>() { };
    public SpriteRenderer spriteRendererEdificio;
    public TextMeshProUGUI textoSumaRecurso;

    public void ProporcionarRecurso()
    {
        for(int i = 0; i < Recursos.recursos.Count; i++)
        {
            Recursos.SumarRecurso(i,recursosProporcionados[i]);
        }
    }
}
