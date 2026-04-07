using System;
using System.Collections.Generic;
using Intefaces;
using Unity.VisualScripting;
using UnityEngine;

public class DmgSpike : MonoBehaviour, IDamageable
{
    private bool damaged = false;
    public void Damaged(ref bool bol, ref float timer)
    {
        Debug.Log("Damaged");
        bol = true;
        timer = 0;
    }
}
