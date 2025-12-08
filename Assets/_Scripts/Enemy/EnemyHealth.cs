using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour,IDamageable
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] float health;
    [SerializeField] private GameObject hurtSpriteObject;
    [SerializeField] private float hurtTime;
    [SerializeField] private AudioClip[] hurtSounds;
    [SerializeField] private AudioSource enemyAudioSource;
    private void Start()
    {
        health = UpgradeWallet.instance.GetValue(UpgradeTypes.E_Health,enemyData.health);
    }


    public void Damage(float damage)
    {
        health -= damage;
        enemyAudioSource.pitch = Random.Range(0.8f, 1.2f);
        enemyAudioSource.PlayOneShot(hurtSounds[Random.Range(0, hurtSounds.Length)]);
        Debug.Log("Damaged for: " + damage + " | current health: " + health);
        if(health <= 0) Die();
        else
        {
            StartCoroutine(ShowHurtSprite());
        }
    }

    public void Die()
    {
        ExperienceLevelManager.instance.AddExperience(enemyData.xp);
        GameOverUIManager.instance.AddKillToStats();
        Destroy(gameObject);
    }

    IEnumerator ShowHurtSprite()
    {
        hurtSpriteObject.SetActive(true);
        yield return new WaitForSecondsRealtime(hurtTime);
        hurtSpriteObject.SetActive(false);
    }
    
}
