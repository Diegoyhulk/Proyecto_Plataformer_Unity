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
    private bool completo2;

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
            float progreso1 = Mathf.InverseLerp(xInicio1, xFin1, player.transform.position.x);
            if (progreso1 > 0 && Fondos[1].color.a < 1f)
            {
                Color c = Fondos[1].color;
                c.a = progreso1;
                Fondos[1].color = c;
            }
            if (player.transform.position.x >= xFin1)
            {
                completo1 = true;
            }
        }
        if (completo1 && !completo2)
        {
            float progreso2 = Mathf.InverseLerp(xInicio2, xFin2, player.transform.position.x);
            if (progreso2 > 0 && Fondos[2].color.a < 1f)
            {
                Color c = Fondos[2].color;
                c.a = progreso2;
                Fondos[2].color = c;
            }
            if (player.transform.position.x >= xFin2)
            {
                completo2 = true;
            }
        }
    }
}
