using System;
using UnityEngine;

public abstract class PlayerMain1 : MonoBehaviour
{
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }
}
