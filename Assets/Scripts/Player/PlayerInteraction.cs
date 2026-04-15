using Intefaces;
using UnityEngine;

public class PlayerInteraction : PlayerSystem
{
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ISinkable sinkable))
        {
            sinkable.OnEnter(main.rb);
        }
        if (other.gameObject.TryGetComponent(out ISlippery slippery))
        {
            slippery.Slide(ref main.Stop, ref main.rb, ref main.anim);
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
            main.rb.constraints = RigidbodyConstraints2D.None;
            main.WMode = true;
        }
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            Debug.Log("Checkpoint");
            main.Checkpointtransform = main.rb.position;
            main.rb.linearVelocity = Vector3.zero;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!main.Stop)
        {
            if (other.gameObject.TryGetComponent(out IRecuperable rec))
            {
                rec.Recuperar(ref main.Stop, ref main.rb, ref main.anim);
            }
        }
        if (other.gameObject.TryGetComponent(out ISinkable sinkable))
        {
            sinkable.Recuperar(ref main.Stop, ref main.rb, ref  main.anim);
            if (Input.GetKey(KeyCode.W))
                sinkable.PartMove(main.rb);
        }
        if (other.gameObject.TryGetComponent(out IAttackable attackable))
        {
            attackable.AddForce(ref main.Stop, ref main.rb, ref main.currentdamage, ref main.maxhealth);
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
            main.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            foreach (var bub in main.bb)
            {
                bub.Stop();
            }
            main.rb.rotation = 0;
            main.WMode = false;
        }
    }
}
