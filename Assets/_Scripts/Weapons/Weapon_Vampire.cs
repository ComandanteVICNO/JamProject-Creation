using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon_Vampire : Weapon
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject healingSpriteObject;
    [SerializeField] private float healingSpriteTime;
    [SerializeField] private float minCooldown;
    [SerializeField] private GameObject vampireProjectile;
    [SerializeField] private float vampireProjectileTime;
    Transform playerTransform;
    float elapsedTime = 0;
    private Transform enemyTransform;
    private void Awake()
    {
        
        playerTransform = GameObject.FindWithTag("Player").transform;
        playerHealth = playerTransform.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        float cooldown = UpgradeWallet.instance.GetValue(UpgradeTypes.W_Rate, weaponData.rate);
        if(cooldown <= minCooldown) cooldown = minCooldown;
        if (elapsedTime >= cooldown)
        {
            Fire();
            elapsedTime = 0;
        }
    }

    override public void Fire()
    {
        float minDistance = float.MaxValue;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in EnemySpawnManager.instance.enemies)
        {
            Vector2 distance = enemy.transform.position - transform.position;
            if (distance.magnitude < minDistance)
            {
                minDistance = distance.magnitude;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            enemyTransform = nearestEnemy.transform;
            Vector3 distanceToEnemy = enemyTransform.position - transform.position;
            if (distanceToEnemy.magnitude < UpgradeWallet.instance.GetValue(UpgradeTypes.W_Size,weaponData.size))
            {
                IDamageable damageable = enemyTransform.gameObject.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.Damage(UpgradeWallet.instance.GetValue(UpgradeTypes.W_Damage, weaponData.damage));
                    playerHealth.Heal(UpgradeWallet.instance.GetValue(UpgradeTypes.W_Damage, weaponData.damage));
                    SpawnVampireProjectile(nearestEnemy.transform);
                    StartCoroutine(PlayeHealingAnimation());
                }
            }
        }
    }


    void SpawnVampireProjectile(Transform enemyTransform)
    {
       GameObject projectile = Instantiate(vampireProjectile,enemyTransform);
       Destroy(projectile, vampireProjectileTime);
    }

    IEnumerator PlayeHealingAnimation()
    {
        healingSpriteObject.SetActive(true);
        
        yield return new WaitForSecondsRealtime(healingSpriteTime);
        healingSpriteObject.SetActive(false);
    }
    
}
