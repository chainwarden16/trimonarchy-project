using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TituloJuego : MonoBehaviour
{
    public void EmpezarPartida()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void MenuOpciones()
    {
        SceneManager.LoadScene("Opciones");
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
