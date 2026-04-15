using System;
using Intefaces;
using Managers;
using UnityEngine;

public class Mine : MonoBehaviour, IAttackable
{
    private Animator anim;
    private bool addforce = false;
    [SerializeField] private float force;
    [SerializeField] private float damage;
    private bool doonce = true;


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

    public void AddForce(ref bool stop, ref Rigidbody2D rb, ref float currenthealth, ref float maxhealth)
    {
        if (addforce)
        {
            rb.AddForce((  rb.transform.position - transform.position) * force, ForceMode2D.Impulse);
            if (doonce)
            {
                currenthealth += damage;
                EventManager.instance.PlayerDamage(currenthealth, maxhealth);
                doonce = false;
            }
        }
    }
    public void Attack()
    {
        anim.SetTrigger("Explode");
    }

    public void IsOust()
    {
    }
}
