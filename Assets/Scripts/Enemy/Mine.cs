using System;
using Intefaces;
using UnityEngine;

public class Mine : MonoBehaviour, IAttackable
{
    private Animator anim;
    private bool addforce = false;
    [SerializeField] private float force;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Explode()
    {
        addforce = true;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    public void AddForce(ref bool stop, ref Rigidbody2D rb)
    {
        if (addforce)
        {
            rb.AddForce((  rb.transform.position - transform.position) * force, ForceMode2D.Impulse);
        }
    }
    public void Attack()
    {
        anim.SetTrigger("Explode");
    }
    public void IsOust(){}
}
