using System;
using System.Collections.Generic;
using Managers;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public event Action IsDead;
    public static UIManager Instance { private set; get; }
    [SerializeField] private PlayerHealthSystem playerHealthSystem;
    [SerializeField] private Image healthBar;
    [SerializeField] private List<Image> HP;
    private UIManager instance;

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

    private void OnEnable()
    {
        EventManager.instance.OnDamage += updateHealthBar;
        playerHealthSystem.LessHp += DeleateHP;
    }
    private void OnDisable()
    {
        EventManager.instance.OnDamage -= updateHealthBar;
        playerHealthSystem.LessHp += DeleateHP;
    }
    private void DeleateHP(float damage, float health)
    {
        if (HP.Count > 0)
        {
            Destroy(HP[0].gameObject);
            HP.RemoveAt(0);
        }
        healthBar.fillAmount = damage / health;
    }
    public void updateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        IsDead?.Invoke();
    }
}
