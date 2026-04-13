using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class FondoScript : MonoBehaviour
{
    private List<SpriteRenderer> Fondos = new List<SpriteRenderer>();
    private float timer;
    [SerializeField] GameObject player;
    [SerializeField] float xInicio1;
    [SerializeField] float xFin1;
    private bool completo1 = false;
    [SerializeField] float xInicio2;
    [SerializeField] float xFin2;
    private bool completo2 = false;
    [SerializeField] float YCuevaIni;
    [SerializeField] float YCuevaFin;
    [SerializeField] float YCuevaIni2;
    [SerializeField] float YCuevaFin2;
    private bool completo3;

    private void Awake()
    {
        foreach (SpriteRenderer fondo in GetComponentsInChildren<SpriteRenderer>())
        {
            Fondos.Add(fondo);
        }
    }
    private void Update()
    {
        if (!completo1)
        {
            ChangeWorld();
        }
        if (completo1 && !completo2)
        {
            float progreso = Mathf.InverseLerp(xInicio2, xFin2, player.transform.position.x);
            if (progreso > 0 && Fondos[2].color.a < 1f)
            {
                Color c = Fondos[2].color;
                c.a = progreso;
                Fondos[2].color = c;
            }
            if (player.transform.position.x >= xFin2)
            {
                completo2 = true;
            }
        }
        if (completo2 && !(player.transform.position.y >= YCuevaFin))
        {
            float progreso = Mathf.InverseLerp(YCuevaIni, YCuevaFin, player.transform.position.y);
            if (progreso > 0 && Fondos[3].color.a < 1f)
            {
                Color c = Fondos[3].color;
                c.a = progreso;
                Fondos[3].color = c;
            }
            completo3 = true;
        }

        if (!(player.transform.position.y <= YCuevaFin2)/* && completo3*/)
        {
            float progreso = Mathf.InverseLerp(YCuevaIni2, YCuevaFin2, player.transform.position.y);
            if (progreso > 0 && Fondos[4].color.a < 1f)
            {
                Color c = Fondos[4].color;
                c.a = progreso;
                Fondos[4].color = c;
            }
        }
    }

    private void ChangeWorld()
    {
        float progreso = Mathf.InverseLerp(xInicio1, xFin1, player.transform.position.x);
        if (progreso > 0 && Fondos[1].color.a < 1f)
        {
            Color c = Fondos[1].color;
            c.a = progreso;
            Fondos[1].color = c;
        }
        if (player.transform.position.x >= xFin1)
        {
            completo1 = true;
        }
    }
}
