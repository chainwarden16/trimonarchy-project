using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System;
using UnityEditor;
using UnityEngine;



/// <summary>
/// Esto son funciones auxiliares que llevarán a cabo parte del trabajo de la segunda parte del videojuego ("Metajuego"), con búsqueda & creación de archivos, comprobaciones de que los archivos proporcionados sean los esperados y
/// haciendo que la ventana se vuelva invisible/visible. No olvidar añadir excepciones para que el juego haga algo - o más bien, hacer que el juego vaya por otro camino si faltara el archivo o, en general, algo no saliera bien
/// </summary>
public class UtilityFunctions : MonoBehaviour
{

    private static readonly Dictionary<string, string> shaFicherosARecibir = new Dictionary<string, string> { { "mind.txt", "1822315416359141710902123516621918144421211701640216101182036881391492421222" }, { "Hey", "Hello" } };

    private const string directorioLocal = "d:/Users/Usuario/Desktop/Cursos CICE/Unity/Editor Unity/Trimonarchy/Files";
    private const string directorioProyectoLocal = "d:/Users/Usuario/Desktop/Cursos CICE/Unity/Editor Unity/Trimonarchy";

    private struct MARGINS
    {
        public int bordeIzquierdo;
        public int bordederecho;
        public int bordeSuperior;
        public int bordeInferior;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr tamanoVentana, ref MARGINS margenes);


    #region Operaciones de manipulacion de ventana


    ///<summary>
    ///
    /// Hace que la ventana principal se vuelva transparente. Sólo funciona en Windows, así que hay que verificar que se esté usando ese sistema operativo
    /// 
    /// </summary>
    public static void MakeWindowTransparent(Camera camara)
    {
        IntPtr tamano = GetActiveWindow();

        MARGINS margenes = new MARGINS { bordeIzquierdo = -1 };
        DwmExtendFrameIntoClientArea(tamano, ref margenes);
        camara.backgroundColor = new Color32(0, 0, 0, 0); //se pone en negro para que no se vea el fondo
        camara.clearFlags = CameraClearFlags.SolidColor;

    }

    ///<summary>
    ///
    /// Hace que la ventana principal se vuelva opaca. Sólo funciona en Windows, así que hay que verificar que se esté usando ese sistema operativo
    /// 
    /// </summary>
    public static void MakeWindowVisible(Camera camara)
    {
        IntPtr tamano = GetActiveWindow();

        MARGINS margenes = new MARGINS { bordeIzquierdo = -1 };
        DwmExtendFrameIntoClientArea(tamano, ref margenes);
        camara.backgroundColor = new Color32(255, 255, 255, 255); //se pone en blanco para que se vea el fondo
        camara.clearFlags = CameraClearFlags.Skybox;
    }

    ///<summary>
    ///
    /// Hace que la ventana principal se esconda y no muestre su icono en la barra de herramientas. Sólo funciona en Windows, así que hay que verificar que se esté usando ese sistema operativo
    /// 
    /// </summary>
    public static void MakeWindowRunHidden()
    {

    }

    ///<summary>
    ///
    /// Hace que la ventana principal se vuelva visible en la barra de herramientas. Sólo funciona en Windows, así que hay que verificar que se esté usando ese sistema operativo
    /// 
    /// </summary>
    public static void MakeWindowRunInSight()
    {



    }

    ///<summary>
    ///
    /// Crea un archivo en el directorio indicado con el nombre dado. O más bien, copia uno que ya está guardado dentro de los Assets de Unity y lo esconde en el directorio que se le porporcione. Si ya existe, no se vuelve a crear
    /// 
    /// </summary>


    #endregion

    #region Crear Archivos o ficheros y eliminarlos


