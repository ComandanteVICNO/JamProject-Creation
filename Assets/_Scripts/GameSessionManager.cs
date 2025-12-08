using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionManager : MonoBehaviour
{
    public static GameSessionManager instance;
    [SerializeField]  Color playerColor;
    [SerializeField] List<InitialUpgrade> initialUpgrades = new List<InitialUpgrade>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        for(int i= 0; i < 3; i++)
        {
            initialUpgrades.Add(new InitialUpgrade());
        }
    }

    public void SetUpgrades(int index, InitialUpgrade upgrade) {initialUpgrades[index] = upgrade;}
    public List<InitialUpgrade> GetUpgrades(){return initialUpgrades;}
    
    public void SetPlayerColor(Color c){playerColor = c;}
    public Color GetPlayerColor(){return playerColor;}
    
    
}

[Serializable]
public class InitialUpgrade
{
    public UpgradeTypes upgradeType;
    public float upgradeValue;
}