using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinPartida : MonoBehaviour
{
    public void VolverAlTitulo()
    {
        SceneManager.LoadScene("Titulo");
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
