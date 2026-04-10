using UnityEngine;

namespace Intefaces
{
    public interface IRecuperable
    {
        public void Recuperar(ref bool moving, ref Rigidbody2D rb);
    }
}