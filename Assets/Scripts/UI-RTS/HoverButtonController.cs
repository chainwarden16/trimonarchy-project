using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverButtonController : MonoBehaviour
{
    [Header("Prefab del que sacar los datos")]
    GameObject prefabEdificio;

    [Header("Texto e iconos a modificar")]
    public Text nombreEdificio;
    public List<Image> iconosCoste;
    public List<Image> iconosBeneficio;
    public List<Sprite> spritesIconos;

    public List<Text> textosCoste;
    public List<Text> textosBeneficio;

    public GameObject panelHover;

    void Start()
    {
        prefabEdificio = gameObject.GetComponent<ButtonController>().edificioAConstruir;
        panelHover.SetActive(false);

    }

    public void MostrarPanel()
    {
        int indiceBeneficio = 0;
        nombreEdificio.text = prefabEdificio.GetComponent<Edificio>().edificioData.nombre;

        for (int i = 0; i < textosCoste.Count; i++) //actualiza los costes
        {
            textosCoste[i].text = prefabEdificio.GetComponent<Edificio>().edificioData.coste[i].ToString();
        }


        for(int i = 0; i < prefabEdificio.GetComponent<Edificio>().edificioData.materiales.Count; i++) //actualiza los beneficios
        {
            if(prefabEdificio.GetComponent<Edificio>().edificioData.materiales[i] != 0)
            {
                iconosBeneficio[indiceBeneficio].sprite = spritesIconos[i];
                iconosBeneficio[indiceBeneficio].color = new Color(1, 1, 1, 1);
                textosBeneficio[indiceBeneficio].text = prefabEdificio.GetComponent<Edificio>().edificioData.materiales[i].ToString();
                indiceBeneficio++;
            }
        }

        if (prefabEdificio.GetComponent<Edificio>().edificioData.beneficio[0] != 0)
        {
            iconosBeneficio[indiceBeneficio].sprite = spritesIconos[8];
            iconosBeneficio[indiceBeneficio].color = new Color(1, 1, 1, 1);
            textosBeneficio[indiceBeneficio].text = prefabEdificio.GetComponent<Edificio>().edificioData.beneficio[0].ToString();
            indiceBeneficio++;
        }
        else if(prefabEdificio.GetComponent<Edificio>().edificioData.beneficio[1] != 0)
        {
            iconosBeneficio[indiceBeneficio].sprite = spritesIconos[9];
            iconosBeneficio[indiceBeneficio].color = new Color(1, 1, 1, 1);
            textosBeneficio[indiceBeneficio].text = prefabEdificio.GetComponent<Edificio>().edificioData.beneficio[1].ToString();
            indiceBeneficio++;
        }


        if(indiceBeneficio < iconosBeneficio.Count) //pone el resto de cuadros vacíos a 0 o invisibles
        {
            for(int i = indiceBeneficio; i < iconosBeneficio.Count; i++)
            {
                iconosBeneficio[indiceBeneficio].color = new Color(1, 1, 1, 0);
                textosBeneficio[indiceBeneficio].text = "";
                indiceBeneficio++;
            }
        }

        if(prefabEdificio.GetComponent<Edificio>().edificioData.nombre == "Iglesia") //especifica la función de la iglesia, pues no depende de materiales ni da más ciudadanos o soldados
        {
            textosBeneficio[0].text = "Restaura algo de vida de tus soldados periódicamente";
        }

        //Hace aparecer el panel en sí
        panelHover.SetActive(true);
    }

    public void EsconderPanel()
    {
        panelHover.SetActive(false);
    }
}
