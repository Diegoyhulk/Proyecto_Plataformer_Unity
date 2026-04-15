using System;
using Intefaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Collisions
{
    public class Sink : MonoBehaviour, ISinkable
    {
        float timer;
        private ParticleSystem ps;
        private bool isInside;
        [SerializeField] private InputReaderSO inputreader;
        void Awake()
        {
            ps = GetComponentInChildren<ParticleSystem>();
        }

        private void OnEnable()
        {
            inputreader.OnJumpStarted += Play;
            inputreader.OnJumpCanceled += Stop;
        }

        private void OnDisable()
        {
            inputreader.OnJumpStarted -= Play;
            inputreader.OnJumpCanceled -= Stop;
        }

        private void Play()
        {
            if (isInside)
            {
                ps.Play(); 
            }
        }

        private void Stop()
        {
            if (isInside)
            {
                ps.Stop();
            }
        }
        public void OnEnter(Rigidbody2D rb)
        {
            isInside = true;
            var vector3 = ps.transform.position;
            vector3.y = rb.position.y - 0.5f;
            ps.transform.position = vector3;
        }

        public void OnExit()
        {
            isInside = false;
        }

        public void Recuperar(ref bool moving, ref Rigidbody2D rb, ref Animator anim)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                moving = true;
                anim.SetBool("Spinning", false);
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.rotation = 0;
                timer = 0;
            }
        }
        public void PartMove(Rigidbody2D rb)
        {
            var vector3 = ps.transform.position;
            vector3.x = rb.position.x;
            ps.transform.position = vector3;
        }
    }
}