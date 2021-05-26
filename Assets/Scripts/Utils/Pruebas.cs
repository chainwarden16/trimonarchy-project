using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Pruebas : MonoBehaviour
{
    // Start is called before the first frame update
    bool estaTransparente = false;

    public Camera camara;

    bool existeCarpeta;

    bool existeFichero;

    byte[] hashcodeCorrecto;

    bool pararEstaEjecucion = false;

    void Start()
    {

        //Buscar archivo

        /*
        bool prueba = UtilityFunctions.IsFilePresent("mind.txt");

        print("¿El fichero mind.txt existe? La respuesta es: " + prueba);

        //Hacer camara invisible o visible

        //UtilityFunctions.CreateFolder("Test");

        */

    }

    // Update is called once per frame
    void Update()
    {
        /*
        existeCarpeta = UtilityFunctions.IsFolderPresent("Files");
        existeFichero = UtilityFunctions.IsFilePresent("Hey.txt", "Files");

        print("¿El directorio Files existe? La respuesta es: " + existeCarpeta);
        print("¿El fichero Hey.txt existe? La respuesta es: " + existeFichero);

        if (!pararEstaEjecucion)
        {

            string hash = UtilityFunctions.CalculateHashcodeSha("mind.txt", "Files");
            Debug.Log("¿Coinciden los hash? La respuesta es: " +UtilityFunctions.IsSameHashcode(hash));
            pararEstaEjecucion = !pararEstaEjecucion;


        }
        */
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (estaTransparente)
            {
                UtilityFunctions.MakeWindowRunInSight();
            }
            else
            {
                UtilityFunctions.MakeWindowRunHidden();
            }
            estaTransparente = !estaTransparente;
        }

        /*if (UtilityFunctions.IsFolderPresent("/Test") && !UtilityFunctions.IsFilePresent(Application.streamingAssetsPath + "/Test", "Hey.txt")){
            UtilityFunctions.CreateFile("/Test/Hey.txt", "Don't waste your time tampering. It won't work.");
        }
        */

        /*print(existeCarpeta);
        print(existeFichero);

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (estaTransparente)
            {
                UtilityFunctions.MakeWindowVisible(camara);
            }
            else
            {
                UtilityFunctions.MakeWindowTransparent(camara);
            }
            estaTransparente = !estaTransparente;
        }*/
    }
}
