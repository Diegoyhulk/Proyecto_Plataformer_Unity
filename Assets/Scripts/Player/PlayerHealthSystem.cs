using System;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerHealthSystem : PlayerSystem
    {
        public event Action<float, float> LessHp;
        private int Lives = 3;
        [SerializeField] UIManager UiManager;
        private void OnEnable()
        {
            UiManager.IsDead += IfIsDead;
        }

        private void OnDisable()
        {
            UiManager.IsDead -= IfIsDead;
        }

        private void IfIsDead()
        {
            if (main.currentdamage >= main.maxhealth)
            {
                if (Lives > 0)
                {
                    main.rb.linearVelocity= Vector2.zero;
                    main.rb.transform.position = main.Checkpointtransform;
                    main.currentdamage = 0;
                    LessHp?.Invoke(main.currentdamage, main.maxhealth);
                    Lives--;
                }
                else if (Lives == 0)
                {
                    SceneManager.LoadScene("GameOver");
                }
            }
        }
    }
}
