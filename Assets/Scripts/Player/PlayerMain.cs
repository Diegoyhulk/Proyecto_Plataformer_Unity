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
    private bool moving = false;

    [Header("RB")]
    public Rigidbody2D rb;
    private RigidbodyConstraints2D constraints;
    private bool start = false;
    float timer = 0;
    
    [Header("Particle")]
    private ParticleSystem ps;
	private float spinamount => jumpangle / maxjumpangle;
    
    
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        
    }

    private void Update()
    {
        hinput = Input.GetAxisRaw("Horizontal");
        if (!moving)
        {
            Create_Spin();
        }
    }

    private void Create_Spin()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ps.Play();
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (jumpangle <= maxjumpangle)
            {
                jumpangle += (Time.deltaTime * 20);
                float t = spinamount;
                float lifetime = Mathf.Lerp(1, 2, t);
                var main = ps.main;
                main.startLifetime = lifetime;
            }
            Rotate();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            ps.Stop();
            rb.AddForce(new Vector2((spinamount) * hinput, 1f) * 20, ForceMode2D.Impulse);
            rb.AddTorque(-100f * hinput * (spinamount) , ForceMode2D.Force);
            jumpangle = 0;
            rb.constraints = RigidbodyConstraints2D.None;
            moving = true;
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
        if (!moving)
        {
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W)||
                Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
            {
                Move();
            }
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
            slippery.Slide(ref moving, ref rb);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (moving)
        {
            if (other.gameObject.TryGetComponent(out IRecuperable rec))
            {
                rec.Recuperar(ref moving, ref rb);
            }
        }

        if (other.gameObject.TryGetComponent(out ISinkable sinkable))
        {
            sinkable.Recuperar(ref moving, ref rb);
            if(Input.GetKey(KeyCode.W))
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
