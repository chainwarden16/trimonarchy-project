using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuenteRecursos : MonoBehaviour
{
    public List<int> recursosProporcionados = new List<int>() { };

    public void ProporcionarRecurso()
    {
        for(int i = 0; i < Recursos.recursos.Count; i++)
        {
            Recursos.SumarRecurso(i,recursosProporcionados[i]);
        }
    }
}
