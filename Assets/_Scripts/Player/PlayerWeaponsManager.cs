using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsManager : MonoBehaviour
{
    public static PlayerWeaponsManager instance;
    [SerializeField] private List<WeaponComponentHolder> weaponsList = new List<WeaponComponentHolder>();


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }


    public void EnableWeapon(WeaponTypes weaponType)
    {
        foreach (var weapon in weaponsList)
        {
            if (weaponType != weapon.weaponEnum) continue;
            weapon.scriptComponent.enabled = true;
            weapon.spriteComponent.enabled = true;
        }
    }
    
}

public enum WeaponTypes
{
    PaintBrush,
    Flashlight,
    TeslaTowers,
    MatchaDrink,
    Bowtie,
    Vampire
}
[System.Serializable]
public class WeaponComponentHolder
{
    public Weapon scriptComponent;
    public SpriteRenderer spriteComponent;
    public WeaponTypes weaponEnum;
}