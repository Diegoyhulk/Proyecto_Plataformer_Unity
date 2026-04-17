using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class FondoScript : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip[] clips;
    private List<SpriteRenderer> Fondos = new List<SpriteRenderer>();
    [SerializeField] private List<Triggers> triggers = new List<Triggers>();
    private bool[] starts = new bool[4];
    private float timer;
    [SerializeField] GameObject player;
    [SerializeField] Transform xInicio1;
    [SerializeField] Transform xFin1;
    private bool completo1 = false;
    [SerializeField] Transform xInicio2;
    [SerializeField] Transform xFin2;
    private bool completo2 = false;
    [SerializeField] Transform YCuevaIni;
    [SerializeField] Transform YCuevaFin;
    [SerializeField] Transform YCuevaIni2;
    [SerializeField] Transform YCuevaFin2;
    private bool completo3;
    
    private void Awake()
    {
        foreach (SpriteRenderer fondo in GetComponentsInChildren<SpriteRenderer>())
        {
            Fondos.Add(fondo);
        }
    }
    void OnEnable()
    {
        foreach (var t in triggers)
        {
            t.OnEnter += HandleTriggerEnter;
            t.Exit += HandleTriggerExit;
        }
    }
    void OnDisable()
    {
        foreach (var t in triggers)
        {
            t.OnEnter -= HandleTriggerEnter;
            t.Exit -= HandleTriggerExit;
        }
    }
    private void HandleTriggerEnter(int id)
    {
        starts[id] = true;
    }
    private void HandleTriggerExit(int id)
    {
        starts[id] = false;
    }
    private void Update()
    {
        if (starts[0])
        {
            float progreso = Mathf.InverseLerp(xInicio1.transform.position.x, xFin1.transform.position.x, player.transform.position.x);
            if (progreso > 0 && Fondos[1].color.a < 1f)
            {
                Color c = Fondos[1].color;
                c.a = progreso;
                Fondos[1].color = c;
            }
        }
        else if (starts[1])
        {
            float progreso = Mathf.InverseLerp(xInicio2.transform.position.x, xFin2.transform.position.x, player.transform.position.x);
            if (progreso > 0 && Fondos[2].color.a < 1f)
            {
                Color c = Fondos[2].color;
                c.a = progreso;
                Fondos[2].color = c;
            }
        }
        else if (starts[2])
        {
            float progreso = Mathf.InverseLerp(YCuevaIni.transform.position.y, YCuevaFin.transform.position.y, player.transform.position.y);
            if (progreso > 0 && Fondos[3].color.a < 1f)
            {
                Color c = Fondos[3].color;
                c.a = progreso;
                Fondos[3].color = c;
                if(Fondos[3].color.a >= 1f)
                    audioManager.ChangeMusic(clips[0]);
            }
        }
        else if (starts[3])
        {
            float progreso = Mathf.InverseLerp(YCuevaIni2.transform.position.y, YCuevaFin2.transform.position.y, player.transform.position.y);
            if (progreso > 0 && Fondos[4].color.a < 1f)
            {
                Color c = Fondos[4].color;
                c.a = progreso;
                Fondos[4].color = c;
                if(Fondos[4].color.a >= 1f)
                    audioManager.ChangeMusic(clips[1]);
            }
        }
    }
}
