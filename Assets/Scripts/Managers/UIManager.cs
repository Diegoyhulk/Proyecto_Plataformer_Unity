using System;
using System.Collections.Generic;
using Managers;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class UIManager : MonoBehaviour
{
    public event Action IsDead;
    public static UIManager Instance { private set; get; }
    [SerializeField] PlayerHealthSystem playerHealthSystem;
    [SerializeField] private Image healthBar;
    [SerializeField] private List<Image> HP;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        EventManager.instance.OnDamage += updateHealthBar;
        playerHealthSystem.LessHp += DeleateHP;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        EventManager.instance.OnDamage -= updateHealthBar;
        playerHealthSystem.LessHp -= DeleateHP;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerHealthSystem = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerHealthSystem>();
        healthBar.fillAmount = 0f;
        RebuildHPImages();
    }

    private void RebuildHPImages()
    {
        
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
