using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExperienceLevelManager : MonoBehaviour
{
    
    public static ExperienceLevelManager instance;
    
    private int currentLevel = 1;
    private float currentExperience = 0;
    private float maxExperience = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        maxExperience = 2 * Mathf.Pow(currentLevel, 1.15f);
        
        UpdateExperienceUI();
    }

    private void Update()
    {
        if (InputManager.instance.inputActions.Player.DebugLevelUp.WasPerformedThisFrame())
        {
            LevelUp();
        }
    }

    public void AddExperience(float experience)
    {
        currentExperience+=experience;
        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
        UpdateExperienceUI();
    }

    void LevelUp()
    {
        currentLevel++;
        currentExperience = 0;
        maxExperience = 2 * Mathf.Pow(currentLevel, 1.15f);
        UpdateExperienceUI();
        //Eventualy show levelup screen
        if (currentLevel % 5 == 0)
        {
            if (!LevelUpUiManager.instance.AreAllWeaponsEnabled())LevelUpUiManager.instance.ShowLevelUpWeaponPanel();
            else LevelUpUiManager.instance.ShowLevelUpStatPanel();
            
            LevelUpEnemies();
        }
        else
        {
            LevelUpUiManager.instance.ShowLevelUpStatPanel();
            LevelUpEnemies();
        }
    }
    void UpdateExperienceUI()
    {
        UIStatsManager.instance.UpdatePlayerLevel(currentLevel);
        UIStatsManager.instance.UpdatePlayerExperience((int)currentExperience, (int)maxExperience);
    }

    void LevelUpEnemies()
    {
        int index = Random.Range(1, 5);
        UpgradeTypes upgradeType = UpgradeTypes.E_Health;
        switch (index)
        {
            case 1:
                upgradeType = UpgradeTypes.E_Cooldown;
                break;
            case 2:
                upgradeType = UpgradeTypes.E_Damage;
                break;
            case 3:
                upgradeType = UpgradeTypes.E_Health;
                break;
            
        }

        float percentage = StatRoller.instance.RollStat();
        
        UpgradeWallet.instance.AddUpgrade(upgradeType, percentage);

    }
}
