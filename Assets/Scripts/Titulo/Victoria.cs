using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Victoria : MonoBehaviour
{
    public Text texto; 
    void Start()
    {
        texto.text = PlayerPrefs.GetString("TiempoGastado", "15:00") + " minutos";
    }

}
