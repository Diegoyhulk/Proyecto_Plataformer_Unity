using UnityEngine;

namespace Intefaces
{
    public interface IAttackable
    {
        public void Attack();
        public void AddForce(ref bool stop, ref Rigidbody2D rb, ref float currenthealth, ref float maxhealth);
        public void IsOust();
    }
}