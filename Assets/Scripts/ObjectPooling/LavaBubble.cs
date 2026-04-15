using System;
using Intefaces;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPooling
{
    public class LavaBubble: MonoBehaviour, IAttackable
    {
        public ObjectPool<LavaBubble> MyPool { get; set; }
        private Spawner spawner;
        private float timer = 0;
        private Rigidbody2D rb;
        private bool despawneable;
        private bool doonce;
        [SerializeField] private float damage;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                despawneable = true;
                timer = 0;
            }
            if (rb.linearVelocity.y >= 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (rb.linearVelocity.y < 0)
            {
                transform.eulerAngles = new Vector3(0, 0, -180);
            }
        }

        private void OnEnable()
        {
            despawneable = false;
            rb.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);
        }

        public void SetSpawner(Spawner s)
        {
            spawner = s;
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Spawner") && despawneable)
            {
                spawner.BubleRelease();
                MyPool.Release(this);
            }
        }
        public void Attack()
        {
            doonce = true;
        }
        public void AddForce(ref bool stop, ref Rigidbody2D rb, ref float currenthealth, ref float maxhealth)
        {
            if (doonce)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.AddForce(( rb.transform.position - transform.position) * 10f, ForceMode2D.Impulse);
                stop = false;
                currenthealth += damage;
                EventManager.instance.PlayerDamage(currenthealth, maxhealth);
                doonce = false;
            }
        }

        public void IsOust()
        {}
    }
}