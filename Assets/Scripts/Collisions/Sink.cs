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
        public void OnEnter()
        {
            isInside = true;
        }

        public void OnExit()
        {
            isInside = false;
        }

        public void Recuperar(ref bool moving, ref Rigidbody2D rb)
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
        public void PartMove(Rigidbody2D rb)
        {
            var vector3 = ps.transform.position;
            vector3.x = rb.position.x;
            ps.transform.position = vector3;
        }
    }
}