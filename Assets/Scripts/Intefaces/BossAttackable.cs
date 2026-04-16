using UnityEngine;

namespace Intefaces
{
    public interface BossAttackable
    {
        public void Attack(Rigidbody2D rb);
        public void AddForce(ref bool stop, ref Rigidbody2D rb, ref float currenthealth, ref float maxhealth);
        public void IsOust();
    }
}