using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuenteRecursosOperaciones : MonoBehaviour
{
    public FuenteRecursos fuente;

    public void ProporcionarRecurso()
    {
        for (int i = 0; i < Recursos.recursos.Count; i++)
        {
            Recursos.SumarRecurso(i, fuente.cantidad);
        }
    }
}
