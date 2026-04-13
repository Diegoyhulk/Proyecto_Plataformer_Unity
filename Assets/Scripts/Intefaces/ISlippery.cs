using UnityEngine;

namespace Intefaces
{
    public interface ISlippery
    {
        public void Slide(ref bool move, ref Rigidbody2D rb, ref Animator anim);
    }
}