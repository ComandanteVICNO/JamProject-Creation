using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Bowtie_Projectile : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private float flightTime;
    [SerializeField] private float standByTime;
    [SerializeField] private float minCooldown;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    private float elapsedFlightTime = 0;
    float elapsedStandByTime = 0;
    [SerializeField] private float minAttackCooldown;
    private float attackElapsedTime = 0;
    private float damage;
    private Vector2 direction;
    private float size;
    Transform playerTransform;
    private BowtieState state;
    enum BowtieState
    {
        Going,
        Idle,
        Returning
    }
    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    
    public void SetupProjectile(Vector2 pDirection,  float pDamage, float pSize)
    {
        damage = pDamage;
        direction = pDirection;
            
        size = pSize;
        transform.localScale = new Vector3(pSize, pSize, pSize);
        state = BowtieState.Going;
        
    }

    private void Update()
    {
        transform.Rotate(0f,0f,rotationSpeed * Time.deltaTime);
        
        if (state == BowtieState.Going)
        {
            direction.Normalize();
            transform.position += new Vector3(direction.x * speed, direction.y * speed, 0) * Time.deltaTime;
            elapsedFlightTime += Time.deltaTime;
            if (elapsedFlightTime >= flightTime)
            {
                state = BowtieState.Idle;
            }
        }

        if (state == BowtieState.Idle)
        {
            elapsedStandByTime += Time.deltaTime;

            if (elapsedStandByTime >= standByTime)
            {
                state = BowtieState.Returning;
            }
            
        }

        if (state == BowtieState.Returning)
        {
            Vector2 distance = playerTransform.position - transform.position;
            direction = distance.normalized;
            transform.position += new Vector3(direction.x * speed, direction.y * speed, 0) * Time.deltaTime;

            if (distance.magnitude <= 1)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") return;
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(UpgradeWallet.instance.GetValue(UpgradeTypes.W_Damage, weaponData.damage));
            Debug.Log("Damaged: " +other.gameObject.name);
        }
    }

    
}
