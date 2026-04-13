using Intefaces;
using UnityEngine;

public class Slippery : MonoBehaviour , ISlippery
{
    public void Slide(ref bool move , ref Rigidbody2D rb, ref Animator anim)
    {
        rb.constraints = RigidbodyConstraints2D.None;
        anim.SetBool("Spinning",false);
        move = false;
    }
}
