using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,IDamageable
{
    [SerializeField] PlayerData playerData;
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;


    private void Awake()
    {
        maxHealth = currentHealth = playerData.maxHealth;
        UpgradeWallet.instance.OnUpgradeAdded += UpdateMaxHealth;
    }

    void UpdateMaxHealth(UpgradeTypes upgradeType)
    {
       
        if (upgradeType == UpgradeTypes.P_Health)
        {
            float currentHealthFraction = currentHealth / maxHealth;
        
            float newMaxHealth = UpgradeWallet.instance.GetValue(UpgradeTypes.P_Health, playerData.maxHealth);
            maxHealth = newMaxHealth;

            currentHealth = maxHealth * currentHealthFraction;
        }

        UpdateHealthUI();
    }

    public void Damage(float damage)
    {
        currentHealth -= UpgradeWallet.instance.GetValue(UpgradeTypes.P_Resistance, damage);
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Die();
        }
        
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealthUI();
    }
    
    public void Die()
    {
        GameOverUIManager.instance.GameOver();
    }

    void UpdateHealthUI()
    {
        UIStatsManager.instance.UpdatePlayerHealth(currentHealth, maxHealth);
    }
}
