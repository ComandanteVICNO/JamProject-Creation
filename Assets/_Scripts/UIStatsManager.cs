using System;
using TMPro;
using UnityEngine;

public class UIStatsManager : MonoBehaviour
{
    public static UIStatsManager instance;
    [SerializeField] private TMP_Text playerHeath;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text expText;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void UpdatePlayerHealth(float currentHealth, float maxHealth)
    {
        playerHeath.text = "Health: " + (int)currentHealth + " / " + (int)maxHealth;
    }

    public void UpdatePlayerLevel(int level)
    {
        levelText.text = "Level: " + level;
    }
    
    public void UpdatePlayerExperience(int exp, int maxExp)
    {
        expText.text = "Experience: " + exp +" / " + maxExp;
    }
    
}
