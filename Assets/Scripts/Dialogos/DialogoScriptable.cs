using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Dialogo", menuName ="DialogoScriptable/Dialogo")]
public class DialogoScriptable : ScriptableObject
{
    public Sprite personaje;
    public string nombrePersonaje;
    public string texto;
    public List<DialogoScriptable> posibilidadesDialogo;
}
