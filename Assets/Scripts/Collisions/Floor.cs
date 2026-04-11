using Intefaces;
using UnityEngine;

namespace Collisions
{
    public class Floor : MonoBehaviour, IRecuperable
    {
        private float timer = 0f;
        public void Recuperar(ref  bool moving, ref Rigidbody2D rb)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                    moving = true;
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    rb.rotation = 0;
                    timer = 0;
            }
        }
    }
}