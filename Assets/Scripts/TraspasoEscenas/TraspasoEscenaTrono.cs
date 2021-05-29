using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TraspasoEscenaTrono : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && Input.GetButtonDown("Submit"))
        {
            if (SceneManager.GetActiveScene().name == "Pasillo")
                SceneManager.LoadScene("SalaTrono"); //Sala del trono
            else if(SceneManager.GetActiveScene().name == "SalaTrono")
                SceneManager.LoadScene("MapaCiudad"); //Mapa de la ciudad donde se construye los edificios
        }
    }
}
