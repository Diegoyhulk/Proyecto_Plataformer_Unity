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
    [SerializeField] float xInicio;
    [SerializeField] float xFin;
    private void Awake()
    {
        foreach (SpriteRenderer fondo in GetComponentsInChildren<SpriteRenderer>())
        {
            Fondos.Add(fondo);
        }
    }
    private void Update()
    {
        float progreso = Mathf.InverseLerp(xInicio, xFin, player.transform.position.x);
        if (progreso > 0 && Fondos[1].color.a < 1f)
        {
            Color c = Fondos[1].color;
            c.a = progreso;
            Fondos[1].color = c;
        }
        
    }
}
