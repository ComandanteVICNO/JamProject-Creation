using UnityEngine;

public class Weapon_Bowtie : Weapon
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] GameObject projectilePrefab;
    private float rateElapsedTime = 0;
    [SerializeField] private float minCooldown;
    
    private void Update()
    {
        rateElapsedTime += Time.deltaTime;
        float cooldown = UpgradeWallet.instance.GetValue(UpgradeTypes.W_Rate, weaponData.rate);
        if (cooldown <= minCooldown) cooldown = minCooldown;
        if (rateElapsedTime >= cooldown)
        {
            Fire();
            rateElapsedTime = 0;
        }
        
    }
    
    
    Vector2 GetNearestEnemyDirection()
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

        if (nearestEnemy == null) return Vector2.zero;
        Vector2 directionToEnemy = nearestEnemy.transform.position - transform.position;
        directionToEnemy.Normalize();
        return directionToEnemy;
    }
    
    public override void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Weapon_Bowtie_Projectile projectileScript = projectile.GetComponent<Weapon_Bowtie_Projectile>();
        projectileScript.SetupProjectile(GetNearestEnemyDirection(), UpgradeWallet.instance.GetValue(UpgradeTypes.W_Damage, weaponData.damage), UpgradeWallet.instance.GetValue(UpgradeTypes.W_Size, weaponData.size));
    }
    
    
}
