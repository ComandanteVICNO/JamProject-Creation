using System;
using UnityEngine;

public class GunProjectile : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    [SerializeField] private float speed;
    private float damage;
    private Vector2 direction;
    private float size;
    

    public void SetupProjectile(Vector2 pDirection,  float pDamage, float pSize)
    {
        damage = pDamage;
        direction = pDirection;
            
        size = pSize;
        transform.localScale = new Vector3(pSize, pSize, pSize);
        
        Destroy(gameObject, timeToDestroy);
        
    }


    private void Update()
    {
        direction.Normalize();
        transform.position += new Vector3(direction.x * speed, direction.y * speed, 0) * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damage);
            }
        }
    }
}
