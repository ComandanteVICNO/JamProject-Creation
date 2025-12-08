using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GruntController : EnemyController
{
    GameObject player;
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private float maxDistanceToPlayer;
    bool playerInRange = false;
    float attackElapsedTime = 0;
    [SerializeField] private AudioClip[] attackSounds;
    [SerializeField] private AudioSource enemyAudioSource;
    
    private void Awake()
    {
        player =  GameObject.FindGameObjectWithTag("Player");
        
    }


    private void Update()
    {
        HandleMovement(player.transform, maxDistanceToPlayer, enemyData.speed);
       
        HandleAttacking();
    }
    
    
    
    
    
    private void HandleAttacking()
    {
        if (!playerInRange) return;

        attackElapsedTime += Time.deltaTime;
        
        float cooldown = UpgradeWallet.instance.GetValue(UpgradeTypes.E_Cooldown, enemyData.attackCooldown);
        if(cooldown <= 0.4f) cooldown = 0.4f;
        if (attackElapsedTime >= cooldown)
        {
            Attack();
            attackElapsedTime = 0f;
        }
    }

    public override void Attack()
    {
        enemyAudioSource.pitch = Random.Range(0.8f, 1.2f);
        enemyAudioSource.PlayOneShot(attackSounds[Random.Range(0,attackSounds.Length)]);
        IDamageable damageable = player.GetComponent<IDamageable>();
        damageable.Damage(UpgradeWallet.instance.GetValue(UpgradeTypes.E_Damage, enemyData.damage));
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            attackElapsedTime = 0f; // restart the timer when entering
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            attackElapsedTime = 0f;
        }
    }
}

