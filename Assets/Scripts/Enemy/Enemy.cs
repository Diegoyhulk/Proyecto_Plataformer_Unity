using System;
using System.Collections;
using System.Collections.Generic;
using Intefaces;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class Enemy : MonoBehaviour, IAttackable
    {
        private bool isin = false;
        private bool open = false;
        private Animator anim;
        private bool doonceanim;
        private bool doonce = true;
        private bool attacking = false;
        private static readonly int AttackT = Animator.StringToHash("Attacking");
        [SerializeField] private GameObject patrolpoints;
        [SerializeField] private float patrolSpeed;
        private List<Vector3> patrollpoints = new List<Vector3>();
        private Vector3 currentDestination;
        private int currentIndex;
        [SerializeField] private float damage;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            foreach (Transform child in patrolpoints.transform)
            {
                patrollpoints.Add(child.position);
            }
            StartCoroutine(PatrollAndWait());
        }
        public void Attack()
        {
            doonceanim = true;
            isin = true;
            if (doonceanim)
            {
                attacking = true;
                anim.SetTrigger(AttackT);
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
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (Math.Sign(x) == 1f)
            {
                transform.eulerAngles = Vector3.zero;
            }
        }
        private void CalculateNewDestination()
        {
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
}