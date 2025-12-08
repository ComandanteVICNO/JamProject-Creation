using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon_PaintBucket : Weapon
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float minCooldown;
    [SerializeField] private float spawnDistance;

    private float elapsedTime;
    Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        
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


    public override void Fire()
    {
        Vector2 position;
        Vector2 direction = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        direction.Normalize();
        position = new Vector3(playerTransform.position.x + direction.x * spawnDistance, playerTransform.position.y+ direction.y * spawnDistance, playerTransform.position.z);

        GameObject p = Instantiate(projectilePrefab, position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        Weapon_PaintBucket_Projectile projectileScript = p.GetComponent<Weapon_PaintBucket_Projectile>();
        projectileScript.SetupProjectile(UpgradeWallet.instance.GetValue(UpgradeTypes.W_Damage, weaponData.damage), UpgradeWallet.instance.GetValue(UpgradeTypes.W_Size, weaponData.size));
    }
}
