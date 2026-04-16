using System;
using System.Collections;
using System.Collections.Generic;
using Intefaces;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossEnemy : MonoBehaviour, BossAttackable
{
        private bool isin = false;
        private bool open = false;
        private Animator anim;
        private bool doonceanim;
        private bool doonce = true;
        private bool attacking = false;
        private static readonly int AttackT = Animator.StringToHash("Attacking");
        private static readonly int AttackBite = Animator.StringToHash("AttackingBite");
        [SerializeField] private GameObject patrolpoints;
        [SerializeField] private float patrolSpeed;
        private List<Vector3> patrollpoints = new List<Vector3>();
        private Vector3 currentDestination;
        private int currentIndex;
        [SerializeField] private float damage;
        public bool rotated;
        private bool damaged = false;
        private SpriteRenderer sr;
        [SerializeField] private float totaltime;
        [SerializeField] private float interval;
        private float timer;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
            foreach (Transform child in patrolpoints.transform)
            {
                patrollpoints.Add(child.position);
            }
            StartCoroutine(PatrollAndWait());
        }

        void Update()
        {
            if (damaged)
            {
                timer += Time.deltaTime;
                if (timer >= totaltime)
                {
                    timer = 0;
                    damaged = false;
                }
            }
        }
        public IEnumerator BlinkForTime(SpriteRenderer sr, float totalTime, float blinkInterval)
        {
            float endTime = Time.time + totalTime;
            while (Time.time < endTime)
            {
                sr.enabled = !sr.enabled;
                yield return new WaitForSeconds(blinkInterval);
            }
            sr.enabled = true;
        }
        public void Attack(Rigidbody2D rb)
        {
            doonceanim = true;
            isin = true;
            if (doonceanim && !damaged)
            {
                
                if (rb.transform.position.y >= transform.position.y + 1f && !attacking)
                {
                    damaged = true;
                    StartCoroutine(BlinkForTime(sr, totaltime, interval));
                    attacking = false;
                }
                else if (rb.position.x - transform.position.x < 0 && !rotated || rotated && rb.position.x - transform.position.x > 0)
                {
                    attacking = true;
                    anim.SetTrigger(AttackT);
                }
                else if (rb.position.x - transform.position.x > 0 && !rotated ||  rotated && rb.position.x - transform.position.x < 0)
                {
                    attacking = true;
                    anim.SetTrigger(AttackBite);
                }
            }
        }
        public void AddForce(ref bool stop, ref Rigidbody2D rb, ref float currenthealth, ref float maxhealth)
        {
            if (open && isin && doonce)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.AddForce((  rb.transform.position - transform.position) * 10f, ForceMode2D.Impulse);
                stop = false;
                doonce = false;
                doonceanim = false;
                currenthealth += damage;
                EventManager.instance.PlayerDamage(currenthealth, maxhealth);
            }  
        }
        private IEnumerator PatrollAndWait()
        {
            while (true)
            {
                CalculateNewDestination();
                FaceToDestination();
                while (transform.position != currentDestination && !attacking)
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentDestination, patrolSpeed * Time.deltaTime);
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(Random.Range(0.5f, 1.75f));
                currentIndex = (currentIndex + 1) % patrollpoints.Count;
            }
        }
        private void FaceToDestination()
        {
            float x = currentDestination.x - transform.position.x;
            if (Math.Sign(x) == -1f)
            {
                rotated = true;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (Math.Sign(x) == 1f)
            {
                rotated = false;
                transform.eulerAngles = Vector3.zero;
            }
        }
        private void CalculateNewDestination()
        {
            rotated = false;
            currentDestination = patrollpoints[currentIndex];
        }
        void OpenAttackWindw()
        {
            open = true;
            doonce = true;
        }
        void CloseAttackWindow()
        {
            open = false;
            attacking = false;
        }
        public void IsOust()
        {
            isin = false;
        }
}
