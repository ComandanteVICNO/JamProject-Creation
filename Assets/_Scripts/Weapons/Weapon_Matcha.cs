using System;
using System.Collections;
using UnityEngine;

public class Weapon_Matcha : Weapon
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private float minCooldown;
    [SerializeField] private float pushForce;
    [SerializeField] private CircleCollider2D collider;

    [SerializeField] private float animationTime;
    [SerializeField] private GameObject healingSpriteObject;
    
    private float originalColliderSize;
    private float elapsedTime = 0;
    private float forceFieldActiveTime = 0.5f;
    private float forceFieldElapsedTime = 0;
    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        collider = GetComponent<CircleCollider2D>();
        originalColliderSize = collider.radius;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        float cooldown = UpgradeWallet.instance.GetValue(UpgradeTypes.W_Rate, weaponData.rate);
        if(cooldown <= minCooldown) cooldown = minCooldown;
        if (elapsedTime >= cooldown)
        {
            playerHealth.Heal(UpgradeWallet.instance.GetValue(UpgradeTypes.W_Damage, weaponData.damage));
            elapsedTime = 0;
            StartCoroutine(EnableCollider());
            StartCoroutine(PlayeHealingAnimation());
        }
    }


    IEnumerator EnableCollider()
    {
        collider.enabled = true;
        collider.radius = UpgradeWallet.instance.GetValue(UpgradeTypes.W_Size, originalColliderSize);
        yield return new WaitForSecondsRealtime(0.1f);
        collider.enabled = false;
    }

    IEnumerator PlayeHealingAnimation()
    {
        healingSpriteObject.SetActive(true);
        
        yield return new WaitForSecondsRealtime(animationTime);
        healingSpriteObject.SetActive(false);
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Enemy") return;
        Vector2 direction = other.transform.position - transform.position;
        direction.Normalize();
        Transform enemyTrasform = other.transform;
        enemyTrasform.position = new Vector3(enemyTrasform.position.x + direction.x * pushForce, enemyTrasform.position.y + direction.y * pushForce, enemyTrasform.position.z);
    }
}
