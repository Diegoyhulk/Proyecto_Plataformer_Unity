using UnityEngine;

namespace Intefaces
{
    public interface ISinkable
    {
        public void OnEnter();
        public void OnExit();
        public void Recuperar(ref bool moving, ref Rigidbody2D rb);
        public void PartMove(Rigidbody2D rb);
    }
}