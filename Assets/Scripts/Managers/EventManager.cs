using System;
using UnityEngine;

namespace Managers
{
    public class EventManager: MonoBehaviour
    {
        public static EventManager instance;
        public event Action<float, float> OnDamage;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else 
            {
                Destroy(gameObject);
            }
        }
        public void PlayerDamage(float currentHealth, float maxHealth)
        {
            OnDamage?.Invoke(currentHealth, maxHealth);
        }
    }
}