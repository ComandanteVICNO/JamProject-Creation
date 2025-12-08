using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWallet : MonoBehaviour
{
    public static UpgradeWallet instance;
    
    public List<Upgrade> upgradesList = new List<Upgrade>();

    public event Action<UpgradeTypes> OnUpgradeAdded;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        
    }

    private void Start()
    {
        GetInitialStats();
    }


    public void AddUpgrade(UpgradeTypes upgradeType, float percetange)
    {
        if (upgradeType == UpgradeTypes.E_Cooldown || upgradeType == UpgradeTypes.W_Rate || upgradeType == UpgradeTypes.P_Resistance)
        {
            percetange *= -1;
        }

        
        Upgrade upgrade = new Upgrade();
        upgrade.type = upgradeType;
        upgrade.percentage = percetange;
        upgradesList.Add(upgrade);
        OnUpgradeAdded?.Invoke(upgrade.type);
    }

    public float GetValue(UpgradeTypes upgradeType, float baseValue)
    {
        float percentage = 0;
        foreach (Upgrade upgrade in upgradesList)
        {
            if (upgrade.type == upgradeType)
            {
                percentage += upgrade.percentage;
            }
            else continue;
        }

        if (upgradeType == UpgradeTypes.P_Resistance && percentage <= -90) percentage = -90; // Limit Resistance to 90%
        float decimalValue = percentage / 100.0f;
        float aditiveValue = baseValue * decimalValue;
        return baseValue + aditiveValue;
    }

    void GetInitialStats()
    {
        if (GameSessionManager.instance.GetUpgrades() != null && GameSessionManager.instance.GetUpgrades().Count > 0)
        {
            foreach (var upgrade in GameSessionManager.instance.GetUpgrades())
            {
                AddUpgrade(upgrade.upgradeType,upgrade.upgradeValue);
            }
        }
    }
    
}
[Serializable]
public class Upgrade
{
    public UpgradeTypes type;
    public float percentage;
}
public enum UpgradeTypes
{
    P_Health,
    P_Resistance,
    P_Speed,
    W_Damage,
    W_Rate,
    W_Size,
    E_Health,
    E_Damage,
    E_Cooldown
    
}