    public static void CreateFile(string folderName, string name, string message)
    {
        if (EditorApplication.isPlaying)
        {
            if (IsFolderPresent(folderName))
            {
                if (!File.Exists(Path.Combine(directorioLocal, name)))
                    File.WriteAllText(Path.Combine(directorioLocal, name), message);
            }
            else
            {
                RecreateSpecialFolder();
                File.WriteAllText(Path.Combine(directorioLocal, name), message);
            }

        }
        else
        {
            if (IsFolderPresent(folderName))
            {
                if (!File.Exists(Path.Combine(Path.Combine(Application.streamingAssetsPath, folderName), name)))
                    File.WriteAllText(Path.Combine(Path.Combine(Application.streamingAssetsPath, folderName), name), message);
            }
            else
            {
                RecreateSpecialFolder();
                File.WriteAllText(Path.Combine(Path.Combine(Application.streamingAssetsPath, folderName), name), message);
            }

        }
    }


    ///<summary>
    ///
    /// Crea una carpeta en el directorio indicado con el nombre pasado como parámetro.
    /// 
    /// </summary>

    public static void CreateFolder(string name)
    {

        if (EditorApplication.isPlaying)
        {
            if (!Directory.Exists(directorioLocal))
                Directory.CreateDirectory(directorioLocal);

        }
        else
        {
            if (!Directory.Exists(Path.Combine(Application.streamingAssetsPath, name)))
                Directory.CreateDirectory(Path.Combine(Application.streamingAssetsPath, name));
        }
    }

    ///<summary>
    ///
    /// Hace desaparecer un archivo de un directorio siempre que tenga el nombre pasado. Hay que comprobar que existe, que para eso se tiene IsFilePresent
    /// 
    /// </summary>
    public static void DeleteFile(string fileName, string folderName)
    {
        if (IsFilePresent(fileName, folderName))
            if (EditorApplication.isPlaying)
            {
                File.Delete(Path.Combine(directorioLocal, fileName));
            }
            else
            {
                File.Delete(Path.Combine(Path.Combine(Application.streamingAssetsPath, folderName), fileName));
            }

    }


    public static void RecreateSpecialFolder()
    {
        if (EditorApplication.isPlaying)
        {

            CreateFolder(directorioLocal);


        }
        else
        {

            CreateFolder("");
            CreateFolder("Files");
        }


        CreateFile("/Files", "Dont.txt", "Please refrain yourself from deleting this folder. It's my only way to communicate with you. Besides, you might do something wrong and leave this whole thing broken beyond repair.");
    }


    #endregion

    #region Hashcode

    ///<summary>
    ///
    /// Comprueba si el archivo en el directorio indicado con el nombre pasado como parámetro coincide con el archivo que se debía proporcionar. Esto es para evitar que el usuario cree un archivo con el nombre que se espera y se acepte como bueno sin más. Se usará SHA256 para comprobar
    /// si el archivo es el correcto, pues sigue siendo un SHA seguro. Los SHA de los archivos se tendrán precalculados dentro de un diccionario para ahorrar tiempo.
    /// </summary>
    public static string CalculateHashcodeSha(string name, string folder)
    {
        byte[] hashcode;

        DirectoryInfo direccion;
        string hashcodeLegible = "";

        if (IsFilePresent(name, folder))
        {
            if (EditorApplication.isPlaying)
            {
                direccion = new DirectoryInfo(Path.Combine(directorioLocal));
            }
            else
            {
                direccion = new DirectoryInfo(Path.Combine(Path.Combine(Application.streamingAssetsPath, folder)));
            }

            // Get the FileInfo objects for every file in the directory.
            FileInfo[] files = direccion.GetFiles();

            SHA256 mySHA256 = SHA256.Create();



            foreach (var file in files)
            {

                try
                {
                    if (file.Name == name)
                    {

                        // Create a fileStream for the file.
                        FileStream fileStream = file.Open(FileMode.Open);
                        // Be sure it's positioned to the beginning of the stream.
                        fileStream.Position = 0;
                        // Compute the hash of the fileStream.
                        hashcode = mySHA256.ComputeHash(fileStream);
                        // Write the name and hash value of the file to the console.
                        Debug.Log($"{file.Name}: ");

                        for (int i = 0; i < hashcode.Length; i++)
                        {
                            hashcodeLegible += hashcode[i];
                        }
                        Debug.Log(hashcodeLegible);
                        // Close the file.
                        fileStream.Close();

                        break;
                    }


                }
                catch (IOException e)
                {
                    Console.WriteLine("No se ha encontrado el archivo o se ha producido un error mientras se procesaba.");
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine("No se tiene permiso para acceder a este archivo.");
                }
            }

        }

        return hashcodeLegible;

    }

