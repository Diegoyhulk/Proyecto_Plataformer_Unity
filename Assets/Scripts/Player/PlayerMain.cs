using System;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [field: SerializeField] public Transform interactionpoint;
    [field: SerializeField] public float interactionradious;

    [field: SerializeField] public float movmentforce;
    [field: SerializeField] private float jumpinpulse;
    public Rigidbody2D rb {get; private set;}
    private float hinput;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        hinput = Input.GetAxisRaw("Horizontal");
        Jump();
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
        rb.AddForce(new Vector2(hinput,0) * movmentforce, ForceMode2D.Force);
    }
}
