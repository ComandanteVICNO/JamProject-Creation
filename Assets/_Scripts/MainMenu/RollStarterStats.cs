using System;
using TMPro;
using UnityEngine;

public class RollStarterStats : MonoBehaviour
{
    [SerializeField] GameObject healthLGObject;
    [SerializeField] GameObject speedLGObject;
    [SerializeField] GameObject resistanceLGObject;
    [SerializeField] TMP_Text healthText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text resistanceText;
    [SerializeField] private int reRollWallet;
    [SerializeField] private TMP_Text remainingRollsText;
    [SerializeField] private GameObject startButton;

    [Header("Dice Rolls")] 
    [SerializeField] private DiceRollBehaviour resistanceRoll;
    [SerializeField] private DiceRollBehaviour healthRoll;
    [SerializeField] private DiceRollBehaviour speedRoll;
    public void GetStarterRolls()
    {
        startButton.SetActive(true);
        if (reRollWallet <= 0) return;
        reRollWallet--;
        remainingRollsText.text = "Remaining Rolls: " + reRollWallet;
        GetResistanceRoll();
        GetHealthRoll();
        GetSpeedRoll();
    }

    void GetResistanceRoll()
    {
        resistanceLGObject.SetActive(true);
        float percentage = StatRoller.instance.RollStat();
        resistanceRoll.RollDice(StatRoller.instance.roll);
        if (percentage >= 0)
        {
            resistanceText.text = "+"+percentage + "%";
            resistanceText.color = Color.green;
        }
        else
        {
            resistanceText.text = percentage + "%";
            resistanceText.color = Color.red;
        }

        InitialUpgrade resUp = new InitialUpgrade();
        resUp.upgradeType = UpgradeTypes.P_Resistance;
        resUp.upgradeValue = percentage;
        GameSessionManager.instance.SetUpgrades(0, resUp);
    }
    
    void GetHealthRoll()
    {
        healthLGObject.SetActive(true);
        float percentage = StatRoller.instance.RollStat();
        healthRoll.RollDice(StatRoller.instance.roll);
        if (percentage >= 0)
        {
            healthText.text = "+"+percentage + "%";
            healthText.color = Color.green;
        }
        else
        {
            healthText.text = percentage + "%";
            healthText.color = Color.red;
        }

        InitialUpgrade healthUp = new InitialUpgrade();
        healthUp.upgradeType = UpgradeTypes.P_Health;
        healthUp.upgradeValue = percentage;
        GameSessionManager.instance.SetUpgrades(1, healthUp);
        
    }

    void GetSpeedRoll()
    {
        speedLGObject.SetActive(true);
        float percentage = StatRoller.instance.RollStat();
        speedRoll.RollDice(StatRoller.instance.roll);
        if (percentage >= 0)
        {
            speedText.text = "+"+percentage + "%";
            speedText.color = Color.green;
        }
        else
        {
            speedText.text = percentage + "%";
            speedText.color = Color.red;
        }

        InitialUpgrade speedUp = new InitialUpgrade();
        speedUp.upgradeType = UpgradeTypes.P_Speed;
        speedUp.upgradeValue = percentage;
        GameSessionManager.instance.SetUpgrades(2, speedUp);
    }

}
