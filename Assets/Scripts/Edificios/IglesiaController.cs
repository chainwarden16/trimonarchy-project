using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IglesiaController : MonoBehaviour
{
    float temporizadorCuracion = 3f;
    GameManager manager;
    List<Unidad> unidadesAliadas = new List<Unidad>();
    Edificio edificio;
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        edificio = GetComponent<Edificio>();

    }

    void Update()
    {
        //Se busca todas las unidades que sean soldados (no improta si son guerreros o magos) y se les restaura un 10% de su vida máxima cada 3 segundos
        if (temporizadorCuracion <= 0f && manager.GetSeHanCreadoEnemigos() && edificio.haFinalizadoConstruccion)
        {
            unidadesAliadas.Clear();
            unidadesAliadas = FindObjectsOfType<Unidad>().Where(uni => uni.unidad.tipo != UnidadScriptable.TipoUnidad.Civil).ToList();
            foreach(Unidad un in unidadesAliadas)
            {

                int vidaMaximaUnidad = un.unidad.vida / 10;
                if(un.vidaActual + vidaMaximaUnidad > un.unidad.vida)
                {
                    un.vidaActual = un.unidad.vida;
                }
                else
                {
                    un.vidaActual += vidaMaximaUnidad;
                }

            }

            temporizadorCuracion = 3f;

        }
        else
        {
            temporizadorCuracion -= Time.deltaTime;
        }
    }
}
