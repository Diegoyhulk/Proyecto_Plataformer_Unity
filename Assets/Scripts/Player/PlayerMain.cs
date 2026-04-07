using System;
using System.Collections.Generic;
using Intefaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMain : MonoBehaviour
{
    [field: SerializeField] public Transform interactionpoint;
    [field: SerializeField] public float interactionradious;

    [field: SerializeField] public float movmentforce;
    [field: SerializeField] private float jumpinpulse;
    public Rigidbody2D rb {get; private set;}
    private float hinput;
    private bool damaged = false;
    float timer = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        hinput = Input.GetAxisRaw("Horizontal");
        if (!damaged){Jump();}

        if (damaged)
        {
           timer += Time.deltaTime;
           if (timer >= 3f)
           {
               damaged = false;
           }
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0,1) * jumpinpulse, ForceMode2D.Impulse);
        }
    }
    private void FixedUpdate()
    {
        if (!damaged)
        {
            rb.AddForce(new Vector2(hinput,0) * movmentforce, ForceMode2D.Force);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damaged(ref damaged, ref timer);
        }
    }
}