    public static bool IsSameHashcode(string hashcodeItem)
    {
        bool sameHashcode = false;

        if (shaFicherosARecibir.ContainsValue(hashcodeItem))
        {
            sameHashcode = true;
        }

        return sameHashcode;


    }


    #endregion


    #region Busqueda de archivos y carpetas

    ///<summary>
    ///
    /// Comprueba si existe un archivo en el directorio indicado con el nombre pasado como parámetro. O más bien, copia uno que ya está guardado dentro de los Assets del proyecto y lo esconde en el directorio que se le porporcione
    /// 
    /// </summary>
    public static bool IsFilePresent(string fileName, string folderName)
    {
        bool estaPresente = false;

        if (EditorApplication.isPlaying)
        {
            if (File.Exists(Path.Combine(directorioLocal, fileName)))
            {
                estaPresente = true;
            }
        }
        else
        {
            if (File.Exists(Path.Combine(Path.Combine(Application.streamingAssetsPath, folderName), fileName)))
            {
                estaPresente = true;
            }
        }


        /*
         * DirectoryInfo info;
         * 
        if (IsFolderPresent(folderName))
        {
            if (EditorApplication.isPlaying)
            {
                info = new DirectoryInfo(directorioLocal);
            }
            else
            {
                
                info = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath, folderName));
            }

        }
        else
        {
            RecreateSpecialFolder();
            if (EditorApplication.isPlaying)
            {
                info = new DirectoryInfo(directorioLocal);
            }
            else
            {

                info = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath, folderName));
            }
        }

        if (info != null)
        {
            var fileInfo = info.GetFiles();
            foreach (var file in fileInfo)
            {
                if (file.Name == fileName) //falta colocar el check con el hashcode y la creacion de un archivo en caso de que se intente hacer trampa
                {
                    estaPresente = true;
                    break;
                }
            }
        }
        else
        {

            CreateFile("/Files", "Stop.txt", "Keep yourself from deleting this folder or any other created automatically. You might affect something and leave this whole thing broken beyond repair, forcing you to restart all over again.");
        }
        */

        return estaPresente;
    }

    ///<summary>
    ///
    /// Comprueba si existe una carpeta en el directorio indicado con el nombre pasado como parámetro. Si no, la crea
    /// 
    /// </summary>
    public static bool IsFolderPresent(string name)
    {
        bool estaPresente = false;

        if (EditorApplication.isPlaying)
        {
            if (Directory.Exists(directorioLocal))
                estaPresente = true;

        }
        else
        {
            if (Directory.Exists(Path.Combine(Application.streamingAssetsPath, name)))
                estaPresente = true;
        }



        /*try
        {
            if (EditorApplication.isPlaying)
            {

                carpetas = Directory.GetDirectories(directorioLocal);
            }
            else
            {
                carpetas = Directory.GetDirectories(Application.streamingAssetsPath);
            }

        }

        catch
        {
            CreateFolder(name);
            CreateFile("/Files", "StopNow.txt", "Keep yourself from deleting any item created automatically. You might endanger your progress by softlocking yourself.");
        }
        finally
        {

            if (EditorApplication.isPlaying)
            {

                carpetas = Directory.GetDirectories(directorioProyectoLocal);
            }
            else
            {
                carpetas = Directory.GetDirectories(Application.streamingAssetsPath);
            }

        }

        if (carpetas.Length != 0 && carpetas != null)
        {

            foreach (var carpeta in carpetas)
            {
                if (carpeta == name) //si el nombre de la carpeta concide, es que existe
                {
                    estaPresente = true;
                    break;
                }
            }

            if (!estaPresente)
            {
                RecreateSpecialFolder();
            }
        }
        else
        {
            RecreateSpecialFolder();
        }*/


        return estaPresente;
    }


    #endregion

}
