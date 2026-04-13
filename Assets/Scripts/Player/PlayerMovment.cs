using System;
using System.Collections.Generic;
using System.Diagnostics;
using Intefaces;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;

public class PlayerMain : PlayerMain1
{
    [Header("Player Movment")]
    [field: SerializeField] public float movmentforce;
    public float jumpangle = 0;
    private float maxjumpangle = 50f;
    private float hinput;
    private static Vector2 Swiming;
    private bool Stop = true;
    private Action OnApressed;
    [SerializeField] public InputReaderSO inputreader;
    public bool WMode { get; set; }
    private Animator anim;

    
    [Header("RB")]
    
    private RigidbodyConstraints2D constraints;
    float timer = 0;
    
    [Header("Particle")]
    [SerializeField] private ParticleSystem ps;
    private bool startpin;
    private bool startparticles = true;
    private float spinamount => jumpangle / maxjumpangle;
    [SerializeField] private ParticleSystem[] bb;


    private void OnEnable()
    {
        inputreader.Moving += UpdateMovment;
        inputreader.WMoving += Swim;
        inputreader.OnWMoving += SwimPlay;
        inputreader.OffWMoving += SwimStop;
    }

    private void OnDisable()
    {
        inputreader.Moving -= UpdateMovment;
        inputreader.WMoving -= Swim;
        inputreader.OnWMoving -= SwimPlay;
        inputreader.OffWMoving -= SwimStop;
    }
    
    public virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Stop && !WMode)
        {
            Create_Spin();  
        }
    }
    private void FixedUpdate()
    {
        if (Stop && !startpin && !WMode) 
        {
            Move();
        }
        if (WMode)
        {
            rb.AddForce(Swiming * 50f, ForceMode2D.Force);
        }
    }

    private void UpdateMovment(Vector2 ctx)
    {
        if (!WMode)
        {
            hinput = ctx.x;
        }
    }

    private void Swim(Vector2 ctx)
    {
        if (WMode)
        {
            Swiming = ctx;
            WRotate();
        }
    }

    private void SwimPlay()
    {
        if (WMode)
        {
            anim.SetBool("Spinning", true);
            foreach (var bub in bb)
            {
                bub.Play();
            }
        }
    }

    private void SwimStop()
    {
        if (WMode)
        {
            anim.SetBool("Spinning", false);
            foreach (var bub in bb)
            {
                bub.Stop();
            }
        }
    }
    private void WRotate()
    {
        Vector2 dir = Swiming;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void Create_Spin()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (startparticles)
            {
                ps.Play(); 
                anim.SetBool("Spinning", true);
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
            slippery.Slide(ref Stop, ref rb, ref anim);
        }
        if (other.gameObject.TryGetComponent(out IEnterExit enter))
        {
            enter.Onenter();
        }
        if (other.gameObject.TryGetComponent(out IAttackable attackable))
        {
            attackable.Attack();
        }
        if (other.gameObject.CompareTag("Water"))
        {
            rb.constraints = RigidbodyConstraints2D.None;
            WMode = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!Stop)
        {
            if (other.gameObject.TryGetComponent(out IRecuperable rec))
            {
                rec.Recuperar(ref Stop, ref rb, ref anim);
            }
        }
        if (other.gameObject.TryGetComponent(out ISinkable sinkable))
        {
            sinkable.Recuperar(ref Stop, ref rb, ref  anim);
            if (Input.GetKey(KeyCode.W))
                sinkable.PartMove(rb);
        }
        if (other.gameObject.TryGetComponent(out IAttackable attackable))
        {
            attackable.AddForce(ref Stop, ref rb);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent(out ISinkable sinkable))
        {
            sinkable.OnExit();
        }

        if (other.gameObject.TryGetComponent(out IEnterExit Exit))
        {
            Exit.Onexit();
        }
        if(other.gameObject.TryGetComponent(out IAttackable attackable))
        {
            attackable.IsOust();
        }
        if (other.gameObject.CompareTag("Water"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            foreach (var bub in bb)
            {
                bub.Stop();
            }
            rb.rotation = 0;
            WMode = false;
        }
    }
}
