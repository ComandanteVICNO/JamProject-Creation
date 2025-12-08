using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelUpUiManager : MonoBehaviour
{
    public static LevelUpUiManager instance;
    
    
    [Header("StatPanel")]
    [SerializeField] GameObject statPanel;
    [SerializeField] private TMP_Text statTitle;
    [SerializeField] private TMP_Text statDescription;
    [SerializeField] private Image statImage;
    [SerializeField] private TMP_Text statAmmountText;
    [SerializeField] private TMP_Text statRemainingRollsText;
    private float statAmmount = 0;
    
    
    [Header("WeaponPanel")] 
    [SerializeField] private GameObject weaponPanel;
    [SerializeField] private TMP_Text weaponTitle;
    [SerializeField] private TMP_Text weaponDescription;
    [SerializeField] private Image weaponImage;
    [SerializeField] private TMP_Text weaponRemainingRollsText;
    
    [Header("Lists")]
    [SerializeField]List<StatLevelUp> statLevelUpList= new List<StatLevelUp>();
    [SerializeField]List<WeaponLevelUp> weaponLevelUpList = new List<WeaponLevelUp>();

    [Header("Values")] [SerializeField] private int rollWallet;
    private int currentRollsRemaining;
    
    private StatLevelUp currentStatLevelUp;
    private WeaponLevelUp currentWeaponLevelUp;

    [SerializeField] DiceRollBehaviour diceRoll;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        currentRollsRemaining = rollWallet;
    }
    
    public void ShowLevelUpStatPanel()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        statPanel.SetActive(true);
        currentStatLevelUp = statLevelUpList[Random.Range(0, statLevelUpList.Count)];
        statAmmount = StatRoller.instance.RollStat();
        diceRoll.RollDice(StatRoller.instance.roll);
        
        
        statTitle.text = currentStatLevelUp.title;
        statDescription.text = currentStatLevelUp.description;
        statImage.sprite = currentStatLevelUp.icon;
        if (statAmmount > 0)
        {
            statAmmountText.color = Color.green;
            statAmmountText.text = "+"+statAmmount;
        }
        else if (statAmmount == 0)
        {
            statAmmountText.color = Color.saddleBrown;
            statAmmountText.text = ""+statAmmount;
        }
        else
        {
            statAmmountText.color = Color.darkRed;
            statAmmountText.text = ""+statAmmount;
        }
        
        statRemainingRollsText.text = "Remaining Rolls: " + currentRollsRemaining;
    }

    public void ReRollStat()
    {
        if (currentRollsRemaining <= 0) return;
        currentRollsRemaining--;
        ShowLevelUpStatPanel();
    }

    public void AcceptStatUpgrade()
    {
        currentRollsRemaining = rollWallet;
        UpgradeWallet.instance.AddUpgrade(currentStatLevelUp.upgradeType, statAmmount);
        statPanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowLevelUpWeaponPanel()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        weaponPanel.SetActive(true);
        currentWeaponLevelUp = weaponLevelUpList[Random.Range(0, weaponLevelUpList.Count)];

        if (currentWeaponLevelUp.isWeaponEnabled)
        {
            ShowLevelUpWeaponPanel();
            return;
        }
        
        weaponTitle.text = currentWeaponLevelUp.title;
        weaponDescription.text = currentWeaponLevelUp.description;
        weaponImage.sprite = currentWeaponLevelUp.icon;
        weaponRemainingRollsText.text = "Remaining Rolls: " + currentRollsRemaining;
    }

    public void ReRollWeapon()
    {
        if(currentRollsRemaining <= 0) return;
        currentRollsRemaining--;
        ShowLevelUpWeaponPanel();
    }

    public void AcceptWeapon()
    {
        currentRollsRemaining = rollWallet;
        PlayerWeaponsManager.instance.EnableWeapon(currentWeaponLevelUp.weaponType);
        currentWeaponLevelUp.isWeaponEnabled = true;
        weaponPanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public bool AreAllWeaponsEnabled()
    {
        foreach (var weapon in weaponLevelUpList)
        {
            if (!weapon.isWeaponEnabled) 
                return false;
        }
        return true; 
    }
    
    
}

[Serializable]
public class StatLevelUp
{
    public string title;
    public string description;
    public Sprite icon;
    public UpgradeTypes upgradeType;
}

[Serializable]
public class WeaponLevelUp
{
    public string title;
    public string description;
    public Sprite icon;
    public WeaponTypes weaponType;
    public bool isWeaponEnabled = false;
}