using System;
using UnityEngine;

public class PlayerMain1 : MonoBehaviour
{
    [Header("RigidBody")]
    public Rigidbody2D rb;
    public bool Stop = true;
    public bool WMode = false;
    public float currentdamage;
    public float maxhealth;
    public Vector3 Checkpointtransform = new Vector3(0, 0, 0);
    
    [Header("Animation")]
    public Animator anim;
    [SerializeField] public ParticleSystem[] bb;
    
    [Header("InputReader")]
    [SerializeField] public InputReaderSO inputreader;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        anim = GetComponent<Animator>();
        Checkpointtransform = rb.position;
    }
    
}
