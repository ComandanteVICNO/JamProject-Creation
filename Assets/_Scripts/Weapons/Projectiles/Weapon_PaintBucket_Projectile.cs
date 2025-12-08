using System;
using UnityEngine;

public class Weapon_PaintBucket_Projectile : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private float timeToKill;
    private float damage;
    private float size;
    
    
    
    public void SetupProjectile(float pDamage, float pSize)
    {
        damage = pDamage;
        size = pSize;
        transform.localScale = new Vector3(pSize, pSize, pSize);
        Destroy(gameObject, timeToKill);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(UpgradeWallet.instance.GetValue(UpgradeTypes.W_Size, damage));
            }
        }
    }
}
