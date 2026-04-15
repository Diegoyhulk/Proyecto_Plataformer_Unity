using UnityEngine;

namespace Intefaces
{
    public interface ISinkable
    {
        public void OnEnter(Rigidbody2D rb);
        public void OnExit();
        public void Recuperar(ref bool moving, ref Rigidbody2D rb, ref Animator anim, ref float damage, float maxhealth);
        public void PartMove(Rigidbody2D rb);
    }
}