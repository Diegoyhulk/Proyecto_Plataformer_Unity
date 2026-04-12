using System;
using Intefaces;
using UnityEngine;

public class World1Enemies : MonoBehaviour, IEnterExit
{
    private GameObject Enemies;

    private void Awake()
    {
        Enemies = GameObject.Find("Enemies");
        Enemies.SetActive(false);
    }
    public void Onenter()
    {
        Enemies.SetActive(true);
    }

    public void Onexit()
    {
        Enemies.SetActive(false);
    }
}
