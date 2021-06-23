using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName ="FuenteRecursos", menuName ="FuenteScriptable/FuenteRecursos")]
public class FuenteRecursos : ScriptableObject
{
    [Tooltip("El orden de los costes es: oro, madera, piedra, fruta, cebada, agua, metal, carne")]
    public int indiceRecurso;
    public int cantidad;
    public SpriteRenderer spriteRendererEdificio;
    public TextMeshProUGUI textoSumaRecurso;

}
