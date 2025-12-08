using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Flashlight : Weapon
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform playerTransform;

    [SerializeField]List<GameObject> enemies;
    [SerializeField] private float minCooldown;
    private float elapsedTime;
    private void Awake()
    {
        enemies = new List<GameObject>();
    }

    private void Update()
    {
        Vector2 direction = InputManager.instance.GetPlayerMovement();
        direction.Normalize();
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

        }

        HandleDamage();
        
    }


    void HandleDamage()
    {
        elapsedTime += Time.deltaTime;
        
        float cooldown = UpgradeWallet.instance.GetValue(UpgradeTypes.W_Rate, weaponData.rate);
        if(cooldown <= minCooldown) cooldown = minCooldown;
        if (elapsedTime >= cooldown)
        {
            foreach (var enemy in enemies)
            {
                IDamageable damageable = enemy.GetComponent<IDamageable>();
                damageable.Damage(UpgradeWallet.instance.GetValue(UpgradeTypes.W_Damage, weaponData.damage));
            }
            elapsedTime = 0;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Enter" + other.name);
        if (other.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Remove(other.gameObject);
        }
    }
}
