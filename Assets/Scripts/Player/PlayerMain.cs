using System;
using System.Collections.Generic;
using Intefaces;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMain : MonoBehaviour
{
    [Header("Player Movment")]
    [field: SerializeField] public float movmentforce;
    public float jumpangle = 0;
    private float maxjumpangle = 50f;
    private float hinput;
    private bool Stop = false;
    private Action OnApressed;
    [SerializeField] private InputReaderSO inputreader;
    
    
    [Header("RB")]
    public Rigidbody2D rb;
    private RigidbodyConstraints2D constraints;
    float timer = 0;
    
    [Header("Particle")]
    private ParticleSystem ps;
    private bool startpin;
    private bool startparticles = true;
    private float spinamount => jumpangle / maxjumpangle;


    private void OnEnable()
    {
        inputreader.Moving += UpdateMovment;
    }

    private void OnDisable()
    {
        inputreader.Moving -= UpdateMovment;
    }
    
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        
    }

    private void Update()
    {
        if (Stop)
        {
            Create_Spin();  
        }
    }

    private void UpdateMovment(Vector2 ctx)
    {
        hinput = ctx.x;
    }

    private void Create_Spin()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (startparticles)
            {
                ps.Play(); 
            }
            startparticles = false;
            startpin = true;
            if (jumpangle <= maxjumpangle)
            {
                jumpangle += (Time.deltaTime * 20);
                float t = spinamount;
                float lifetime = Mathf.Lerp(1, 2, t);
                var main = ps.main;
                main.startLifetime = lifetime;
                Rotate(); 
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            ps.Stop();
            rb.AddForce(new Vector2((spinamount) * hinput, 1f) * 20, ForceMode2D.Impulse);
            rb.AddTorque(-100f * hinput * (spinamount), ForceMode2D.Force);
            jumpangle = 0;
            rb.constraints = RigidbodyConstraints2D.None;
            Stop = false;
            startpin = false;
            startparticles = true;
        }
    }
    private void Rotate()
    {
        if (hinput > 0)//Dch
        {
            ps.transform.eulerAngles = new Vector3(0, -90, 87.615f);
        }
        else if (hinput < 0) //Izq
        {
            ps.transform.eulerAngles = new Vector3(0, 90, 87.615f);
        }
    }

    private void FixedUpdate()
    {
        if (Stop && !startpin)
        {
            Move();
        }
    }

    private void Move()
    {
        rb.AddForce(new Vector2(hinput, 0) * movmentforce, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ISinkable sinkable))
        {
            sinkable.OnEnter();
        }
        if (other.gameObject.TryGetComponent(out ISlippery slippery))
        {
            slippery.Slide(ref Stop, ref rb);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!Stop)
        {
            if (other.gameObject.TryGetComponent(out IRecuperable rec))
            {
                rec.Recuperar(ref Stop, ref rb);
            }
        }
        if (other.gameObject.TryGetComponent(out ISinkable sinkable))
        {
            sinkable.Recuperar(ref Stop, ref rb);
            if (Input.GetKey(KeyCode.W))
                sinkable.PartMove(rb);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent(out ISinkable sinkable))
        {
            sinkable.OnExit();
        }
    }
}
