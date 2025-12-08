using System;
using UnityEngine;

public class RangerController : EnemyController
{
    
    GameObject player;
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private float maxDistanceToPlayer;
    [SerializeField] private GameObject projectilePrefab;
    
    [SerializeField] private AudioClip[] attackSounds;
    [SerializeField] private AudioSource enemyAudioSource;
    float elapsedTime = 0;
    private void Awake()
    {
        player =  GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Update()
    {
        if (HandleMovement(player.transform,  maxDistanceToPlayer,enemyData.speed))
        {
            float cooldown = UpgradeWallet.instance.GetValue(UpgradeTypes.E_Cooldown, enemyData.attackCooldown);
            if(cooldown <= 0.4f) cooldown = 0.4f;
            
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= cooldown)
            {
                Attack();
                elapsedTime = 0;
            }
        }
    }
    
    
    public override void Attack()
    {
        enemyAudioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        enemyAudioSource.PlayOneShot(attackSounds[UnityEngine.Random.Range(0,attackSounds.Length)]);
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Vector2 direction = (player.transform.position - transform.position).normalized;
        EnemyProjectile eProjectile = projectile.GetComponent<EnemyProjectile>();
        eProjectile.SetupProjectile(direction, UpgradeWallet.instance.GetValue(UpgradeTypes.E_Damage,enemyData.damage),1f);
    }
    
}
