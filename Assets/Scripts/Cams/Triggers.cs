using System;
using Intefaces;
using UnityEngine;

public class Triggers : MonoBehaviour, IEnterExit
{
    public event Action<int> OnEnter;
    public event Action<int> Exit;
    [SerializeField] private int id;
    public void Onenter()
    {
        OnEnter?.Invoke(id);
    }

    public void Onexit()
    {
        Exit?.Invoke(id);
    }
}
