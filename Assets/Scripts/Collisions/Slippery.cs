using Intefaces;
using UnityEngine;

public class Slippery : MonoBehaviour , ISlippery
{
    public void Slide(ref bool move , ref Rigidbody2D rb)
    {
        rb.constraints = RigidbodyConstraints2D.None;
        move = false;
    }
}
